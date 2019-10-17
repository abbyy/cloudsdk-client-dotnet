using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace Abbyy.CloudSdk.V2.Client.Sample
{
	public class HttpClientRetryPolicyHandler : DelegatingHandler
	{
		private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;

		/// <summary>Initializes a new instance of the <see cref="HttpClientRetryPolicyHandler"/> class.</summary>
		public HttpClientRetryPolicyHandler(IAsyncPolicy<HttpResponseMessage> retryPolicy)
		{
			_retryPolicy = retryPolicy ?? throw new ArgumentNullException(nameof(retryPolicy));
		}

		/// <inheritdoc />
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellation) =>
			_retryPolicy.ExecuteAsync(ct => base.SendAsync(request, ct), cancellation);
	}
}
