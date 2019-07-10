// Copyright © 2019 ABBYY Production LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Abbyy.CloudSdk.V2.Client.Models;
using Abbyy.CloudSdk.V2.Client.Models.Enums;
using Abbyy.CloudSdk.V2.Client.Models.RequestParams;

namespace Abbyy.CloudSdk.V2.Client
{
	/// <summary>
	/// Client for the API of ABBYY Cloud OCR SDK
	/// </summary>
	public interface IOcrClient : IDisposable
	{
		/// <summary>
		/// The method loads the image, creates a processing task for the image with the specified parameters, and passes the task for processing.
		/// </summary>
		/// <remarks>
		/// This method allows you to specify up to three file formats for the result, in which case the server response for the completed task
		/// will contain several result URLs. If there is not enough money on your account, the task will be created, but will be suspended with
		/// <see cref="Models.Enums.TaskStatus.NotEnoughCredits"/> status. You can pass this task for processing using the <see cref="ProcessDocumentAsync"/>
		/// method after you have topped up your account. The task will not be created, if you exceed the limit of uploaded images.
		/// </remarks>
		/// <param name="parameters">Image processing parameters</param>
		/// <param name="fileStream">Stream of the file with the image to be recognized</param>
		/// <param name="fileName">Name of the file with the image</param>
		/// <param name="waitTaskFinished">Indicates whether to wait until task is finished.</param>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskInfo"/></returns>
		Task<TaskInfo> ProcessImageAsync(ImageProcessingParams parameters, Stream fileStream, string fileName, bool waitTaskFinished = false, CancellationToken cancellationToken = default);

		/// <summary>
		/// The method adds the image to the existing task or creates a new task for the image. This task is not passed for processing until
		/// the <see cref="ProcessDocumentAsync"/> or <see cref="ProcessFieldsAsync"/> method is called for it. Several images can be uploaded to one task
		/// </summary>
		/// <param name="parameters">Image submitting parameters</param>
		/// <param name="fileStream">Stream of the file with the image to be recognized</param>
		/// <param name="fileName">Name of the file with the image</param>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskInfo"/></returns>
		Task<TaskInfo> SubmitImageAsync(ImageSubmittingParams parameters, Stream fileStream, string fileName, CancellationToken cancellationToken = default);

		/// <summary>
		/// The method starts the processing task with the specified parameters.
		/// </summary>
		/// <remarks>
		/// This method allows you to process several images using the same settings and obtain recognition result as a multi-page document.
		/// You can upload several images to one task using <see cref="SubmitImageAsync"/> method.
		/// It is also possible to specify up to three file formats for the result, in which case the server response for the completed
		/// task will contain several result URLs.
		/// Only the task with <see cref="Models.Enums.TaskStatus.Submitted"/>, <see cref="Models.Enums.TaskStatus.Completed"/> or
		/// <see cref="Models.Enums.TaskStatus.NotEnoughCredits"/> status can be started using this method.
		/// </remarks>
		/// <param name="parameters">Document processing parameters</param>
		/// <param name="waitTaskFinished">Indicates whether to wait until task is finished.</param>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskInfo"/></returns>
		Task<TaskInfo> ProcessDocumentAsync(DocumentProcessingParams parameters, bool waitTaskFinished = false, CancellationToken cancellationToken = default);

		/// <summary>
		/// The method allows you to recognize a business card on an image. The method loads the image, creates a processing task for the image
		/// with the specified parameters, and passes the task for processing.
		/// </summary>
		/// <param name="parameters">Business card processing parameters</param>
		/// <param name="fileStream">Stream of the file with the image to be recognized</param>
		/// <param name="fileName">Name of the file with the image</param>
		/// <param name="waitTaskFinished">Indicates whether to wait until task is finished.</param>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskInfo"/></returns>
		Task<TaskInfo> ProcessBusinessCardAsync(BusinessCardProcessingParams parameters, Stream fileStream, string fileName, bool waitTaskFinished = false, CancellationToken cancellationToken = default);

		/// <summary>
		/// The method allows you to extract the value of a text field on an image. The method loads the image, creates a processing task for the image
		/// with the specified parameters, and passes the task for processing. The result of recognition is returned in XML format.
		/// </summary>
		/// <remarks>
		/// See <see href="https://www.ocrsdk.com/documentation/quick-start-guide/text-fields-recognition/">How to Recognize Text Fields</see> to know how
		/// to tune the parameters.
		/// <see href="https://www.ocrsdk.com/documentation/specifications/processing-profiles/"/>
		/// </remarks>
		/// <param name="parameters">Text field processing parameters</param>
		/// <param name="fileStream">Stream of the file with the image to be recognized</param>
		/// <param name="fileName">Name of the file with the image</param>
		/// <param name="waitTaskFinished">Indicates whether to wait until task is finished.</param>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskInfo"/></returns>
		Task<TaskInfo> ProcessTextFieldAsync(TextFieldProcessingParams parameters, Stream fileStream, string fileName, bool waitTaskFinished = false, CancellationToken cancellationToken = default);

