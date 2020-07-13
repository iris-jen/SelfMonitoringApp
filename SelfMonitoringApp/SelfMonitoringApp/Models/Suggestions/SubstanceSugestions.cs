using SelfMonitoringApp.Models.Base;
using System.Collections.Generic;

namespace SelfMonitoringApp.Models.Suggestions
{
    public class SubstanceSugestions : ISugestions
    {
        public ModelType ModelType
        {
            get
            {
                return ModelType.Substance;
            }
        }

        public List<string> SubstanceNames { get; set; }

        public List<string> ConsumptionMethods { get; set; }

        public List<string> Units { get; set; }
    }
}
