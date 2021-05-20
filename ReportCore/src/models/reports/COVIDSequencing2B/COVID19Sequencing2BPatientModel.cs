namespace com.bitscopic.reportcore.models
{
    public class COVID19Sequencing2BPatientModel : PatientModel {
        public string PraediGeneAccessionNumber { get; set; }

        public COVID19Sequencing2BPatientModel(string name) 
            : base(name)
        { }
    }
}