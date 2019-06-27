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
	/// Parameters for Text Field Processing request.
	/// </summary>
	public sealed class TextFieldProcessingParams
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
		/// Optional. Default "-1,-1,-1,-1". Specifies the region of the text field on the image. The coordinates of the region are measured in pixels relative to the left top corner of the image and are specified in the following order: left, top, right, bottom. By default, the region of the whole image is used.
		/// </summary>
		public string Region { get; set; }

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
		/// Optional. Default "". Specifies the letter set, which should be used during recognition. Contains a string with the letter set characters. For example, "ABCDabcd'-.". By default, the letter set of the language, specified in the <see cref="Language"/> parameter, is used.
		/// </summary>
		public string LetterSet { get; set; }

		/// <summary>
		/// Optional. Default "". Specifies the regular expression which defines which words are allowed in the field and which are not. By default, the set of allowed words is defined by the dictionary of the language, specified in the language parameter.
		/// </summary>
		/// <remarks>
		/// See the <see href="https://www.ocrsdk.com/documentation/specifications/regular-expressions/">description of regular expressions</see>.
		/// Note that regular expressions do not strictly limit the set of characters of the output result, i.e. the recognized value may contain characters which are not included into the regular expression. During recognition all hypotheses of a word recognition are checked against the specified regular expression. If a given recognition variant conforms to the expression, it has higher probability of being selected as final recognition output. But if there is no variant that matches regular expression, the result will not conform to the expression. If you want to limit the set of characters, which can be recognized, the best way to do it is to use letterSet parameter.
		/// </remarks>
		public string RegExp { get; set; }

		/// <summary>
		/// Optional. Default is <see cref="Enums.TextType.Normal"/>. Specifies the type of the text on a page.
		/// </summary>
		[JsonProperty("textType")]
		public TextType[] TextTypes { get; set; }

		/// <summary>
		/// Optional. Default "false". Specifies whether the field contains only one text line. The value should be true, if there is one text line in the field; otherwise it should be false.
		/// </summary>
		public bool? OneTextLine { get; set; }

		/// <summary>
		/// Optional. Default "false". Specifies whether the field contains only one word in each text line. The value should be true, if no text line contains more than one word (so the lines of text will be recognized as a single word); otherwise it should be false.
		/// </summary>
		public bool? OneWordPerTextLine { get; set; }

		/// <summary>
		/// Optional. Default is <see cref="Enums.MarkingType.SimpleText"/>. This property is valid only for the <see cref="Enums.TextType.Handprinted"/>> recognition. Specifies the type of marking around letters (for example, underline, frame, box, etc.). By default, there is no marking around letters.
		/// </summary>
		public MarkingType? MarkingType { get; set; }

		/// <summary>
		/// Optional. Default "1". Specifies the number of character cells for the field. This property has a sense only for the field marking types(the markingType parameter) that imply splitting the text in cells. Default value for this property is 1, but you should set the appropriate value to recognize the text correctly.
		/// </summary>
		public int? PlaceholdersCount { get; set; }

		/// <summary>
		/// Optional. Default "default". Provides additional information about handprinted letters writing style.
		/// </summary>
		public WritingStyle? WritingStyle { get; set; }
	}
}
