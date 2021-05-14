using System;
using System.IO;
using Newtonsoft.Json.Linq;

using Adobe.DocumentServices.PDFTools;
using Adobe.DocumentServices.PDFTools.auth;
using Adobe.DocumentServices.PDFTools.options.documentmerge;
using Adobe.DocumentServices.PDFTools.pdfops;
using Adobe.DocumentServices.PDFTools.io;

using TemplatedReportGenerator.ReportModel;

namespace TemplatedReportGenerator
{
    public static class TemplatedReportGenerator {
        public static string GetReportDefaultFilename(ReportID reportID, OutputFormat outputFormat) {
            string timeStamp = DateTime.Now.ToString("dd-MM-yy_hh-ss");

            return reportID.GetReportDefaultFilename() + "_" + timeStamp + outputFormat.GetFileExtensionForOutputFormat();
        }
        public static string GetFileExtensionForOutputFormat(this OutputFormat outputFormat) {
            if (outputFormat == OutputFormat.PDF)
                return ".pdf";
            else if (outputFormat == OutputFormat.DOCX)
                return ".docx";
            else
                throw new ArgumentException("Output format not supported");
        }

        private static string GetCredentialFilePath() {
            return Directory.GetCurrentDirectory() + "/pdftools-api-credentials.json";
        }

        ///<summary>
        ///Generate a PDF report from a supported model. The template used will be inferred by the model's ReportID property.
        ///</summary>
        ///<returns>
        ///A FileRef representing the report. This points to a file in the OS's default temporary folder (such as Window's roaming). This FileRef can be saved to a specific location or turned into a bytestream.
        ///</returns>
        ///<exception cref="System.NotSupportedException">Thrown when model is of an unsupported type.</exception>
        ///<exception cref="Adobe.DocumentServices.PDFTools.exception.ServiceApiException">Thrown when the API key is invalid. The API key for this toolkit expires yearly and needs to be refreshed on that basis.</exception>
        ///<param name="model">A data transfer model representing all information needed by the corresponding templated report. Only models from the TemplatedReportGenerator.ReportModel namespace are supported, other models will throw an exception.</param>
        public static FileRef GenerateReport<T>(T model) {
            return GenerateReport(model, OutputFormat.PDF);
        }
        ///<summary>
        ///Generate a PDF or MSWord report from a supported model. The template used will be inferred by the model's ReportID property.
        ///</summary>
        ///<returns>
        ///A FileRef representing the report. This points to a file in the OS's default temporary folder (such as Window's roaming). This FileRef can be saved to a specific location or turned into a bytestream.
        ///</returns>
        ///<exception cref="System.NotSupportedException">Thrown when model is of an unsupported type.</exception>
        ///<exception cref="Adobe.DocumentServices.PDFTools.exception.ServiceApiException">Thrown when the API key is invalid. The API key for this toolkit expires yearly and needs to be refreshed on that basis.</exception>
        ///<param name="model">A data transfer model representing all information needed by the corresponding templated report. Only models from the TemplatedReportGenerator.ReportModel namespace are supported, other models will throw an exception.</param>
        ///<param name="outputFormat">The output format of the generated report. Adobe's generation toolkit supports both PDF and DOCX</param>
        public static FileRef GenerateReport<T>(T model, OutputFormat outputFormat) {
            JObject jsonModel = JObject.FromObject(model);

            return GenerateReport(jsonModel, outputFormat);
        }
    
        public static FileRef GenerateReport(JObject jsonModel) {
            return GenerateReport(jsonModel, OutputFormat.PDF);
        }
        public static FileRef GenerateReport(JObject jsonModel, OutputFormat outputFormat) {
            string strReportID = ((JToken)jsonModel["ReportID"]).Value<string>();
            int intReportID;
            bool isNumeric = int.TryParse(strReportID, out intReportID);

            ReportID reportID;
            if (isNumeric)
                reportID = (ReportID)intReportID;
            else
                reportID = strReportID.GetReportIDFromReportName();
            
            
            Credentials credentials = Credentials.ServiceAccountCredentialsBuilder()
                                        .FromFile(GetCredentialFilePath())
                                        .Build();
            
            ExecutionContext executionContext = ExecutionContext.Create(credentials);

            DocumentMergeOptions documentMergeOptions = new DocumentMergeOptions(jsonModel, outputFormat);
            
            DocumentMergeOperation documentMergeOperation = DocumentMergeOperation.CreateNew(documentMergeOptions);
            string templateFilePath = StaticReportMetadata.ReportMetadata[reportID].Filepath;
            documentMergeOperation.SetInput(FileRef.CreateFromLocalFile(templateFilePath));

            FileRef result = documentMergeOperation.Execute(executionContext);

            return result;
        }
    }
}