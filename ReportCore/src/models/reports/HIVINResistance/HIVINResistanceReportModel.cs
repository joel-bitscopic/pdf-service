using TemplatedReportGenerator.Model;

namespace TemplatedReportGenerator.ReportModel
{
    public class HIVINResistanceReportModel : ReportBaseModel {
        public PatientModel Patient { get; set; }
        public HIVINResistanceTestModel ResistanceTest { get; set; }
        public InhibitorResistanceResultsModel<InhibitorResistanceStandardModel> ResultSet { get; set; }

        public string IntegraseHIVSubtype { get; set; }
        public string IntegraseCodonsAnalyzed { get; set; }
        public string IntegraseMajorResistanceDetected { get; set; }
        public string IntegraseAccessoryResistanceDetected { get; set; }
        public string OtherAminoAcidChanges { get; set; }

        public string SuperscriptAContent { get; set; }
        public string SuperscriptBContent { get; set; }
        

        public HIVINResistanceReportModel(EncodedImage headerImage, PatientModel patient, HIVINResistanceTestModel resistanceTest, InhibitorResistanceResultsModel<InhibitorResistanceStandardModel> results) 
            : base(ReportID.HIVINResistance)
        {
            this.HeaderImage = headerImage ?? throw new System.ArgumentNullException(nameof(headerImage));
            this.Patient = patient ?? throw new System.ArgumentNullException(nameof(patient));
            this.ResistanceTest = resistanceTest ?? throw new System.ArgumentNullException(nameof(resistanceTest));
            this.ResultSet = results ?? throw new System.ArgumentNullException(nameof(results));
        }
    }
}