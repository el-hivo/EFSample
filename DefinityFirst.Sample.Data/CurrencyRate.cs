//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DefinityFirst.Sample.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class CurrencyRate
    {
        public CurrencyRate()
        {
            this.SalesOrderHeader = new HashSet<SalesOrderHeader>();
        }
    
        public int CurrencyRateID { get; set; }
        public System.DateTime CurrencyRateDate { get; set; }
        public string FromCurrencyCode { get; set; }
        public string ToCurrencyCode { get; set; }
        public decimal AverageRate { get; set; }
        public decimal EndOfDayRate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual Currency FromCurrency { get; set; }
        public virtual Currency ToCurrency { get; set; }
        public virtual ICollection<SalesOrderHeader> SalesOrderHeader { get; set; }
    }
}
