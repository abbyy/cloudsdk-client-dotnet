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

namespace Abbyy.CloudSdk.V2.Client.Models.RequestParams
{
	/// <summary>
	/// Parameters for Tasks Listing request
	/// </summary>
	public sealed class TasksListingParams
	{
		/// <summary>
		/// Optional. Default is the current date minus 7 days. Specifies the date to list tasks from.
		/// </summary>
		public DateTime? FromDate { get; set; }

		/// <summary>
		/// Optional. Default is the current date. Specifies the date to list tasks to. 
		/// </summary>
		public DateTime? ToDate { get; set; }

		/// <summary>
		/// Optional. Default is "false". Specifies if the tasks that have already been deleted must be excluded from the listing.
		/// </summary>
		public bool? ExcludeDeleted { get; set; }
	}
}
