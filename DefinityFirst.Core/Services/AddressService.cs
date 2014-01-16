using System.Collections.Generic;
using System.Linq;
using DefinityFirst.Core.Entities.Listings;
using DefinityFirst.Sample.Data;

namespace DefinityFirst.Core.Services
{
    public class AddressService
    {

        public List<AddressEntityListItem> GetAddressesThatHaveSalesOrders(string city, string postalCode, string stateProvinceName, string countryRegionName, int pageNumber, int pagseSize, out int totalRows)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                IQueryable<AddressEntityListItem> query =
                    (from add in db.Address
                     let state = add.StateProvince
                     let country = state.CountryRegion
                     where add.SalesOrderHeader.Any()
                     select new AddressEntityListItem
                     {
                        AddressLine1 = add.AddressLine1,
                        City = add.City,
                        CountryRegionCode = country.CountryRegionCode,
                        CountryRegionName = country.Name,
                        PostalCode = add.PostalCode,
                        StateProvinceCode = state.StateProvinceCode,
                        StateProvinceName = state.Name
                     });

                if (!string.IsNullOrWhiteSpace(city))
                {
                    query = query.Where(a => a.City.StartsWith(city));
                }

                if (!string.IsNullOrWhiteSpace(postalCode))
                {
                    query = query.Where(a => a.PostalCode == postalCode);
                }

                if (!string.IsNullOrWhiteSpace(stateProvinceName))
                {
                    query = query.Where(a => a.StateProvinceName.StartsWith(stateProvinceName));
                }

                if (!string.IsNullOrWhiteSpace(countryRegionName))
                {
                    query = query.Where(a => a.CountryRegionName.StartsWith(countryRegionName));
                }

                totalRows = query.Count();

                return query.Skip(pagseSize*(pageNumber - 1)).Take(pagseSize).ToList();
            }
        }

    }
}
