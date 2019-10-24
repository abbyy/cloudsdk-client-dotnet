using System;
using System.IO;
using System.Threading.Tasks;
using Abbyy.CloudSdk.V2.Client.Models;
using Abbyy.CloudSdk.V2.Client.Models.Enums;
using Abbyy.CloudSdk.V2.Client.Models.RequestParams;
using NUnit.Framework;
using Shouldly;
using TaskStatus = Abbyy.CloudSdk.V2.Client.Models.Enums.TaskStatus;

namespace Abbyy.CloudSdk.V2.Client.Tests.Tests
{
	[TestFixture]
	internal sealed class SmokeTests : ApiClientTests
	{
		[Test]
		public async Task ProcessImage_ShouldBeOk()
		{
			var fileName = TestFile.Article;

			var parameters = new ImageProcessingParams
			{
				Language = "English",
				ExportFormats = new[]
				{
					ExportFormat.Docx,
				},
			};

			TaskInfo processImageTask;
			using (var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read))
			{
				processImageTask = await ApiClient.ProcessImageAsync(
					parameters,
					fileStream,
					fileName,
					true
				);
			}

			processImageTask.ShouldNotBeNull();
			processImageTask.TaskId.ShouldNotBe(Guid.Empty);
			processImageTask.Status.ShouldBe(TaskStatus.Completed);
			processImageTask.ResultUrls.Count.ShouldBe(1);
		}
	}
}
