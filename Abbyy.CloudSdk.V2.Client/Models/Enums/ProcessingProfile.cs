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
	/// ABBYY Cloud OCR SDK provides a set of processing profiles which are designed for the main usage
	/// scenarios. Choose the profile which fits your situation, because selecting the wrong profile can
	/// significantly reduce processing quality.
	/// </summary>
	public enum ProcessingProfile
	{
		/// <summary>
		/// Suitable for converting documents into an editable format such as RTF or DOCX. This profile is
		/// focused on retaining the original document structure and appearance, including font styles,
		/// pictures, background color, etc.
		/// </summary>
		DocumentConversion = 1,

		/// <summary>
		/// Suitable for creating a digital archive of searchable documents. This profile is not intended for
		/// converting documents into an editable format. It is best optimized for creating searchable PDFs
		/// (with the recognized text saved under the image). All possible text is found and recognized on the
		/// input image, including text embedded into pictures.
		/// </summary>
		DocumentArchiving = 2,

		/// <summary>
		/// Suitable for extracting all text from the input image, including small text areas of low quality.
		/// The document appearance and structure are ignored, pictures and tables are not detected.
		/// </summary>
		/// <remarks>
		/// This profile is not intended for converting documents into an editable format. It is designed for
		/// the situations when you need to retrieve the data from the image for some further processing on
		/// your side, such as extracting data from bills, receipts or invoices for use in accounting systems.
		/// Consider using XML export format, which allows you to access the recognized text coordinates.
		/// </remarks>
		TextExtraction = 3,

		/// <summary>
		/// Suitable for barcode extraction. Extracts only barcodes (texts, pictures, or tables are not detected)
		/// </summary>
		BarcodeRecognition = 4,
	}
}
