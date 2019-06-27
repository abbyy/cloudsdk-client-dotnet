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
	/// Specifies whether the result must be written as tagged PDF.
	/// </summary>
	public enum WriteTags
	{
		/// <summary>
		/// Automatic selection: the tags are written into the output PDF file
		/// if it must comply with PDF/A-1a standard, and are not written otherwise.
		/// </summary>
		Auto = 0,

		/// <summary>
		/// Write tags
		/// </summary>
		Write = 1,

		/// <summary>
		/// Don't write tags
		/// </summary>
		DontWrite = 2,
    }
}
