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
	/// Parameters for Business Card Processing request
	/// </summary>
	public sealed class BusinessCardProcessingParams
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
		/// Optional. Default is <see cref="Enums.BusinessCardExportFormat.VCard"/>. Specifies the export format.
		/// </summary>
		[JsonProperty("exportFormat")]
		public BusinessCardExportFormat? ExportFormats { get; set; }

		/// <summary>
		/// Optional. Default "English". Specifies recognition language of the document.
		/// This parameter can contain several language names separated with commas, for example
		/// "English,French,German".
		/// </summary>
		/// <remarks>
		/// <see href="https://www.ocrsdk.com/documentation/specifications/recognition-languages/"/>
		/// </remarks>
		public string Language { get; set; }

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
		/// Optional. Default "false". Specifies whether the additional information
		/// on the recognized characters (e.g. whether the character is recognized
		/// uncertainly) should be written to an output file in XML format. This
		/// parameter can be used only if the <see cref="ExportFormat"/> parameter
		/// is set to <see cref="Enums.ExportFormat.Xml"/>. 
		/// </summary>
		[JsonProperty(PropertyName = "xml:writeExtendedCharacterInfo")]
		public bool? WriteExtendedCharacterInfo { get; set; }

		/// <summary>
		/// Optional. Default "false". Specifies whether the field components should
		/// be written to an output file in XML format. For example, for the Name
		/// field the components can include first name and last name, returned
		/// separately.This parameter can be used only if the <see cref="ExportFormat"/>
		/// parameter is set to <see cref="Enums.ExportFormat.Xml"/>. 
		/// </summary>
		[JsonProperty(PropertyName = "xml:writeFieldComponents")]
		public bool? WriteFieldComponents { get; set; }
	}
}
