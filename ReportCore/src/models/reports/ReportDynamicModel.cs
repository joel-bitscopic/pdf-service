using System;

namespace com.bitscopic.reportcore.models
{
    public abstract class ReportDynamicModel : ReportBaseModel {
        public EncodedImage HeaderImage { get; set; }
        public string Comments { get; set; }
        public string Footer { get; set; }

        public ReportDynamicModel(ReportID reportID) 
            : base(reportID)
        {
            
        }
    }
}