using System.Collections.Generic;

namespace DefinityFirst.Core.Entities.Simple
{
    public class CurrencyEntity
    {
        public CurrencyEntity()
        {
            Rates = new List<CurrencyRateEntity>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public List<CurrencyRateEntity> Rates { get; set; } 
    }
}
