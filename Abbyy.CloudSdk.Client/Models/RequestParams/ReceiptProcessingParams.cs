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

using Abbyy.CloudSdk.Client.Models.Enums;
using Newtonsoft.Json;

namespace Abbyy.CloudSdk.Client.Models.RequestParams
{
	/// <summary>
	/// Parameters for Receipt Processing request
	/// </summary>
	public sealed class ReceiptProcessingParams
	{
		/// <summary>
		/// Optional. Contains a password for accessing password-protected images in PDF format.
		/// </summary>
		public string PdfPassword { get; set; }

		/// <summary>
		/// Optional. Contains the description of the processing task. Cannot contain more than 255 characters.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Optional. Default is <see cref="Enums.ImageSource.Auto"/>. Specifies the source of the image.
		/// </summary>
		public ImageSource? ImageSource { get; set; }

		/// <summary>
		/// Optional. Default "true". Specifies whether the orientation of the image should be automatically detected and corrected.
		/// </summary>
		/// <list type="bullet">
		/// <item>
		/// <term>true</term>
		/// <description>The page orientation is automatically detected, and if it differs from normal the image is rotated.</description>
		/// </item>
		/// <item>
		/// <term>false</term>
		/// <description>The page orientation detection and correction is not performed.</description>
		/// </item>
		/// </list>
		public bool? CorrectOrientation { get; set; }

		/// <summary>
		/// Optional. Default "true". Specifies whether the skew of the image should be automatically detected and corrected.
		/// </summary>
		public bool? CorrectSkew { get; set; }

		/// <summary>
		/// Optional. Default is <see cref="Enums.ReceiptRecognizingCountry.Usa"/>. Important! The technology fully supports the receipts from USA and France, other countries are currently supported in beta mode. Specifies the country where the receipt was printed. This parameter can contain several names of countries.
		/// </summary>
		[JsonProperty("country")]
		public ReceiptRecognizingCountry[] Countries { get; set; }

		/// <summary>
		/// Optional. Default "false". Specifies whether the additional information on the recognized characters (e.g. whether the character is recognized uncertainly) should be written to an output file in XML format.
		/// </summary>
		[JsonProperty(PropertyName = "xml:writeExtendedCharacterInfo")]
		public bool? WriteExtendedCharacterInfo { get; set; }

		/// <summary>
		/// Optional. Default is <see cref="Enums.FieldRegionExportMode.DoNotExport"/>. Specifies if the coordinates of field regions should be saved to the resulting XML file, and how the coordinates should be specified: on the original or on the corrected image.
		/// </summary>
		[JsonProperty(PropertyName = "xml:fieldRegionExportMode")]
		public FieldRegionExportMode? FieldRegionExportMode { get; set; }
	}
}
