using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Abbyy.CloudSdk.V2.Client.Models;
using Polly;
using Polly.Timeout;

namespace Abbyy.CloudSdk.V2.Client.Sample.RetryPolicySample
{
	public class PolicyBehaviourHttpClientBuilder
	{
        private readonly AuthInfo _authInfo;
		private List<IAsyncPolicy> retryPolicies = new List<IAsyncPolicy>();

		private TimeSpan _maxDelayForPolicy = TimeSpan.FromMilliseconds(0);

		public PolicyBehaviourHttpClientBuilder(AuthInfo authInfo)
		{
			this._authInfo = authInfo ?? throw new ArgumentNullException(nameof(authInfo));
		}

		/// <summary>
		/// Adding a retry policy in cases where the OrcHttpClient returned an ApiException error with specific error code
		/// </summary>
		/// <param name="statusErrorCode"> Specific Http status error code </param>
		/// <param name="retryCount"> Retry count </param>
		/// <param name="millisecondsDelay"> The duration to wait for for a particular retry attempt in milliseconds </param>
		/// <param name="onRetry"> The action to call on each retry </param>
		/// <returns></returns>
		public PolicyBehaviourHttpClientBuilder AddRetryPolicyForSpecificErrorStatusCode(
			HttpStatusCode statusErrorCode,
			int retryCount,
			int millisecondsDelay,
			Action<Exception, TimeSpan, int, Context> onRetry = null)
		{
			if (onRetry == null)
			{
				onRetry = (Action<Exception, TimeSpan, int, Context>) ((_, __, ___, ____) => { });
			}

			var retryPolicy =
				Polly.Policy
					.Handle<ApiException>(result => result.StatusCode == (int)statusErrorCode)
					.WaitAndRetryAsync(
						retryCount,
						sleepDurationProvider => TimeSpan.FromMilliseconds(millisecondsDelay),
						onRetry: onRetry
					)
					.WithPolicyKey($"WaitAndRetryAsync_For_{statusErrorCode}_StatusCode");
			retryPolicies.Add(retryPolicy);

			AccountingTimeDelayForPolicies(retryCount, millisecondsDelay);

			return this;
		}



		/// <summary>
		/// Adding a retry policy in cases where the OrcHttpClient threw an Exception
		/// </summary>
		/// <param name="retryCount"> Retry count </param>
		/// <param name="millisecondsDelay"> The duration to wait for for a particular retry attempt in milliseconds </param>
		/// <param name="onRetry"> The action to call on each retry </param>
		/// <returns></returns>
		public PolicyBehaviourHttpClientBuilder AddRetryPolicyWhenException(
			int retryCount,
			int millisecondsDelay,
			Action<Exception, TimeSpan, int, Context> onRetry = null)
		{
			if (onRetry == null)
			{
				onRetry = (Action<Exception, TimeSpan, int, Context>)((_, __, ___, ____) => { });
			}

			var retryPolicy =
				Polly.Policy
					.Handle<Exception>()
					.WaitAndRetryAsync(
						retryCount,
						sleepDurationProvider => TimeSpan.FromMilliseconds(millisecondsDelay),
						onRetry: onRetry
					)
					.WithPolicyKey($"WaitAndRetryAsync_For_Exception"); ;

			retryPolicies.Add(retryPolicy);

			AccountingTimeDelayForPolicies(retryCount, millisecondsDelay);

			return this;
		}

		/// <summary>
		/// Build HttpClient with PolicyBehaviour HttpClientHandler
		/// </summary>
		/// <returns></returns>
		public HttpClient Build()
		{
			var handler = new PolicyBehaviourHttpClientHandler(_authInfo, policies: retryPolicies?.ToArray());

			var httpClient = new HttpClient(handler)
			{
				BaseAddress = new Uri(_authInfo.Host),
			};

			// Here it is necessary to take into account the maximum delay time of the policies,
			// so that the request does not expire on timeout earlier than it should

			httpClient.Timeout = httpClient.Timeout + _maxDelayForPolicy;

			return httpClient;
		}


		private void AccountingTimeDelayForPolicies(int retryCount, int millisecondsDelay)
		{
			var delayPolicy = TimeSpan.FromMilliseconds(millisecondsDelay * retryCount);
			_maxDelayForPolicy = _maxDelayForPolicy >= delayPolicy ? _maxDelayForPolicy : delayPolicy;

		}
	}
}
