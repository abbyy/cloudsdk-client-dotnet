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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Abbyy.CloudSdk.V2.Client.Models;
using Abbyy.CloudSdk.V2.Client.Models.RequestParams;
using Abbyy.CloudSdk.V2.Client.Utils;
using Newtonsoft.Json;

namespace Abbyy.CloudSdk.V2.Client
{
	/// <inheritdoc />
	public class OcrClient : IOcrClient
	{
		private static readonly string Version = Assembly
			.GetAssembly(typeof(OcrClient))
			.GetName()
			.Version
			.ToString();

		protected readonly HttpClient HttpClient;

		private bool _disposed;

		/// <summary>
		/// Instantiates <see cref="OcrClient"/> class
		/// </summary>
		/// <param name="authInfo">Connection configuration info</param>
		public OcrClient(AuthInfo authInfo)
		{
			var handler = new HttpClientHandler
			{
				Credentials = new NetworkCredential(authInfo.ApplicationId, authInfo.Password),
				PreAuthenticate = true,
			};

			HttpClient = new HttpClient(handler)
			{
				BaseAddress = new Uri(authInfo.Host),
			};

			AddUserAgentHeader(HttpClient);
		}

		public OcrClient(HttpClient httpClient)
		{
			HttpClient = httpClient;

			AddUserAgentHeader(HttpClient);
		}

		/// <inheritdoc />
		public Task<TaskInfo> ProcessImageAsync(
			ImageProcessingParams parameters,
			Stream fileStream,
			string fileName,
			bool waitTaskFinished = false,
			CancellationToken cancellationToken = default)
		{
			return StartTaskAsync(
				HttpMethod.Post,
				Urls.Ocr.ProcessImage,
				parameters,
				fileStream,
				fileName,
				waitTaskFinished,
				cancellationToken);
		}

		/// <inheritdoc />
		public Task<TaskInfo> SubmitImageAsync(
			ImageSubmittingParams parameters,
			Stream fileStream,
			string fileName,
			CancellationToken cancellationToken = default)
		{
			return MakeRequestAsync<TaskInfo>(
				HttpMethod.Post,
				Urls.Ocr.SubmitImage,
				parameters,
				fileStream,
				fileName,
				cancellationToken);
		}

		/// <inheritdoc />
		public Task<TaskInfo> ProcessDocumentAsync(
			DocumentProcessingParams parameters,
			bool waitTaskFinished = false,
			CancellationToken cancellationToken = default)
		{
			return StartTaskAsync(
				HttpMethod.Post,
				Urls.Ocr.ProcessDocument,
				parameters,
				null,
				null,
				waitTaskFinished: waitTaskFinished,
				cancellationToken: cancellationToken);
		}

		/// <inheritdoc />
		public Task<TaskInfo> GetTaskStatusAsync(
			Guid taskId,
			CancellationToken cancellationToken = default)
		{
			return MakeRequestAsync<TaskInfo>(
				HttpMethod.Get,
				Urls.Ocr.GetTaskStatus,
				new { taskId },
				cancellationToken: cancellationToken);
		}

		/// <inheritdoc />
		public Task<TaskList> ListTasksAsync(
			TasksListingParams parameters,
			CancellationToken cancellationToken = default)
		{
			return MakeRequestAsync<TaskList>(
				HttpMethod.Get,
				Urls.Ocr.ListTasks,
				parameters,
				cancellationToken: cancellationToken);
		}

		/// <inheritdoc />
		public Task<TaskList> ListFinishedTasksAsync(
			CancellationToken cancellationToken = default)
		{
			return MakeRequestAsync<TaskList>(
				HttpMethod.Get,
				Urls.Ocr.ListFinishedTasks,
				cancellationToken: cancellationToken);
		}

		/// <inheritdoc />
		public Task<TaskInfo> DeleteTaskAsync(
			Guid taskId,
			CancellationToken cancellationToken = default)
		{
			return MakeRequestAsync<TaskInfo>(
				HttpMethod.Post,
				Urls.Ocr.DeleteTask,
				new { taskId },
				cancellationToken: cancellationToken);
		}

		/// <inheritdoc />
		public Task<TaskInfo> ProcessBarcodeFieldAsync(
			BarcodeFieldProcessingParams parameters,
			Stream fileStream,
			string fileName,
			bool waitTaskFinished = false,
			CancellationToken cancellationToken = default)
		{
			return StartTaskAsync(
				HttpMethod.Post,
				Urls.Ocr.ProcessBarcodeField,
				parameters,
				fileStream,
				fileName,
				waitTaskFinished,
				cancellationToken);
		}

		/// <inheritdoc />
		public Task<TaskInfo> ProcessCheckmarkFieldAsync(
			CheckmarkFieldProcessingParams parameters,
			Stream fileStream,
			string fileName,
			bool waitTaskFinished = false,
			CancellationToken cancellationToken = default)
		{
			return StartTaskAsync(
				HttpMethod.Post,
				Urls.Ocr.ProcessCheckmarkField,
				parameters,
				fileStream,
				fileName,
				waitTaskFinished,
				cancellationToken);
		}

		/// <inheritdoc />
		public Task<TaskInfo> ProcessTextFieldAsync(
			TextFieldProcessingParams parameters,
			Stream fileStream,
			string fileName,
			bool waitTaskFinished = false,
			CancellationToken cancellationToken = default)
		{
			return StartTaskAsync(
				HttpMethod.Post,
				Urls.Ocr.ProcessTextField,
				parameters,
				fileStream,
				fileName,
				waitTaskFinished,
				cancellationToken);
		}

