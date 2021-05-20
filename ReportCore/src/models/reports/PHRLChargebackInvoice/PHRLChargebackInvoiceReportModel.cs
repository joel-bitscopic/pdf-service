using System;
using System.Collections.Generic;

using com.bitscopic.reportcore.utils;

namespace com.bitscopic.reportcore.models
{
    public class PHRLChargebackInvoiceReportModel : ReportBaseModel {
        public IList<PHRLInvoiceModel> Invoices { get; set; }

        public string Site { get; set; }
        public DateTime? Date { get; set; }
        public string FormattedDate {
            get {
                return this.Date.HasValue ? this.Date.Value.ToReportDate(false) : "";
            }
        }
        public string Attention { get; set; }
        public decimal Total { get; set; }
        public string FormattedTotal {
            get {
                return this.Total.ToString("C2");
            }
        }

        public PHRLChargebackInvoiceReportModel(EncodedImage headerImage, IList<PHRLInvoiceModel> invoices) 
            : base(ReportID.PHRLChargebackInvoice)
        {
            this.HeaderImage = headerImage ?? throw new ArgumentNullException(nameof(headerImage));
            this.Invoices = invoices ?? throw new ArgumentNullException(nameof(invoices));
        }
    }
}