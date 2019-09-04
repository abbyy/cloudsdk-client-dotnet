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
using System.Threading.Tasks;

namespace Abbyy.CloudSdk.V2.Client.Sample
{
	public class Program
	{
		private const string ApplicationId = @"PASTE_Application_ID";
		private const string Password = @"PAST_Application_Password";

		/// <summary>
		/// Processing Location URL https://www.ocrsdk.com/documentation/specifications/data-processing-location/
		/// </summary>
		private const string ServiceUrl = "https://cloud-eu.ocrsdk.com";

		private static AuthInfo _authInfo;

		public static async Task Main()
		{
			_authInfo = new AuthInfo
			{
				Host = ServiceUrl,
				ApplicationId = ApplicationId,
				Password = Password,
			};
			var ocrClient = new OcrClient(_authInfo);

			var resultUrls = await ProcessImageAsync(ocrClient);

			foreach (var resultUrl in resultUrls)
			{
				Console.WriteLine(resultUrl);
			}

			resultUrls = await ProcessDocumentAsync(ocrClient);

			foreach (var resultUrl in resultUrls)
			{
				Console.WriteLine(resultUrl);
			}
		}

		private static async Task<List<string>> ProcessImageAsync(IOcrClient ocrClient)
		{
			var imageParams = new ImageProcessingParams
			{
				ExportFormats = new[] { ExportFormat.Docx, ExportFormat.Txt, },
				Language = "English,French",
			};
			const string filePath = "New Image.jpg";

			using (var fileStream = new FileStream(filePath, FileMode.Open))
			{
				var taskInfo = await ocrClient.ProcessImageAsync(
					imageParams,
					fileStream,
					Path.GetFileName(filePath),
					waitTaskFinished: true);

				return taskInfo.ResultUrls;
			}
		}

		private static async Task<List<string>> ProcessDocumentAsync(IOcrClient ocrClient)
		{
			Guid taskId = await UploadFilesAsync(ocrClient);

			var processingParams = new DocumentProcessingParams
			{
				ExportFormats = new[] {ExportFormat.Docx, ExportFormat.Txt,},
				Language = "English,French",
				TaskId = taskId,
			};

			var taskInfo = await ocrClient.ProcessDocumentAsync(processingParams, waitTaskFinished: true);

			return taskInfo.ResultUrls;
		}

		private static async Task<Guid> UploadFilesAsync(IOcrClient ocrClient)
		{
			ImageSubmittingParams submitParams;
			string firstFilePath = "New Image.jpg";
			string secondFilePath = "Picture_003.jpg";

			// First file
			using (var fileStream = new FileStream(firstFilePath, FileMode.Open))
			{
				var submitImageResult = await ocrClient.SubmitImageAsync(
					null,
					fileStream,
					Path.GetFileName(firstFilePath));

				// Save TaskId for next files and ProcessDocument method
				submitParams = new ImageSubmittingParams { TaskId = submitImageResult.TaskId };
			}

			// Second file
			using (var fileStream = new FileStream(secondFilePath, FileMode.Open))
			{
				await ocrClient.SubmitImageAsync(
					submitParams,
					fileStream,
					Path.GetFileName(secondFilePath));
			}

			return submitParams.TaskId.Value;
		}
	}
}
