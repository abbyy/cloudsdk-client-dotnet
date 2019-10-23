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
	/// Types of text supported by ABBYY Cloud OCR SDK.
	/// Several types may be provided at once
	/// </summary>
	/// <remarks>
	/// <see href="https://www.ocrsdk.com/documentation/specifications/text-types/"/>
	/// </remarks>
	public enum TextType
	{
		/// <summary>
		/// Common typographic type of text.
		/// </summary>
		Normal = 1,

		/// <summary>
		/// The text is typed on a typewriter.
		/// </summary>
		Typewriter = 2,

		/// <summary>
		/// The text is printed on a dot-matrix printer.
		/// </summary>
		Matrix = 3,

		/// <summary>
		/// A special set of characters including only
		/// digits written in ZIP-code style.
		/// </summary>
		Index = 4,

		/// <summary>
		/// Handprinted text.
		/// </summary>
		Handprinted = 5,

		/// <summary>
		/// A monospaced font, designed for Optical Character
		/// Recognition. Largely used by banks, credit card
		/// companies and similar businesses.
		/// </summary>
		OcrA = 6,

		/// <summary>
		/// A font designed for Optical Character Recognition.
		/// </summary>
		OcrB = 7,

		/// <summary>
		/// A special set of characters including only digits and
		/// A, B, C, D characters printed in magnetic ink.
		/// MICR (Magnetic Ink Character Recognition) characters
		/// are found in a variety of places, including personal
		/// checks.
		/// </summary>
		E13b = 8,

		/// <summary>
		/// A special set of characters, which includes only digits
		/// and A, B, C, D, E characters, written in MICR barcode
		/// font (CMC-7).
		/// </summary>
		Cmc7 = 9,

		/// <summary>
		/// Text printed in Gothic type.
		/// </summary>
		Gothic = 10,
	}
}
