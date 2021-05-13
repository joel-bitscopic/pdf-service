using System;

namespace TemplatedReportGenerator.Model
{
    public class PHRLInvoiceModel {
        public string PHRLNumber { get; set; }
        public string Description { get; set; }
        public string CPTCode { get; set; }
        public decimal Price { get; set; }
        public string FormattedPrice {
            get {
                return this.Price.ToString("C2");
            }
        }
        public DateTime? DateReceived { get; set; }
        public string FormattedDateReceived {
            get {
                return this.DateReceived.HasValue ? this.DateReceived.Value.ToReportDate(false) : "";
            }
        }
        public string SiteSpecimenNumber { get; set; }
    }
}