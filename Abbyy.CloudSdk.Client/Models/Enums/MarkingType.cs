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

namespace Abbyy.CloudSdk.Client.Models.Enums
{
	/// <summary>
	/// Specifies the type of marking around letters
	/// </summary>
	/// <remarks>
	/// <see href="https://www.ocrsdk.com/documentation/specifications/field-marking/"/>
	/// </remarks>
	public enum MarkingType
	{
		/// <summary>
		/// This value denotes plain text.
		/// </summary>
		SimpleText = 1,

		/// <summary>
		/// This value specifies that the text is underlined.
		/// </summary>
		UnderlinedText = 2,

		/// <summary>
		/// This value specifies that the text is enclosed in a
		/// frame.
		/// </summary>
		TextInFrame = 3,

		/// <summary>
		/// This value specifies that the text is located in white
		/// fields on a gray background.
		/// </summary>
		GreyBoxes = 4,

		/// <summary>
		/// This value specifies that the field where the text is
		/// located is a set of separate boxes.
		/// </summary>
		CharBoxSeries = 5,

		/// <summary>
		/// This value specifies that the field where the text is
		/// located is a comb.
		/// </summary>
		SimpleComb = 6,

		/// <summary>
		/// This value specifies that the field where the text is
		/// located is a comb and that this comb is also the bottom
		/// line of a frame.
		/// </summary>
		CombInFrame = 7,

		/// <summary>
		/// This value specifies that the field where the text is
		/// located is a frame and this frame is split by vertical
		/// lines.
		/// </summary>
		PartitionedFrame = 8,
	}
}
