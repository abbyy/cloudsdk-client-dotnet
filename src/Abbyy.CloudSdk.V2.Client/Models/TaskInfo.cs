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
using System.Collections.Generic;
using Abbyy.CloudSdk.V2.Client.Models.Enums;

namespace Abbyy.CloudSdk.V2.Client.Models
{
	public sealed class TaskInfo
	{
		/// <summary>
		/// Task identifier
		/// </summary>
		public Guid TaskId { get; set; }

		/// <summary>
		/// Task creation time
		/// </summary>
		public DateTime RegistrationTime { get; set; }

		/// <summary>
		/// Last Task modification time
		/// </summary>
		public DateTime StatusChangeTime { get; set; }

		/// <summary>
		/// The task can have one of the following statuses
		/// </summary>
		public TaskStatus Status { get; set; }

		/// <summary>
		/// Description of the processing error. Specified only with
		/// ProcessingFailed Task status
		/// </summary>
		public string Error { get; set; }

		/// <summary>
		/// Number of files added to a Task
		/// </summary>
		public int FilesCount { get; set; }

		/// <summary>
		/// Recommended delay before request for new Task Status in milliseconds
		/// </summary>
		public int RequestStatusDelay { get; set; }

		/// <summary>
		/// The hyperlink collection with recognition results.
		/// The links have limited lifetime. If you want to download the
		/// result after the time has passed, call the
		/// <see cref="IOcrClient.GetTaskStatusAsync"/>
		/// or <see cref="IOcrClient.ListTasksAsync"/> method
		/// to obtain the new hyperlink collection.
		/// </summary>
		public List<string> ResultUrls { get; set; }

		/// <summary>
		/// Task description specified when the Task is created
		/// </summary>
		public string Description { get; set; }
	}
}
