using System.Collections.Generic;

namespace com.bitscopic.reportcore.models
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
        public string Comments { get; set; }        

        public COVID19Sequencing2BReportModel(COVID19Sequencing2BPatientModel patient, ResistanceTestModel resistanceTest, IList<COVID19Sequencing2BResultsModel> results) 
            : base(ReportID.COVIDSequencing2B)
        {
            this.Patient = patient ?? throw new System.ArgumentNullException(nameof(patient));
            this.ResistanceTest = resistanceTest ?? throw new System.ArgumentNullException(nameof(resistanceTest));
            this.ResultSet = results ?? throw new System.ArgumentNullException(nameof(results));
        }
    }
}