using System;
using System.IO;
using System.Linq;
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
			// Arrange
			var parameters = new ImageProcessingParams
			{
				Language = "English",
				ExportFormats = new[]
				{
					ExportFormat.Docx,
				},
			};

			// Act
			TaskInfo processImageTask;
			using (var fileStream = GetResourceFileStream(TestFile.Image))
			{
				processImageTask = await ApiClient.ProcessImageAsync(
					parameters,
					fileStream,
					TestFile.Image,
					true
				);
			}

			// Assert
			CheckResultTask(processImageTask);
		}

		[Test]
		public async Task SubmitImage_ShouldBeOk()
		{
			// Arrange and Act
			var first = await SubmitImageAsync(TestFile.Image);
			var second = await SubmitImageAsync(TestFile.Fields, first.TaskId);

			// Assert
			first.FilesCount.ShouldBe(1);
			second.FilesCount.ShouldBe(2);
		}

		[Test]
		public async Task ProcessDocument_ShouldBeOk()
		{
			// Arrange
			var submitImageTask = await SubmitImageAsync(TestFile.Image);
			var parameters = new DocumentProcessingParams
			{
				TaskId = submitImageTask.TaskId,
				Language = "English",
				ExportFormats = new[]
				{
					ExportFormat.PdfSearchable,
					ExportFormat.Rtf,
				},
			};

			// Act
			var processDocumentTask = await ApiClient.ProcessDocumentAsync(
				parameters,
				true
			);

			// Assert
			CheckResultTask(processDocumentTask, submitImageTask.TaskId, 2);
		}

		[Test]
		public async Task ProcessBusinessCard_ShouldBeOk()
		{
			// Arrange
			var parameters = new BusinessCardProcessingParams
			{
				Language = "English",
				ExportFormats = BusinessCardExportFormat.Xml,
			};

			// Act
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

			// Assert
			CheckResultTask(processBusinessCardTask);
		}

		[Test]
		public async Task ProcessTextField_ShouldBeOk()
		{
			// Arrange
			var parameters = new TextFieldProcessingParams
			{
				Language = "English",
				Region = "140,550,1130,700",
			};

			// Act
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

			// Assert
			CheckResultTask(processTextFieldTask);
		}

		[Test]
		public async Task ProcessBarcodeField_ShouldBeOk()
		{
			// Arrange
			var parameters = new BarcodeFieldProcessingParams
			{
				BarcodeTypes = new[]
				{
					BarcodeType.Ean13,
				},
				Region = "1800,3200,2250,3400",
			};

			// Act
			TaskInfo processBarcodeFieldTask;
			using (var fileStream = GetResourceFileStream(TestFile.Fields))
			{
				processBarcodeFieldTask = await ApiClient.ProcessBarcodeFieldAsync(
					parameters,
					fileStream,
					TestFile.Fields,
					true
				);
			}

			// Assert
			CheckResultTask(processBarcodeFieldTask);
		}

		[Test]
		public async Task ProcessCheckmarkField_ShouldBeOk()
		{
			// Arrange
			var parameters = new CheckmarkFieldProcessingParams
			{
				CheckmarkType = CheckmarkType.Square,
				Region = "700,930,800,1030",
			};

			// Act
			TaskInfo processCheckmarkFieldTask;
			using (var fileStream = GetResourceFileStream(TestFile.Fields))
			{
				processCheckmarkFieldTask = await ApiClient.ProcessCheckmarkFieldAsync(
					parameters,
					fileStream,
					TestFile.Fields,
					true
				);
			}

			// Assert
			CheckResultTask(processCheckmarkFieldTask);
		}

		[Test]
		public async Task ProcessFields_ShouldBeOk()
		{
			// Arrange
			var submitImageTask = await SubmitImageAsync(TestFile.Fields);
			var parameters = new FieldsProcessingParams
			{
				TaskId = submitImageTask.TaskId,
				WriteRecognitionVariants = true,
			};

			// Act
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

			// Assert
			CheckResultTask(processFieldsTask, submitImageTask.TaskId);
		}

		[Test]
		public async Task ProcessMrz_ShouldBeOk()
		{
			// Arrange
			var parameters = new MrzProcessingParams
			{
				Description = "Task description: Mrz processing.",
			};

			// Act
			TaskInfo processMrzTask;
			using (var fileStream = GetResourceFileStream(TestFile.Mrz))
			{
				processMrzTask = await ApiClient.ProcessMrzAsync(
					parameters,
					fileStream,
					TestFile.Mrz,
					true
				);
			}

			// Assert
			CheckResultTask(processMrzTask);
		}

		[Test]
		public async Task GetTaskStatus_ShouldBeOk()
		{
			// Arrange
			var submitImageTask = await SubmitImageAsync(TestFile.Image);

			// Act
			var resultTask = await ApiClient.GetTaskStatusAsync(submitImageTask.TaskId);

			// Assert
			resultTask.ShouldNotBeNull();
			resultTask.TaskId.ShouldBe(submitImageTask.TaskId);
			resultTask.Status.ShouldBe(TaskStatus.Submitted);
		}

		[Test]
		public async Task DeleteTask_ShouldBeOk()
		{
			// Arrange
			var submitImageTask = await SubmitImageAsync(TestFile.Image);

			// Act
			var deletedTask = await ApiClient.DeleteTaskAsync(submitImageTask.TaskId);
			var resultTask = await ApiClient.GetTaskStatusAsync(deletedTask.TaskId);

			// Assert
			deletedTask.ShouldNotBeNull();
			resultTask.ShouldNotBeNull();

			deletedTask.TaskId.ShouldBe(submitImageTask.TaskId);
			resultTask.TaskId.ShouldBe(deletedTask.TaskId);

			submitImageTask.Status.ShouldBe(TaskStatus.Submitted);
			deletedTask.Status.ShouldBe(TaskStatus.Deleted);
			resultTask.Status.ShouldBe(TaskStatus.Deleted);
		}

		[Test]
		public async Task ListTasks_ShouldBeOk()
		{
			// Arrange
			var submitImageTask = await SubmitImageAsync(TestFile.Image);
			var parameters = new TasksListingParams
			{
				ExcludeDeleted = true,
			};

			// Act
			var listTasks = await ApiClient.ListTasksAsync(
				parameters
			);

			// Assert
			listTasks.ShouldNotBeNull();
			listTasks.Tasks.Count.ShouldBePositive();
			listTasks.Tasks.FirstOrDefault(item => item.TaskId == submitImageTask.TaskId).ShouldNotBeNull();
		}

		[Test]
		public async Task ListFinishedTasks_ShouldBeOk()
		{
			// Arrange
			var submitImageTask = await SubmitImageAsync(TestFile.Image);
			TaskInfo processImageTask;
			using (var fileStream = GetResourceFileStream(TestFile.Image))
			{
				processImageTask = await ApiClient.ProcessImageAsync(
					null,
					fileStream,
					TestFile.Image,
					true
				);
			}

			// Act
			var listFinishedTasks = await ApiClient.ListFinishedTasksAsync();

			// Assert
			listFinishedTasks.ShouldNotBeNull();

			listFinishedTasks.Tasks.Count.ShouldBePositive();
			listFinishedTasks.Tasks.Count.ShouldBeLessThanOrEqualTo(100);

			listFinishedTasks.Tasks.FirstOrDefault(
				item => item.TaskId == submitImageTask.TaskId).ShouldBeNull();
			listFinishedTasks.Tasks.FirstOrDefault(
				item => item.TaskId == processImageTask.TaskId).ShouldNotBeNull();
		}

		[Test]
		public async Task GetApplicationInfo_ShouldBeOk()
		{
			// Act
			var response = await ApiClient.GetApplicationInfoAsync();

			// Assert
			response.ShouldNotBeNull();
			response.Id.ShouldBe(Config.ApplicationId);
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

		private static void CheckResultTask(TaskInfo resultTask, Guid? taskId = null, int resultUrls = 1,
			TaskStatus taskStatus = TaskStatus.Completed)
		{
			resultTask.ShouldNotBeNull();

			resultTask.TaskId.ShouldNotBe(Guid.Empty);
			if (taskId.HasValue) resultTask.TaskId.ShouldBe(taskId.Value);

			resultTask.Status.ShouldBe(taskStatus);

			resultTask.ResultUrls.Count.ShouldBe(resultUrls);
		}
	}
}
