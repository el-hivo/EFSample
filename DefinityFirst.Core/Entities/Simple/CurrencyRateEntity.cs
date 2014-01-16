using System;

namespace DefinityFirst.Core.Entities.Simple
{
    public class CurrencyRateEntity
    {
        public string FromCurrencyCode { get; set; }
        public string FromCurrencyName { get; set; }
        public string ToCurrencyCode { get; set; }
        public string ToCurrencyName { get; set; }
        public decimal AverageRate { get; set; }
        public decimal EndOfDayRate { get; set; }
        public DateTime RateDate { get; set; }
    }
}
