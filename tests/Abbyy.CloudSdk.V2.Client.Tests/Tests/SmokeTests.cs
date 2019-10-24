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
			var parameters = new ImageProcessingParams
			{
				Language = "English",
				ExportFormats = new[]
				{
					ExportFormat.Docx,
				},
			};

			TaskInfo processImageTask;
			using (var fileStream = File.Open(TestFile.Article, FileMode.Open, FileAccess.Read))
			{
				processImageTask = await ApiClient.ProcessImageAsync(
					parameters,
					fileStream,
					TestFile.Article,
					true
				);
			}

			processImageTask.ShouldNotBeNull();
			processImageTask.TaskId.ShouldNotBe(Guid.Empty);
			processImageTask.Status.ShouldBe(TaskStatus.Completed);
			processImageTask.ResultUrls.Count.ShouldBe(1);
		}

		[Test]
		public async Task SubmitImage_ShouldBeOk()
		{
			var first = await SubmitImageAsync(TestFile.Article);
			first.FilesCount.ShouldBe(1);

			var second = await SubmitImageAsync(TestFile.Questionnaire, first.TaskId);
			second.FilesCount.ShouldBe(2);
		}

		[Test]
		public async Task ProcessDocument_ShouldBeOk()
		{
			var submitImageTask = await SubmitImageAsync(TestFile.Article);
			var parameters = new DocumentProcessingParams
			{
				TaskId = submitImageTask.TaskId,
				Language = "English",
				ExportFormats = new[]
				{
					ExportFormat.PdfSearchable,
				},
			};

			var processDocumentTask = await ApiClient.ProcessDocumentAsync(
				parameters,
				true
			);

			processDocumentTask.ShouldNotBeNull();
			processDocumentTask.TaskId.ShouldBe(submitImageTask.TaskId);
			processDocumentTask.Status.ShouldBe(TaskStatus.Completed);
			processDocumentTask.ResultUrls.Count.ShouldBe(1);
		}

		[Test]
		public async Task ProcessBusinessCard_ShouldBeOk()
		{
			var parameters = new BusinessCardProcessingParams
			{
				Language = "English",
				ExportFormats = BusinessCardExportFormat.Xml,
			};

			TaskInfo processBusinessCardTask;
			using (var fileStream = File.Open(TestFile.BusinessCard, FileMode.Open, FileAccess.Read))
			{
				processBusinessCardTask = await ApiClient.ProcessBusinessCardAsync(
					parameters,
					fileStream,
					TestFile.BusinessCard,
					true
				);
			}

			processBusinessCardTask.ShouldNotBeNull();
			processBusinessCardTask.TaskId.ShouldNotBe(Guid.Empty);
			processBusinessCardTask.Status.ShouldBe(TaskStatus.Completed);
			processBusinessCardTask.ResultUrls.Count.ShouldBe(1);
		}

		[Test]
		public async Task ProcessTextField_ShouldBeOk()
		{
			var parameters = new TextFieldProcessingParams
			{
				Language = "English",
				Region = "2000,2700,2600,2800",
			};

			TaskInfo processTextFieldTask;
			using (var fileStream = File.Open(TestFile.BusinessCard, FileMode.Open, FileAccess.Read))
			{
				processTextFieldTask = await ApiClient.ProcessTextFieldAsync(
					parameters,
					fileStream,
					TestFile.BusinessCard,
					true
				);
			}

			processTextFieldTask.ShouldNotBeNull();
			processTextFieldTask.TaskId.ShouldNotBe(Guid.Empty);
			processTextFieldTask.Status.ShouldBe(TaskStatus.Completed);
			processTextFieldTask.ResultUrls.Count.ShouldBe(1);
		}

		private async Task<TaskInfo> SubmitImageAsync(string fileName, Guid? taskId = null)
		{
			var parameters = taskId.HasValue
				? new ImageSubmittingParams {TaskId = taskId.Value}
				: null;

			TaskInfo submitImageTask;
			using (var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read))
			{
				submitImageTask = await ApiClient.SubmitImageAsync(
					parameters,
					fileStream,
					fileName
				);
			}

			submitImageTask.ShouldNotBeNull();
			submitImageTask.TaskId.ShouldNotBe(Guid.Empty);
			submitImageTask.Status.ShouldBe(TaskStatus.Submitted);

			return submitImageTask;
		}
	}
}
