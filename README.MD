# Cloud OCR SDK 

This .NET library is intended to simplify the development of image recognition solutions with ABBYY Cloud OCR SDK API. This repo contains the client library and the sample enabling simple access to high-quality recognition technologies. 
For more information about the product visit [ABBYY Cloud OCR SDK website](https://www.ocrsdk.com/).

## Quick start guide

To use the library in your project, do the following:

1. [Register](https://cloud.ocrsdk.com/Account/Register) on the ABBYY Cloud OCR SDK website and create your Application. You will provide the Application ID and Application Password as authentication information to initialize the top hierarchy object of the library and to get access to the API.
2. Install the package from the [NuGet.org](https://www.nuget.org/packages/Abbyy.CloudSdk.Client/) and it will update automatically. If you intend to modify the API, you can download the library from this repo.
3. Implement your application using ABBYY OCR and capturing technologies.

## Documentation
Find the full API description in the [Documentation](https://www.ocrsdk.com/documentation/api-reference/) section.

## Sample
The sample demonstrates how to process image with the specified parameters and export the result to TXT and DOCX using the ABBYY Cloud OCR SDK client library. Start your developement with the following steps. 

Provide your authentication info and the HTTP server URL address, used for API calls (see [Data processing location](https://www.ocrsdk.com/documentation/specifications/data-processing-location/) for details):
```
var authInfo = new AuthInfo
         {
             Host = "https://<PROCESSING_LOCATION_ID>.ocrsdk.com",
             ApplicationId = ApplicationId,
             Password = Password,
         };
```

Create the main OcrClient object:
```
var client = new OcrClient(authInfo))
```

Set the processing and result options (you can find all the settings [here](https://www.ocrsdk.com/documentation/api-reference/process-image-method/):
```
var imageParams = new ImageProcessingParams
        {
            ExportFormats = new[] { ExportFormat.Docx, ExportFormat.Txt, },
            Language = "English,French",
        };
```

Open file as stream:
```
var fileStream = new FileStream(FilePath, FileMode.Open))
```



Pass the parameters to a new task. Set `waitTaskFinished` flag to **true** to wait for the results in current method. If `waitTaskFinished` is **false**, the method will not wait for the task finish.
```
var taskInfo = await client.ProcessImageAsync(
    imageParams,
    fileStream,
    Path.GetFileName(FilePath),
    waitTaskFinished: true);
```

Display the URL addresses of the result documents:
```
foreach (var resultUrl in taskInfo.ResultUrls)
{
    Console.WriteLine(resultUrl);
}
```

Investigate the whole sample in [Sample folder](Abbyy.CloudSdk.Client.Sample).

## License
Copyright © 2019 ABBYY Production LLC

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
