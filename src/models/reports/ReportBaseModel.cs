using System;
using TemplatedReportGenerator.Model;

namespace TemplatedReportGenerator.ReportModel
{
    public abstract class ReportBaseModel {
        public ReportID ReportID { get; set; }

        public EncodedImage HeaderImage { get; set; }
        public string Comments { get; set; }
        public string Footer { get; set; }

        public ReportBaseModel(ReportID reportID) {
            this.ReportID = reportID;
        }
    }
}