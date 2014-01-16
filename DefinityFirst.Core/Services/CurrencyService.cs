using System;
using System.Linq;
using System.Data.Entity;
using DefinityFirst.Core.Entities.Simple;
using DefinityFirst.Sample.Data;

namespace DefinityFirst.Core.Services
{
    public class CurrencyService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromCurrencyCode"></param>
        /// <param name="toCurrencyCode"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public CurrencyEntity GetCurrencyWithRates(string fromCurrencyCode, string toCurrencyCode, DateTime? fromDate, DateTime? toDate)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                Currency currency = db.Currency.Find(fromCurrencyCode);

                IQueryable<CurrencyRateEntity> rates =
                    (from rate in db.CurrencyRate
                     let fromCurrency = rate.FromCurrency
                     let toCurrency = rate.ToCurrency
                     where rate.FromCurrencyCode == fromCurrencyCode
                     select new CurrencyRateEntity
                     {
                         FromCurrencyCode = fromCurrency.CurrencyCode,
                         FromCurrencyName = fromCurrency.Name,
                         ToCurrencyName = toCurrency.Name,
                         ToCurrencyCode = toCurrency.CurrencyCode,
                         AverageRate = rate.AverageRate,
                         RateDate = rate.CurrencyRateDate,
                         EndOfDayRate = rate.EndOfDayRate
                     });

                if (!string.IsNullOrWhiteSpace(toCurrencyCode))
                {
                    rates = rates.Where(r => r.ToCurrencyCode == toCurrencyCode);
                }

                if (fromDate.HasValue)
                {
                    rates = rates.Where(r => r.RateDate >= fromDate);
                }

                if (toDate.HasValue)
                {
                    rates = rates.Where(r => r.RateDate <= toDate);
                }

                return new CurrencyEntity
                {
                    Code = currency.CurrencyCode,
                    Name = currency.Name,
                    Rates = rates.ToList()
                };
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromCurrencyCode"></param>
        /// <param name="toCurrencyCode"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public CurrencyEntity GetCurrencyWithRatesExplicitLoading(string fromCurrencyCode, string toCurrencyCode, DateTime? fromDate, DateTime? toDate)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                Currency currency = db.Currency.Find(fromCurrencyCode);

                IQueryable<CurrencyRate> rates =
                    db.Entry(currency)
                      .Collection(c => c.ToCurrencyRate)
                      .Query()
                      .Include(cr => cr.ToCurrency);

                if (!string.IsNullOrWhiteSpace(toCurrencyCode))
                {
                    rates = rates.Where(r => r.ToCurrencyCode == toCurrencyCode);
                }

                if (fromDate.HasValue)
                {
                    rates = rates.Where(r => r.CurrencyRateDate >= fromDate);
                }

                if (toDate.HasValue)
                {
                    rates = rates.Where(r => r.CurrencyRateDate <= toDate);
                }

                rates.Load();

                return new CurrencyEntity
                {
                    Code = currency.CurrencyCode,
                    Name = currency.Name,
                    Rates = (from r in rates
                             select new CurrencyRateEntity
                             {
                                 AverageRate = r.AverageRate,
                                 EndOfDayRate = r.EndOfDayRate,
                                 RateDate = r.CurrencyRateDate,
                                 FromCurrencyCode = currency.CurrencyCode,
                                 FromCurrencyName = currency.Name,
                                 ToCurrencyCode = r.ToCurrencyCode,
                                 ToCurrencyName = r.ToCurrency.Name
                             }).ToList()
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromCurrencyCode"></param>
        /// <param name="toCurrencyCode"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public CurrencyEntity GetCurrencyWithRatesEagerLoading(string fromCurrencyCode, string toCurrencyCode, DateTime? fromDate, DateTime? toDate)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                IQueryable<CurrencyRate> query =
                    db.CurrencyRate
                      .Include(cr => cr.FromCurrency)
                      .Include(cr => cr.ToCurrency)
                      .Where(cr => cr.FromCurrencyCode == fromCurrencyCode)
                      .AsQueryable();

                if (!string.IsNullOrWhiteSpace(toCurrencyCode))
                {
                    query = query.Where(cr => cr.ToCurrencyCode == toCurrencyCode);
                }

                if (fromDate.HasValue)
                {
                    query = query.Where(cr => cr.CurrencyRateDate >= fromDate);
                }

                if (toDate.HasValue)
                {
                    query = query.Where(cr => cr.CurrencyRateDate <= toDate);
                }

                CurrencyRate[] rates = query.ToArray();

                if (rates.Length == 0)
                {
                    return new CurrencyEntity();
                }

                Currency fromCurrency = rates.Select(cr => cr.FromCurrency).First();

                return new CurrencyEntity
                {
                    Code = fromCurrency.CurrencyCode,
                    Name = fromCurrency.Name,
                    Rates = (from r in rates
                             select new CurrencyRateEntity
                             {
                                 AverageRate = r.AverageRate,
                                 EndOfDayRate = r.EndOfDayRate,
                                 RateDate = r.CurrencyRateDate,
                                 FromCurrencyCode = r.FromCurrencyCode,
                                 FromCurrencyName = r.FromCurrency.Name,
                                 ToCurrencyCode = r.ToCurrencyCode,
                                 ToCurrencyName = r.ToCurrency.Name
                             }).ToList()
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromCurrencyCode"></param>
        /// <param name="toCurrencyCode"></param>
        /// <param name="currencyRateDate"></param>
        /// <param name="averageRate"></param>
        /// <param name="endOfDayRate"></param>
        /// <returns></returns>
        public int CreateCurrency(string fromCurrencyCode, string toCurrencyCode, DateTime currencyRateDate, decimal averageRate, decimal endOfDayRate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currencyRateId"></param>
        /// <param name="averageRate"></param>
        /// <param name="endOfDayRate"></param>
        public void UpdateCurrencyRate(int currencyRateId, decimal averageRate, decimal endOfDayRate)
        {
            throw new NotImplementedException();
        }
    }
}
