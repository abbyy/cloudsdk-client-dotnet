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

namespace Abbyy.CloudSdk.V2.Client.Models.Enums
{
	/// <summary>
	/// Format of exporting recognized text.
	/// Several formats may be provided at once
	/// </summary>
	public enum ExportFormat
	{
		/// <summary>
		/// TXT
		/// </summary>
		Txt = 1,

		/// <summary>
		/// TXT. The exported file contains the text that was saved according to the order of the original blocks.
		/// </summary>
		TxtUnstructured = 2,
		
		/// <summary>
		/// RTF
		/// </summary>
		Rtf = 3,

		/// <summary>
		/// DOCX
		/// </summary>
		Docx = 4,

		/// <summary>
		/// XLSX
		/// </summary>
		Xlsx = 5,

		/// <summary>
		/// PPTX
		/// </summary>
		Pptx = 6,

		/// <summary>
		/// PDF. The entire image is saved as a picture, with recognized text put under the image.
		/// </summary>
		PdfSearchable = 7,

		/// <summary>
		/// PDF. The recognized text is saved as text, and the pictures are embedded as images.
		/// </summary>
		PdfTextAndImages = 8,

		/// <summary>
		/// PDF/A-1b. The file is saved in PDF/A-1b-compliant format, with the entire image saved
		/// as a picture and recognized text put under it.
		/// </summary>
		PdfA = 9,

		/// <summary>
		/// XML. All coordinates are saved relative to the original image.
		/// </summary>
		/// <remarks>
		/// See <see href="https://www.ocrsdk.com/documentation/specifications/xml-scheme-recognized-document/">
		/// Output XML Document</see> for the description of tags. If you select this export format, barcodes
		/// are recognized on the image and saved to output XML no matter which profile is used for recognition.
		/// </remarks>
		Xml = 10,

		/// <summary>
		/// XML. All coordinates are saved relative to the image after geometry correction.
		/// </summary>
		/// <remarks>
		/// See <see href="https://www.ocrsdk.com/documentation/specifications/xml-scheme-recognized-document/">
		/// Output XML Document</see> for the description of tags. If you select this export format, barcodes
		/// are recognized on the image and saved to output XML no matter which profile is used for recognition.
		/// </remarks>
		XmlForCorrectedImage = 11,

		/// <summary>
		/// ALTO
		/// </summary>
		Alto = 12,
	}
}
