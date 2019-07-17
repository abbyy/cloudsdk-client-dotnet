using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Abbyy.CloudSdk.V2.Client.Models;
using Polly;

namespace Abbyy.CloudSdk.V2.Client.Sample.RetryPolicySample
{
	public class PolicyBehaviourHttpClientHandler : HttpClientHandler
	{
		private readonly IAsyncPolicy _policyWrapper;

		public PolicyBehaviourHttpClientHandler(AuthInfo authInfo, IAsyncPolicy[] policies) : base()
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
                this._policyWrapper != null
	                ? await this._policyWrapper.ExecuteAsync(async () => await this.CallBaseSendAsync(request, ct))
					: await this.CallBaseSendAsync(request, ct);
		}

		protected virtual async Task<HttpResponseMessage> CallBaseSendAsync(
			HttpRequestMessage request,
			CancellationToken ct)
		{
			return await base.SendAsync(request, ct);
		}

		private IAsyncPolicy WrapOrSingleAsync(IAsyncPolicy[] policies)
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
