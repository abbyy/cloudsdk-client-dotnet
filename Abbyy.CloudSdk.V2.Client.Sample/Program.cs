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

using Abbyy.CloudSdk.V2.Client.Models;
using Abbyy.CloudSdk.V2.Client.Models.Enums;
using Abbyy.CloudSdk.V2.Client.Models.RequestParams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Abbyy.CloudSdk.V2.Client.Sample.RetryPolicySample;
using BusinessCardExportFormat = Abbyy.CloudSdk.V2.Client.Models.Enums.BusinessCardExportFormat;

namespace Abbyy.CloudSdk.V2.Client.Sample
{
	public class Program
	{
		private const string ApplicationId = "YOUR_APP_ID";
		private const string Password = "YOUR_APP_PWD";
		private const string FilePath = "YOUR_FILE_PATH";
		private const string HostUrl = "https://cloud-westus.ocrsdk.com";

		private static AuthInfo _authInfo;

	    public static async Task Main()
	    {
		    _authInfo = new AuthInfo
		    {
			    Host = HostUrl,
			    ApplicationId = ApplicationId,
			    Password = Password,
		    };

			var resultUrls = await ProcessImageAsync();

			foreach (var resultUrl in resultUrls)
			{
				Console.WriteLine(resultUrl);
			}

			resultUrls = await RetryPolicyForSpecificErrorStatusCodeAsync();

			foreach (var resultUrl in resultUrls)
			{
				Console.WriteLine(resultUrl);
			}
		}

	    private static async Task<List<string>> ProcessImageAsync()
	    {
		    var imageParams = new ImageProcessingParams
		    {
			    ExportFormats = new[] { ExportFormat.Docx, ExportFormat.Txt, },
			    Language = "English,French",
			};

		    using (var fileStream = new FileStream(FilePath, FileMode.Open))
		    using (var client = new OcrClient(_authInfo))
			{
				var taskInfo = await client.ProcessImageAsync(
					imageParams,
					fileStream,
					Path.GetFileName(FilePath),
					waitTaskFinished: true);

				return taskInfo.ResultUrls;
			}
	    }

	    private static async Task<List<string>> RetryPolicyForSpecificErrorStatusCodeAsync()
	    {
		    int retryCount = 3;
			var millisecondsDelay = 3000;

			var imageParams = new ImageProcessingParams
			{
				ExportFormats = new[] { ExportFormat.Docx, ExportFormat.Txt, },
				Language = "English,French",
			};


			var policyBehaviourHttpClient =
			    new PolicyBehaviourHttpClientBuilder(_authInfo)
				    .AddRetryPolicyForSpecificErrorStatusCode(HttpStatusCode.BadGateway, retryCount, millisecondsDelay,
					    (exception, calculatedWaitDuration, retries, context) =>
					    {
						    Console.WriteLine($"Retry {retries} for policy with key {context.PolicyKey}");
					    })

				    .AddRetryPolicyWhenException(retryCount, millisecondsDelay,
					    (exception, calculatedWaitDuration, retries, context) =>
					    {
						    Console.WriteLine($"Retry {retries} for policy with key {context.PolicyKey} - exception: {exception.Message}");
					    })
					;

			using (var fileStream = new FileStream(FilePath, FileMode.Open))
			using (var client = new PolicyBehaviourOcrClient(policyBehaviourHttpClient.Build()))
			{
				var taskInfo = await client.ProcessImageAsync(
					imageParams,
					fileStream,
					Path.GetFileName(FilePath),
					waitTaskFinished: true);

				return taskInfo.ResultUrls;
			}
	    }
	}
}
