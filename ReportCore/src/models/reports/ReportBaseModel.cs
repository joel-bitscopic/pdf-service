using System;

namespace com.bitscopic.reportcore.models
{
    public abstract class ReportBaseModel {
        private ReportID _ReportID { get; set; }
        public ReportID ReportID { 
            get {
                return this._ReportID;
            }
        }

        private string _OutputFormat { get; set; }
        public string OutputFormat { 
            get {
                return this._OutputFormat;
            }
            set {
                if (value != Adobe.DocumentServices.PDFTools.options.documentmerge.OutputFormat.PDF.Format 
                && value != Adobe.DocumentServices.PDFTools.options.documentmerge.OutputFormat.DOCX.Format
                && value != "")
                    throw new ArgumentOutOfRangeException($"outputFormat '{value}' is not supported. Only 'pdf', 'docx', and '' are allowed.");
                
                this._OutputFormat = value;
            }
         }

        public ReportBaseModel(ReportID reportID) {
            this._ReportID = reportID;

            //default output format
            this.OutputFormat = "pdf";
        }
    }
}