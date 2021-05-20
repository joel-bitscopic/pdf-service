using System;
using com.bitscopic.reportcore.utils;

namespace com.bitscopic.reportcore.models
{
    public class PatientModel {
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FormattedDateOfBirth {
            get {
                return CollectionDate.HasValue ? this.CollectionDate.Value.ToReportDate(false) : "";
            }
        }
        public DateTime? CollectionDate { get; set; }
        public string FormattedCollectionDate {
            get {
                return CollectionDate.HasValue ? this.CollectionDate.Value.ToReportDate(true) : "";
            }
        }
        public string SiteAccessionNumber { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string OrderingSite { get; set; }
        public string OrderingPhysician { get; set; }
        public string PHLRAccessionNumber { get; set; }

        public PatientModel(string name) {
            //if (String.IsNullOrWhiteSpace(name))
            //    throw new ArgumentNullException("name must not be null or white space");
            
            this.Name = name;
        }
    }
}