using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Abbyy.CloudSdk.V2.Client.Models;
using Polly;

namespace Abbyy.CloudSdk.V2.Client.Sample.RetryPolicySample
{
	public class HttpClientHandlerWithRetry : HttpClientHandler
	{
		private readonly IAsyncPolicy<HttpResponseMessage> _policyWrapper;

		public HttpClientHandlerWithRetry(AuthInfo authInfo, IAsyncPolicy<HttpResponseMessage>[] policies)
		{
			if (authInfo == null) throw new ArgumentNullException(nameof(authInfo));

			Credentials = new NetworkCredential(authInfo.ApplicationId, authInfo.Password);
			PreAuthenticate = true;

			_policyWrapper = WrapOrSingleAsync(policies);
		}

		protected override async Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request,
			CancellationToken ct)
		{
			return
				_policyWrapper != null
					? await _policyWrapper.ExecuteAsync(async () => await base.SendAsync(request, ct))
					: await base.SendAsync(request, ct);
		}

		private IAsyncPolicy<HttpResponseMessage> WrapOrSingleAsync(IAsyncPolicy<HttpResponseMessage>[] policies)
		{
			switch (policies.Length)
			{
				case 0:
					return null;
				case 1:
					return policies[0];
				default:
					return Policy.WrapAsync(policies);
			}
		}
	}
}
