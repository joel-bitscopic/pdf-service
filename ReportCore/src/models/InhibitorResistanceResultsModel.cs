using System.Collections.Generic;

namespace com.bitscopic.reportcore.models
{
    public class InhibitorResistanceResultsModel<T> {
        public string Name { get; set; }
        public IList<T> Results { get; set; }

        public InhibitorResistanceResultsModel(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new System.ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));

            this.Name = name;

            this.Results = new List<T>();
        }
    }
}