using System;
using System.Net.Http;
using Abbyy.CloudSdk.V2.Client.Models;

namespace Abbyy.CloudSdk.V2.Client.Sample.RetryPolicySample
{
	public class HttpClientWithRetry : HttpClient
	{
		public HttpClientWithRetry(AuthInfo authInfo, HttpClientHandler httpClientHandler, int delayForRetry) : base(httpClientHandler)
		{
			if (authInfo == null) throw new ArgumentNullException(nameof(authInfo));

			BaseAddress = new Uri(authInfo.Host);
			this.Timeout = this.Timeout + TimeSpan.FromMilliseconds(delayForRetry);
		}
	}
}
