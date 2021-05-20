using System;
using System.Collections.Generic;

namespace com.bitscopic.reportcore.models {
    //this DTO and static class should be easy to convert to a database driven approach. the enum represents the primary key and the remaining fields represent columns
    public class ReportMetadata {
        public string Name { get; set; }
        public string DefaultFilename { get; set; }
        public string TemplateFilename { get; set; }
        public EncodedImage HeaderImage { get; set;}

        public ReportMetadata(string name, string defaultFilename, string templateFilename, EncodedImage headerImage) {
            this.Name = name;
            this.DefaultFilename = defaultFilename;
            this.TemplateFilename = templateFilename;
            this.HeaderImage = headerImage;
        }
    }
    public static class StaticReportMetadata {
        private const string TemplateDirectoryName = "resources";
        private static Dictionary<ReportID, ReportMetadata> _ReportMetadata { get; set; }
        public static IReadOnlyDictionary<ReportID, ReportMetadata> ReportMetadata 
        { 
            get 
            {
                return StaticReportMetadata._ReportMetadata;
            }
         }
        
        public static string GetReportTemplateFilename(this ReportID reportID) {
            return StaticReportMetadata.ReportMetadata[reportID].TemplateFilename;
        }
        public static string GetReportDefaultFilename(this ReportID reportID) {
            return StaticReportMetadata.ReportMetadata[reportID].DefaultFilename;
        }
        public static EncodedImage GetReportHeaderImage(this ReportID reportID) {
            return StaticReportMetadata.ReportMetadata[reportID].HeaderImage;
        }
        public static ReportID GetReportIDFromReportName(string reportName) {
            foreach (KeyValuePair<ReportID, ReportMetadata> reportTypePair in StaticReportMetadata.ReportMetadata) {
                if (reportTypePair.Value.Name == reportName)
                    return reportTypePair.Key;
            }

            throw new ArgumentException("ReportID " + reportName + " is not a supported report model type");
        }

        static StaticReportMetadata() {
            StaticReportMetadata._ReportMetadata = new Dictionary<ReportID, ReportMetadata>();

            StaticReportMetadata._ReportMetadata.Add(ReportID.HIVINResistance, new ReportMetadata(
                @"HIVINResistance", 
                @"HIV IN Resistance Report",
                @"HIV IN Resistance Template.docx",
                new EncodedImage(
                    ReportImageMetadata.VETERAN_AFFAIR_HEADER_IMAGE,
                    "U.S. Department of Veteran Affairs"
                )
            ));
            //StaticReportMetadata._ReportMetadata.Add(ReportID.HCVResistance, );
            //StaticReportMetadata._ReportMetadata.Add(ReportID.HIVPRRTResistance, );
            StaticReportMetadata._ReportMetadata.Add(ReportID.PHRLChargebackInvoice, new ReportMetadata(
                @"PHRLChargebackInvoice", 
                @"PHRL Chargeback Invoice", 
                @"PHRL Chargeback Invoice Template.docx",
                new EncodedImage(
                    ReportImageMetadata.PHRL_HEADER_IMAGE,
                    "Public Health Reference Library"
                )
            ));
            StaticReportMetadata._ReportMetadata.Add(ReportID.COVIDSequencing2B, new ReportMetadata(
                @"COVIDSequencing2B",
                @"COVID19 sequencing report 2B",
                @"COVID19 sequencing report 2B Template.docx",
                new EncodedImage(
                    ReportImageMetadata.VETERAN_AFFAIR_HEADER_IMAGE,
                    "U.S. Department of Veteran Affairs"
                )
            ));
        }
    }
}