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
using Abbyy.CloudSdk.Client.Models;

namespace Abbyy.CloudSdk.Client
{
	/// <summary>
	/// Describes the API related error
	/// </summary>
	public sealed class ApiException : Exception
    {
		/// <summary>
		/// HTTP Status code returned by a server
		/// </summary>
	    public int StatusCode { get; }

		/// <summary>
		/// Details about an error
		/// </summary>
	    public Error Error { get; }

		/// <summary>
		/// Response headers
		/// </summary>
	    public IDictionary<string, IEnumerable<string>> Headers { get; }

		/// <summary>
		/// Instantiates the <see cref="ApiException"/>
		/// </summary>
		/// <param name="message">Exception message</param>
		/// <param name="statusCode">HTTP Status code returned by a server</param>
		/// <param name="error">Details about an error</param>
		/// <param name="headers">Response headers</param>
		/// <param name="exception">Actual exception</param>
		public ApiException(
		    string message,
		    int statusCode,
			Error error,
		    IDictionary<string, IEnumerable<string>> headers,
		    Exception exception = null)
			: base (message, exception)
	    {
		    StatusCode = statusCode;
		    Error = error;
		    Headers = headers;
		}
	}
}
