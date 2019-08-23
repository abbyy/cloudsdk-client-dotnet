// Copyright © 2019 ABBYY Production LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Abbyy.CloudSdk.V2.Client.Models;
using Abbyy.CloudSdk.V2.Client.Models.Enums;
using Abbyy.CloudSdk.V2.Client.Models.RequestParams;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Abbyy.CloudSdk.V2.Client.Sample.Core
{
	public class Program
	{
		private const string ApplicationId = "PASTE_Application_ID";
		private const string Password = "PAST_Application_Password";
		private const string FilePath = "New Image.jpg";

		/// <summary>
		/// Processing Location URL https://www.ocrsdk.com/documentation/specifications/data-processing-location/
		/// </summary>
		private const string ServiceUrl = "https://cloud-eu.ocrsdk.com";

		private static int _retryCount = 3;
		private static int _delayBetweenRetriesInSeconds = 3;
		private static string _httpClientName = "OCR_HTTP_CLIENT";

		private static readonly AuthInfo _authInfo = new AuthInfo
		{
			Host = ServiceUrl,
			ApplicationId = ApplicationId,
			Password = Password
		};

		private static ServiceProvider _serviceProvider;
		private static HttpClient _httpClient;

		public static async Task Main()
		{
			// Create service collection and configure our services
			var services = ConfigureServices();
			// Generate a provider
			_serviceProvider = services.BuildServiceProvider();

			var httpClientFactory = _serviceProvider.GetService<IHttpClientFactory>();
			_httpClient = httpClientFactory.CreateClient(_httpClientName);

			//Run examples
			var resultUrls = await ProcessImageAsync();
			foreach (var resultUrl in resultUrls)
				Console.WriteLine(resultUrl);

			var finishedTasks = await GetFinishedTasksWithRetry();
			foreach (var finishedTask in finishedTasks.Tasks)
				Console.WriteLine(finishedTask.TaskId);

			DisposeServices();
		}

		private static ServiceCollection ConfigureServices()
		{
			var services = new ServiceCollection();

			//Configure HttpClientFactory with retry handler
			services.AddHttpClient(_httpClientName, conf =>
				{
					conf.BaseAddress = new Uri(_authInfo.Host);
					//increase the default value of timeout for the duration of retries
					conf.Timeout = conf.Timeout + TimeSpan.FromSeconds(_retryCount * _delayBetweenRetriesInSeconds);
				})
				.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
				{
					PreAuthenticate = true,
					Credentials = new NetworkCredential(_authInfo.ApplicationId, _authInfo.Password)
				})
				//add polly
				.AddPolicyHandler(GetRetryPolicy());
			return services;
		}

		public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
		{
			return HttpPolicyExtensions.HandleTransientHttpError()
				//Condition - what kind of request errors should we repeat
				.OrResult(r => r.StatusCode == HttpStatusCode.GatewayTimeout)
				.WaitAndRetryAsync(
					_retryCount,
					sleepDurationProvider => TimeSpan.FromSeconds(_delayBetweenRetriesInSeconds),
					(exception, calculatedWaitDuration, retries, context) =>
					{
						Console.WriteLine($"Retry {retries} for policy with key {context.PolicyKey}");
					}
				)
				.WithPolicyKey("WaitAndRetryAsync_For_GatewayTimeout_504__StatusCode");
		}

		private static async Task<List<string>> ProcessImageAsync()
		{
			var imageParams = new ImageProcessingParams
			{
				ExportFormats = new[] {ExportFormat.Docx, ExportFormat.Txt},
				Language = "English,French"
			};

			using (var fileStream = new FileStream(FilePath, FileMode.Open))
			using (var client = new OcrClient(_authInfo))
			{
				var taskInfo = await client.ProcessImageAsync(
					imageParams,
					fileStream,
					Path.GetFileName(FilePath),
					true);

				return taskInfo.ResultUrls;
			}
		}

		private static async Task<TaskList> GetFinishedTasksWithRetry()
		{
			//here we use HttpClient with retry policy
			using (var client = new OcrClient(_httpClient))
			{
				var finishedTasks = await client.ListFinishedTasksAsync();
				return finishedTasks;
			}
		}

		private static void DisposeServices()
		{
			_serviceProvider?.Dispose();
		}
	}
}