		/// <inheritdoc />
		public Task<TaskInfo> ProcessFieldsAsync(
			FieldsProcessingParams parameters,
			Stream fileStream,
			string fileName,
			bool waitTaskFinished = false,
			CancellationToken cancellationToken = default)
		{
			return StartTaskAsync(
				HttpMethod.Post,
				Urls.Ocr.ProcessFields,
				parameters,
				fileStream,
				fileName,
				waitTaskFinished,
				cancellationToken);
		}

		/// <inheritdoc />
		public Task<TaskInfo> ProcessBusinessCardAsync(
			BusinessCardProcessingParams parameters,
			Stream fileStream,
			string fileName,
			bool waitTaskFinished = false,
			CancellationToken cancellationToken = default)
		{
			return StartTaskAsync(
				HttpMethod.Post, 
				Urls.Ocr.ProcessBusinessCard,
				parameters,
				fileStream,
				fileName,
				waitTaskFinished,
				cancellationToken);
		}

		/// <inheritdoc />
		public Task<TaskInfo> ProcessReceiptAsync(
			ReceiptProcessingParams parameters,
			Stream fileStream,
			string fileName,
			bool waitTaskFinished = false,
			CancellationToken cancellationToken = default)
		{
			return StartTaskAsync(
				HttpMethod.Post, 
				Urls.Ocr.ProcessReceipt,
				parameters,
				fileStream,
				fileName,
				waitTaskFinished,
				cancellationToken);
		}

		/// <inheritdoc />
		public Task<TaskInfo> ProcessMrzAsync(
			MrzProcessingParams parameters,
			Stream fileStream,
			string fileName,
			bool waitTaskFinished = false,
			CancellationToken cancellationToken = default)
		{
			return StartTaskAsync(
				HttpMethod.Post, 
				Urls.Ocr.ProcessMrz,
				parameters,
				fileStream,
				fileName,
				waitTaskFinished,
				cancellationToken);
		}

		/// <inheritdoc />
		public Task<Application> GetApplicationInfoAsync(
			CancellationToken cancellationToken = default)
		{
			return MakeRequestAsync<Application>(
				HttpMethod.Get,
				Urls.Ocr.GetApplicationInfo,
				cancellationToken: cancellationToken);
		}

		protected virtual async Task<T> MakeRequestAsync<T>(
			HttpMethod method,
			string requestUrl,
			object body = null,
			Stream fileStream = null,
			string fileName = null,
			CancellationToken cancellationToken = default)
		{
			using (var request = BuildRequest(method, requestUrl, body, fileStream, fileName))
			{
				var response = await HttpClient
					.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
					.ConfigureAwait(false);

				return await ProcessResponseAsync<T>(response)
					.ConfigureAwait(false);
			}
		}

		protected virtual async Task<TaskInfo> StartTaskAsync(
			HttpMethod method,
			string requestUrl,
			object body,
			Stream fileStream,
			string fileName,
			bool waitTaskFinished = false,
			CancellationToken cancellationToken = default)
		{
			var task = await MakeRequestAsync<TaskInfo>(method, requestUrl, body, fileStream, fileName, cancellationToken)
				.ConfigureAwait(false);

			if (waitTaskFinished == false)
			{
				return task;
			}

			while (task.IsInProcess())
			{
				cancellationToken.ThrowIfCancellationRequested();

				await Task.Delay(TimeSpan.FromMilliseconds(task.RequestStatusDelay), cancellationToken)
					.ConfigureAwait(false);

				task = await GetTaskStatusAsync(task.TaskId, cancellationToken)
					.ConfigureAwait(false);
			}

			return task;
		}

		protected virtual HttpRequestMessage BuildRequest(
			HttpMethod method,
			string relativeUrl,
			object body,
			Stream fileStream,
			string fileName)
		{
			var request = new HttpRequestMessage
			{
				Method = method,
			};

			request.Headers
				.Accept
				.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

			var query = Serializer.ToQueryString(body);

			request.RequestUri = new Uri($"{relativeUrl}?{query}", UriKind.Relative);

			if (fileStream != null)
			{
				var multipartContent = new MultipartFormDataContent
				{
					{ new StreamContent(fileStream), fileName, fileName }
				};

				request.Content = multipartContent;
			}

			return request;
		}

		protected virtual async Task<T> ProcessResponseAsync<T>(HttpResponseMessage response)
		{
			using (response)
			{
				var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
				if (response.Content?.Headers != null)
				{
					foreach (var header in response.Content.Headers)
					{
						headers[header.Key] = header.Value;
					}
				}

				var responseData = response.Content is null
					? null
					: await response
						.Content
						.ReadAsStringAsync()
						.ConfigureAwait(false);

				if (response.StatusCode == HttpStatusCode.OK)
				{
					try
					{
						return JsonConvert.DeserializeObject<T>(responseData);
					}
					catch (Exception exception)
					{
						throw new ApiException(
							"Could not deserialize the response body.",
							(int)response.StatusCode,
							Error.FromText(responseData),
							headers,
							exception);
					}
				}

				throw new ApiException(
							$"Server responded with {response.StatusCode} status code",
							(int)response.StatusCode,
							TryDeserializeError(responseData),
							headers);
			}
		}

		protected virtual Error TryDeserializeError(string responseData)
		{
			try
			{
				return JsonConvert.DeserializeObject<Error>(responseData);
			}
			catch
			{
				return Error.FromText(responseData);
			}
		}

		/// <inheritdoc cref="IDisposable" />
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}

			if (disposing)
			{
				HttpClient?.Dispose();
			}

			_disposed = true;
		}

		private void AddUserAgentHeader(HttpClient client)
		{
			client.DefaultRequestHeaders
				.UserAgent
				.Add(new ProductInfoHeaderValue("C#", Version));
		}
	}
}
