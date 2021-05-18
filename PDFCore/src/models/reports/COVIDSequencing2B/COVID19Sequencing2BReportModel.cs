using System.Collections.Generic;

using TemplatedReportGenerator.Model;

namespace TemplatedReportGenerator.ReportModel
{
    public class COVID19Sequencing2BReportModel : ReportBaseModel 
    {
        public COVID19Sequencing2BPatientModel Patient { get; set; }
        public ResistanceTestModel ResistanceTest { get; set; }
        public IList<COVID19Sequencing2BResultsModel> ResultSet { get; set; }

        public string ResultSummary { get; set; }
        public string NextClade { get; set; }
        public string PangoLineage { get; set; }
        public string Interpretation { get; set; }

        public string SuperscriptAContent { get; set; }
        public string SuperscriptBContent { get; set; }
        public string SuperscriptCContent { get; set; }
        

        public COVID19Sequencing2BReportModel(EncodedImage headerImage, COVID19Sequencing2BPatientModel patient, ResistanceTestModel resistanceTest, IList<COVID19Sequencing2BResultsModel> results) 
            : base(ReportID.COVIDSequencing2B)
        {
            this.HeaderImage = headerImage ?? throw new System.ArgumentNullException(nameof(headerImage));
            this.Patient = patient ?? throw new System.ArgumentNullException(nameof(patient));
            this.ResistanceTest = resistanceTest ?? throw new System.ArgumentNullException(nameof(resistanceTest));
            this.ResultSet = results ?? throw new System.ArgumentNullException(nameof(results));
        }
    }
}