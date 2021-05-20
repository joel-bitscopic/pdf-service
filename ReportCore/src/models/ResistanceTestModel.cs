using System;

using com.bitscopic.reportcore.utils;

namespace com.bitscopic.reportcore.models
{
    public class ResistanceTestModel {
        public string TestPerformed { get; set; }
        public DateTime? TestDate { get; set; }
        public string FormattedTestDate {
            get {
                return this.TestDate.HasValue ? this.TestDate.Value.ToReportDate(false) : "";
            }
        }
        public DateTime? ReceivedDate { get; set; }
        public string FormattedReceivedDate {
            get {
                return this.ReceivedDate.HasValue ? this.ReceivedDate.Value.ToReportDate(true) : "";
            }
        }
        public string SampleType { get; set; }
        public DateTime? ReportDate { get; set; }
        public string FormattedReportDate {
            get {
                return this.ReportDate.HasValue ? this.ReportDate.Value.ToReportDate(true) : "";
            }
        }
    }
}