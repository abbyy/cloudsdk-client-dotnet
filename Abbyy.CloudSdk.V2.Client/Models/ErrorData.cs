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

namespace Abbyy.CloudSdk.V2.Client.Models
{
	/// <summary>
	/// Describes the error details
	/// </summary>
	public sealed class ErrorData
	{
		/// <summary>
		/// The code of the error
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// The message describing the error
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Optional. The description of where the error occurred
		/// </summary>
		public string Target { get; set; }

		/// <summary>
		/// Optional. The validation error description
		/// </summary>
		public ErrorData[] Details { get; set; }
	}
}
