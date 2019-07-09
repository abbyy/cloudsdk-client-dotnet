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

using System;
using Abbyy.CloudSdk.V2.Client.Models.Enums;

namespace Abbyy.CloudSdk.V2.Client.Models
{
	/// <summary>
	/// Describes the Application
	/// </summary>
	public sealed class Application
	{
		/// <summary>
		/// Application Id
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// The number of pages remained for processing
		/// </summary>
		public int? Pages { get; set; }

		/// <summary>
		/// The number of fields remained for processing
		/// </summary>
		public int? Fields { get; set; }

		/// <summary>
		/// Pages expiration date
		/// </summary>
		public DateTime Expires { get; set; }

		/// <summary>
		/// Application type
		/// </summary>
		public ApplicationType Type { get; set; }
	}
}
