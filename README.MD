# Cloud OCR SDK 

This .NET library is intended to simplify the development of image recognition solutions with ABBYY Cloud OCR SDK API. This repo contains the client library and the sample enabling simple access to high-quality recognition technologies.
Using this library, you can supplement your C# code with ready-to-use solutions of images recognition with following export to the most popular text formats, such as searchable PDF, TXT, DOCX.


For more information about the product visit [ABBYY Cloud OCR SDK website](https://www.ocrsdk.com/).

## Features

- Text recognition
  - full-page and zonal OCR (printed text) for 200+ languages
  - ICR (hand-printed text)
- Document conversion
  - convert image/PDF to searchable PDF, PDF/A and Microsoft Word, Excel, PowerPoint
- Data extraction
  - Receipt recognition
  - Barcode recognition 
  - Business card recognition
  - ID recognition (MRZ)
- .Net Framework и .Net Core support
  - integrate this library into .Net, ASP.NET or other applications



## Quick start guide

To use the library in your project for image processing, do the following:

1. [Register](https://cloud.ocrsdk.com/Account/Register) on the ABBYY Cloud OCR SDK website and create your Application. You will provide the Application ID and Application Password as authentication information to initialize the top hierarchy object of the library and to get access to the API.
2. Download the library from this repo, if you intend to modify it, or simply install the package from the [NuGet.org](https://www.nuget.org/packages/Abbyy.CloudSdk.V2.Client/).
3. Implement your application using ABBYY OCR and capturing technologies.

## Sample
The sample demonstrates how to process image with the specified parameters and export the result using this client library. Start your developement with the following steps. 

Provide your authentication info:
```
var authInfo = new AuthInfo
         {
             Host = "https://cloud-westus.ocrsdk.com",
             ApplicationId = ApplicationId,
             Password = Password,
         };
```

Set the processing and result options:
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

Create the main OcrClient object:
```
var client = new OcrClient(authInfo))
```

Pass the parameters to a new task. Set `waitTaskFinished` flag to **true** to wait for the results in current method. If `waitTaskFinished` is false, the task will be processed asynchronous.
```
var taskInfo = await client.ProcessImageAsync(
    imageParams,
    fileStream,
    Path.GetFileName(FilePath),
    waitTaskFinished: true);
```

Display the result:
```
foreach (var resultUrl in taskInfo.ResultUrls)
{
    Console.WriteLine(resultUrl);
}
```

Investigate the whole sample in [Sample folder](https://).

## Supported export formats

You can export recognized text to the following formats:
- TXT
- RTF
- DOCX
- XLSX
- PPTX
- PDF
- PDF/A-1b
- XML
- ALTO
- vCard
- CSV


## Supported text types

ABBYY Cloud OCR SDK detects on the image the following types of text:
- normal
- typewriter
- matrix
- index
- handprinted
- ocrA
- ocrB
- e13b
- cmc7
- gothic



## Documentation
Find the full API description in the [Documentation](https://www.ocrsdk.com/documentation/) section.

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
