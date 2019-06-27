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

using Abbyy.CloudSdk.Client.Models;
using Abbyy.CloudSdk.Client.Models.Enums;
using Abbyy.CloudSdk.Client.Models.RequestParams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Abbyy.CloudSdk.Client.Sample
{
	public class Program
	{
		private const string ApplicationId = "YOUR_APP_ID";
		private const string Password = "YOUR_APP_PWD";
		private const string FilePath = "YOUR_FILE_PATH";

		private static AuthInfo _authInfo;

	    public static async Task Main()
	    {
		    _authInfo = new AuthInfo
		    {
			    Host = "https://cloud-westus.ocrsdk.com",
			    ApplicationId = ApplicationId,
			    Password = Password,
		    };

			var resultUrls = await ProcessImageAsync();

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
	}
}
