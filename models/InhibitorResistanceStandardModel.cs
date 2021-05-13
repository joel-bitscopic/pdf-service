namespace TemplatedReportGenerator.Model
{
    public class InhibitorResistanceStandardModel {
        public string DrugName { get; set; }
        public string Resistance { get; set; }

        public InhibitorResistanceStandardModel(string drugName, string resistance) {
            if (string.IsNullOrWhiteSpace(drugName))
                throw new System.ArgumentException($"'{nameof(drugName)}' cannot be null or whitespace.", nameof(drugName));
            if (string.IsNullOrWhiteSpace(resistance))
                throw new System.ArgumentException($"'{nameof(resistance)}' cannot be null or whitespace.", nameof(resistance));

            this.DrugName = drugName;
            this.Resistance = resistance;
        }
    }
}