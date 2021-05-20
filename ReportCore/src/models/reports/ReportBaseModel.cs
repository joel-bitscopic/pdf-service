using System;

namespace com.bitscopic.reportcore.models
{
    public abstract class ReportBaseModel {
        public ReportID ReportID { get; set; }

        private string _OutputFormat { get; set; }
        public string OutputFormat { 
            get {
                return this._OutputFormat;
            }
            set {
                if (value != Adobe.DocumentServices.PDFTools.options.documentmerge.OutputFormat.PDF.Format 
                && value != Adobe.DocumentServices.PDFTools.options.documentmerge.OutputFormat.DOCX.Format)
                    throw new ArgumentOutOfRangeException($"outputFormat '{value}' is not supported. Only 'pdf' and 'docx' are allowed.");
                
                this._OutputFormat = value;
            }
         }

        public EncodedImage HeaderImage { get; set; }
        public string Comments { get; set; }
        public string Footer { get; set; }

        public ReportBaseModel(ReportID reportID) {
            this.ReportID = reportID;

            //default output format
            this.OutputFormat = "pdf";
        }
    }
}