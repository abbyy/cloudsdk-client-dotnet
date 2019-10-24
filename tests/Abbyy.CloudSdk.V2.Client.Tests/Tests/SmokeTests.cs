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
			using (var fileStream = GetResourceFileStream(TestFile.Article))
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
			using (var fileStream = GetResourceFileStream(TestFile.BusinessCard))
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
			using (var fileStream = GetResourceFileStream(TestFile.BusinessCard))
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

		[Test]
		public async Task ProcessBarcodeField_ShouldBeOk()
		{
			var parameters = new BarcodeFieldProcessingParams
			{
				BarcodeTypes = new[]
				{
					BarcodeType.Ean13,
				},
				Region = "1800,3200,2250,3400",
			};

			TaskInfo processBarcodeFieldTask;
			using (var fileStream = GetResourceFileStream(TestFile.Questionnaire))
			{
				processBarcodeFieldTask = await ApiClient.ProcessBarcodeFieldAsync(
					parameters,
					fileStream,
					TestFile.Questionnaire,
					true
				);
			}

			processBarcodeFieldTask.ShouldNotBeNull();
			processBarcodeFieldTask.TaskId.ShouldNotBe(Guid.Empty);
			processBarcodeFieldTask.Status.ShouldBe(TaskStatus.Completed);
			processBarcodeFieldTask.ResultUrls.Count.ShouldBe(1);
		}

		[Test]
		public async Task ProcessCheckmarkField_ShouldBeOk()
		{
			var parameters = new CheckmarkFieldProcessingParams
			{
				CheckmarkType = CheckmarkType.Square,
				Region = "700,930,800,1030",
			};

			TaskInfo processCheckmarkFieldTask;
			using (var fileStream = GetResourceFileStream(TestFile.Questionnaire))
			{
				processCheckmarkFieldTask = await ApiClient.ProcessCheckmarkFieldAsync(
					parameters,
					fileStream,
					TestFile.Questionnaire,
					true
				);
			}

			processCheckmarkFieldTask.ShouldNotBeNull();
			processCheckmarkFieldTask.TaskId.ShouldNotBe(Guid.Empty);
			processCheckmarkFieldTask.Status.ShouldBe(TaskStatus.Completed);
			processCheckmarkFieldTask.ResultUrls.Count.ShouldBe(1);
		}

		[Test]
		public async Task ProcessFields_ShouldBeOk()
		{
			var submitImageTask = await SubmitImageAsync(TestFile.Questionnaire);
			var parameters = new FieldsProcessingParams
			{
				TaskId = submitImageTask.TaskId,
				WriteRecognitionVariants = true,
			};

			TaskInfo processFieldsTask;
			using (var fileStream = GetResourceFileStream(TestFile.ProcessFieldsXmlConfig))
			{
				processFieldsTask = await ApiClient.ProcessFieldsAsync(
					parameters,
					fileStream,
					TestFile.ProcessFieldsXmlConfig,
					true
				);
			}

			processFieldsTask.ShouldNotBeNull();
			processFieldsTask.TaskId.ShouldBe(submitImageTask.TaskId);
			processFieldsTask.Status.ShouldBe(TaskStatus.Completed);
			processFieldsTask.ResultUrls.Count.ShouldBe(1);
		}

		[Test]
		public async Task ProcessMrz_ShouldBeOk()
		{
			var parameters = new MrzProcessingParams
			{
				Description = "Task description: Mrz processing.",
			};

			TaskInfo processMrzTask;
			using (var fileStream = GetResourceFileStream(TestFile.ProcessMrz))
			{
				processMrzTask = await ApiClient.ProcessMrzAsync(
					parameters,
					fileStream,
					TestFile.ProcessMrz,
					true
				);
			}

			processMrzTask.ShouldNotBeNull();
			processMrzTask.TaskId.ShouldNotBe(Guid.Empty);
			processMrzTask.Status.ShouldBe(TaskStatus.Completed);
			processMrzTask.ResultUrls.Count.ShouldBe(1);
		}

		[Test]
		public async Task ProcessReceipt_ShouldBeOk()
		{
			var parameters = new ReceiptProcessingParams
			{
				Countries = new []
				{
					ReceiptRecognizingCountry.Russia,
				}
			};

			TaskInfo processReceiptTask;
			using (var fileStream = GetResourceFileStream(TestFile.Article))
			{
				processReceiptTask = await ApiClient.ProcessReceiptAsync(
					parameters,
					fileStream,
					TestFile.Article,
					true
				);
			}

			processReceiptTask.ShouldNotBeNull();
			processReceiptTask.TaskId.ShouldNotBe(Guid.Empty);
			processReceiptTask.Status.ShouldBe(TaskStatus.Completed);
			processReceiptTask.ResultUrls.Count.ShouldBe(1);
		}

		private async Task<TaskInfo> SubmitImageAsync(string fileName, Guid? taskId = null)
		{
			var parameters = taskId.HasValue
				? new ImageSubmittingParams {TaskId = taskId.Value}
				: null;

			TaskInfo submitImageTask;
			using (var fileStream = GetResourceFileStream(fileName))
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

		private static FileStream GetResourceFileStream(string fileName)
		{
			return File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
		}
	}
}
