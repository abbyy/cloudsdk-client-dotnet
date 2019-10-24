using System;
using System.IO;
using System.Threading.Tasks;
using Abbyy.CloudSdk.V2.Client.Models;
using Abbyy.CloudSdk.V2.Client.Models.RequestParams;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Abbyy.CloudSdk.V2.Client.Tests.Tests
{
	[Parallelizable(ParallelScope.Children)]
	internal abstract class ApiClientTests : IDisposable
	{
		protected IOcrClient ApiClient;
		protected TestConfig Config;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			Config = new TestConfig();

			new ConfigurationBuilder()
				.AddJsonFile("testsettings.json")
				.Build()
				.GetSection(nameof(TestConfig))
				.Bind(Config);

			ApiClient = new OcrClient(
				new AuthInfo
				{
					Host = Config.Host,
					ApplicationId = Config.ApplicationId,
					Password = Config.Password,
				});
		}

		protected Task<TaskInfo> SubmitImageAsync(
			string fileName,
			Stream fileStream,
			Guid? taskId = null)
		{
			var parameters = taskId.HasValue
				? new ImageSubmittingParams {TaskId = taskId.Value}
				: null;

			return ApiClient.SubmitImageAsync(parameters, fileStream, fileName);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
				ApiClient?.Dispose();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