		/// <summary>
		/// The method allows you to extract the value of a barcode on an image. The method loads the image, creates a processing task for
		/// the image with the specified parameters, and passes the task for processing. The result of recognition is returned in XML format.
		/// Binary data is returned in Base64 encoding.
		/// </summary>
		/// <remarks>
		/// See <see href="https://www.ocrsdk.com/documentation/quick-start-guide/barcode-ocr-sdk/">How to Recognize Barcodes</see> to know another
		/// way of barcode recognition.
		/// <see cref="ProcessingProfile.BarcodeRecognition"/> profile is used for processing. Information about processing profiles
		/// <see href="https://www.ocrsdk.com/documentation/specifications/processing-profiles/"/>
		/// </remarks>
		/// <param name="parameters">Barcode field processing parameters</param>
		/// <param name="fileStream">Stream of the file with the image to be recognized</param>
		/// <param name="fileName">Name of the file with the image</param>
		/// <param name="waitTaskFinished">Indicates whether to wait until task is finished.</param>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskInfo"/></returns>
		Task<TaskInfo> ProcessBarcodeFieldAsync(BarcodeFieldProcessingParams parameters, Stream fileStream, string fileName, bool waitTaskFinished = false, CancellationToken cancellationToken = default);

		/// <summary>
		/// The method allows you to extract the value of a checkmark on an image. The method loads the image, creates a processing task for
		/// the image with the specified parameters, and passes the task for processing. The result of recognition is returned in XML format.
		/// The values of checkmarks are "checked", "unchecked", or "corrected".
		/// </summary>
		/// <remarks>
		/// <see href="https://www.ocrsdk.com/documentation/specifications/processing-profiles/"/>
		/// </remarks>
		/// <param name="parameters">Checkmark field processing parameters</param>
		/// <param name="fileStream">Stream of the file with the image to be recognized</param>
		/// <param name="fileName">Name of the file with the image</param>
		/// <param name="waitTaskFinished">Indicates whether to wait until task is finished.</param>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskInfo"/></returns>
		Task<TaskInfo> ProcessCheckmarkFieldAsync(CheckmarkFieldProcessingParams parameters, Stream fileStream, string fileName, bool waitTaskFinished = false, CancellationToken cancellationToken = default);

		/// <summary>
		/// The method allows you to recognize several fields in a document. The method starts the processing task with the parameters of processing
		/// specified in an XML file. Image files can be uploaded to the task using <see cref="SubmitImageAsync"/> method. The result of recognition is
		/// returned in XML format. Binary data is returned in Base64 encoding.
		/// </summary>
		/// <remarks>
		/// You can use the <see href="https://www.ocrsdk.com/schema/taskDescription-1.0.xsd">XSD schema</see> of the XML file to create the file
		/// with necessary settings. See also the description of the tags and several examples of XML files with settings in
		/// <see href="https://www.ocrsdk.com/documentation/specifications/xml-scheme-field-settings/">XML Parameters of Field Recognition</see>.
		/// Only the task with <see cref="Models.Enums.TaskStatus.Submitted"/>, <see cref="Models.Enums.TaskStatus.Completed"/>Completed or
		/// <see cref="Models.Enums.TaskStatus.NotEnoughCredits"/> status can be started using this method.
		/// Note that this method is most convenient when you process a large number of fields on one page: in this case the price of recognition
		/// of all fields on one page does not exceed the price of recognition of a page of A4 size. 
		/// </remarks>
		/// <param name="parameters">Fields processing parameters</param>
		/// <param name="fileStream">XML File describing fields recognition settings</param>
		/// <param name="fileName">Name of the file with the image</param>
		/// <param name="waitTaskFinished">Indicates whether to wait until task is finished.</param>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskInfo"/></returns>
		Task<TaskInfo> ProcessFieldsAsync(FieldsProcessingParams parameters, Stream fileStream, string fileName, bool waitTaskFinished = false, CancellationToken cancellationToken = default);

		/// <summary>
		/// This method finds a machine-readable zone on the image and extracts data from it. Machine-readable zone(MRZ) is typically found on
		/// official travel or identity documents of many countries.It can have 2 lines or 3 lines of machine-readable data. This method allows to
		/// process MRZ written in accordance with ICAO Document 9303 (endorsed by the International Organization for Standardization and the
		/// International Electrotechnical Commission as ISO/IEC 7501-1)). The result of recognition is returned in XML format.
		/// </summary>
		/// <remarks>
		/// <see href="https://en.wikipedia.org/wiki/ICAO"/>
		/// <see href="https://en.wikipedia.org/wiki/International_Organization_for_Standardization"/>
		/// <see href="https://en.wikipedia.org/wiki/International_Electrotechnical_Commission"/>
		/// </remarks>
		/// <param name="parameters">Mrz processing parameters</param>
		/// <param name="fileStream">Stream of the file with the image to be recognized</param>
		/// <param name="fileName">Name of the file with the image</param>
		/// <param name="waitTaskFinished">Indicates whether to wait until task is finished.</param>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskInfo"/></returns>
		Task<TaskInfo> ProcessMrzAsync(MrzProcessingParams parameters, Stream fileStream, string fileName, bool waitTaskFinished = false, CancellationToken cancellationToken = default);

