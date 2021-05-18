namespace TemplatedReportGenerator.Model
{
    public class COVID19Sequencing2BResultsModel 
    {
        public string Gene { get; set; }
        public string MutationDetected { get; set; }
        public string Zygosity { get; set; }
        public string MutationType { get; set; }

        public COVID19Sequencing2BResultsModel() { }
        public COVID19Sequencing2BResultsModel(string gene, string mutationDetected, string zygosity, string mutationType) 
        {
            this.Gene = gene;
            this.MutationDetected = mutationDetected;
            this.Zygosity = zygosity;
            this.MutationType = mutationType;
        }
    }
}