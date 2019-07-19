using System.Net.Http;

namespace Abbyy.CloudSdk.V2.Client.Sample.RetryPolicySample
{
	public class OcrClientWithRetry : OcrClient
	{
		public OcrClientWithRetry(HttpClient httpClient) : base(httpClient)
		{
		}
	}
}
