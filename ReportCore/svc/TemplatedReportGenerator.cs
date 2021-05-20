using System;
using System.IO;

using Newtonsoft.Json.Linq;

using Adobe.DocumentServices.PDFTools;
using Adobe.DocumentServices.PDFTools.auth;
using Adobe.DocumentServices.PDFTools.options.documentmerge;
using Adobe.DocumentServices.PDFTools.pdfops;
using Adobe.DocumentServices.PDFTools.io;

using com.bitscopic.reportcore.models;

namespace com.bitscopic.reportcore.svc
{
    public static class TemplatedReportGenerator {
        public const string TemplateDirectoryName = "resources";
        public static string GetReportDefaultFilename(JObject reportModel) {
            return GetReportDefaultFilename(ConvertReportID(reportModel), ConvertReportOutputFormat(reportModel));
        }
        public static string GetReportDefaultFilename(ReportID reportID, OutputFormat outputFormat) {
            string timeStamp = DateTime.Now.ToString("dd-MM-yy_hh-ss");

            return reportID.GetReportDefaultFilename() + "_" + timeStamp + GetFileExtensionForOutputFormat(outputFormat);
        }

        public static string GetFileExtensionForOutputFormat(OutputFormat outputFormat) {
            if (outputFormat == OutputFormat.PDF)
                return ".pdf";
            else if (outputFormat == OutputFormat.DOCX)
                return ".docx";
            else
                throw new ArgumentException("Output format not supported");
        }

        public static string GetTemplateFileDirectory(ReportID reportID, string reportTemplateDirectory = null) {
            if (string.IsNullOrWhiteSpace(reportTemplateDirectory))
                reportTemplateDirectory = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{TemplateDirectoryName}";

            string templateFilename = StaticReportMetadata.GetReportTemplateFilename(reportID);
            string absoluteTemplateFilePath = $"{reportTemplateDirectory}{Path.DirectorySeparatorChar}{templateFilename}";

            return absoluteTemplateFilePath;
        }

        public static ReportID ConvertReportID(JObject jsonModel) {
            //attempt to support passing integer ID associated with ReportID enum or string name associated with enum
            string strReportID = ((JToken)jsonModel["ReportID"]).Value<string>();
            int intReportID;
            bool isNumeric = int.TryParse(strReportID, out intReportID);

            if (isNumeric)
                return (ReportID)intReportID;
            else
                return StaticReportMetadata.GetReportIDFromReportName(strReportID);
        }

        public static OutputFormat ConvertReportOutputFormat(ReportBaseModel model) {
            return ConvertReportOutputFormat(model.OutputFormat);
        }
        public static OutputFormat ConvertReportOutputFormat(JObject jsonModel) {
            if (jsonModel.ContainsKey("OutputFormat")) {
                string strOutputFormat = ((JToken)jsonModel["OutputFormat"]).Value<string>();

                return ConvertReportOutputFormat(strOutputFormat);
            }
            else
                return OutputFormat.PDF;
        }
        public static OutputFormat ConvertReportOutputFormat(string outputFormat) {
            if (outputFormat.Trim().Equals("docx", StringComparison.InvariantCultureIgnoreCase))
                    return OutputFormat.DOCX;
                else
                    return OutputFormat.PDF;
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
        public static FileRef GenerateReport<T>(T model, string templateDirectoryFilepath = null) 
            where T : ReportBaseModel 
        {
            JObject jsonModel = JObject.FromObject(model);

            return GenerateReport(jsonModel, templateDirectoryFilepath);
        }
    
        public static FileRef GenerateReport(JObject jsonModel, string templateDirectoryFilepath = null) {
            ReportID reportID = ConvertReportID(jsonModel);
            OutputFormat outputFormat = ConvertReportOutputFormat(jsonModel);
            
            Credentials credentials = AdobeCredentials.GetAdobeCredentials();
            
            ExecutionContext executionContext = ExecutionContext.Create(credentials);

            DocumentMergeOptions documentMergeOptions = new DocumentMergeOptions(jsonModel, outputFormat);
            
            DocumentMergeOperation documentMergeOperation = DocumentMergeOperation.CreateNew(documentMergeOptions);
            string absoluteTemplateFilePath = GetTemplateFileDirectory(reportID, templateDirectoryFilepath);
            documentMergeOperation.SetInput(FileRef.CreateFromLocalFile(absoluteTemplateFilePath));

            FileRef result = documentMergeOperation.Execute(executionContext);

            return result;
        }
    
        public static byte[] ToByteArray(this FileRef fileRef) {
            byte[] byteResult;

            using(MemoryStream outputStream = new MemoryStream()) {
                fileRef.SaveAs(outputStream);
                byteResult = outputStream.ToArray();
            }

            return byteResult;
        }
    }
}