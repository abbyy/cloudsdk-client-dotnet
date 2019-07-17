using System.Net.Http;

namespace Abbyy.CloudSdk.V2.Client.Sample.RetryPolicySample
{
	public class PolicyBehaviourOcrClient : OcrClient
	{
		public PolicyBehaviourOcrClient(HttpClient httpClient) : base(httpClient)
		{

		}
	}
}