		/// <summary>
		/// Important: the technology fully supports the receipts issued in USA and France, other countries are currently supported in beta mode.
		/// The method allows you to recognize the image of a receipt.The method loads the image, creates a processing task for the image with the
		/// specified parameters, and passes the task for processing. The result is returned in XML format.
		/// </summary>
		/// <remarks>
		/// The elements and attributes of the resulting file are described in
		/// <see href="https://www.ocrsdk.com/documentation/specifications/xml-scheme-recognized-receipt/">Output XML with Receipt Data</see>.
		/// For a step-by-step guide, see <see href="https://www.ocrsdk.com/documentation/quick-start-guide/receipt-recognition/">How to Recognize
		/// Receipts.</see> The recommendations on preparing the input images can be found in
		/// <see href="https://www.ocrsdk.com/documentation/hints-tips/photograph-scan-receipts/">Photographing and Scanning Receipts</see>.
		/// </remarks>
		/// <param name="parameters">Receipt processing parameters</param>
		/// <param name="fileStream">Stream of the file with the image to be recognized</param>
		/// <param name="fileName">Name of the file with the image</param>
		/// <param name="waitTaskFinished">Indicates whether to wait until task is finished.</param>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskInfo"/></returns>
		Task<TaskInfo> ProcessReceiptAsync(ReceiptProcessingParams parameters, Stream fileStream, string fileName, bool waitTaskFinished = false, CancellationToken cancellationToken = default);

		/// <summary>
		/// The method returns the current status of the task and the URL of the result of processing for completed tasks.
		/// Please note that the task status is not changed momentarily. Do not call this method more frequently than once in 2 or 3 seconds.
		/// </summary>
		/// <param name="taskId">Id of the task</param>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskInfo"/></returns>
		Task<TaskInfo> GetTaskStatusAsync(Guid taskId, CancellationToken cancellationToken = default);

		/// <summary>
		/// The method deletes the task and the images associated with this task from the ABBYY Cloud OCR SDK storage.
		/// Only the tasks that have the status other than <see cref="Models.Enums.TaskStatus.InProgress"/> or <see cref="Models.Enums.TaskStatus.Deleted"/>
		/// can be deleted.
		/// </summary>
		/// <remarks>
		/// If you try to delete the task that has already been deleted, the successful response is returned.
		/// If you submit the same image to different tasks, to delete the image from the Cloud OCR SDK storage, you will need to call
		/// the <see cref="DeleteTaskAsync"/> method for each task, which contains the image.
		/// </remarks>
		/// <param name="taskId">Id of the task</param>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskInfo"/></returns>
		Task<TaskInfo> DeleteTaskAsync(Guid taskId, CancellationToken cancellationToken = default);

		/// <summary>
		/// The method returns the list of tasks created by your application. By default, the <see cref="Models.Enums.TaskStatus.Deleted"/>
		/// tasks are included, but you can filter them out. This method may be useful if you need to compile an application usage
		/// log for some period of time.
		/// </summary>
		/// <remarks>
		/// The tasks are ordered by the date of the last action with the task. This method can list up to 1000 tasks. If you process
		/// a large number of tasks, call this method for shorter time periods.
		/// </remarks>
		/// <param name="parameters">Parameters for listing tasks</param>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskInfo"/></returns>
		Task<TaskList> ListTasksAsync(TasksListingParams parameters, CancellationToken cancellationToken = default);

		/// <summary>
		/// The method returns the list of finished tasks. A task is finished if it has one of the following statuses:
		/// <see cref="Models.Enums.TaskStatus.Completed"/>, <see cref="Models.Enums.TaskStatus.ProcessingFailed"/>,
		/// <see cref="Models.Enums.TaskStatus.NotEnoughCredits"/>.
		/// </summary>
		/// <remarks>
		/// The tasks are ordered by the time of the end of processing. No more than 100 tasks can be returned at one method call. To
		/// obtain more tasks, delete some finished tasks using the <see cref="DeleteTaskAsync"/> method and then call the
		/// <see cref="ListFinishedTasksAsync"/> method again.
		/// The method may be useful if you work with a large number of tasks simultaneously. But there is no sense in calling this
		/// method if your application does not have any incomplete tasks sent for the processing.
		/// Please note that the task status is not changed momentarily. Do not call this method more frequently than once in 2 or
		/// 3 seconds.
		/// </remarks>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="TaskList"/></returns>
		Task<TaskList> ListFinishedTasksAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// This method allows you to receive information about the application type, its current balance and expiration date. You may
		/// find it helpful for keeping the usage records.
		/// </summary>
		/// <remarks>
		/// The application is identified by its authentication information.
		/// By default the call to this method is disabled for all applications. To enable getting the application information using
		/// this method: 1) go to <see href="https://cloud.ocrsdk.com/"/> and log in; 2) click Settings under your application's name;
		/// 3) on the next screen, click enable:
		/// </remarks>
		/// <param name="cancellationToken">Cancellation token of the request</param>
		/// <returns><see cref="Application"/></returns>
		Task<Application> GetApplicationInfoAsync(CancellationToken cancellationToken = default);
	}
}
