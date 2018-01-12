using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CountryTaxes.DAL
{
    public class Repository
    {
        private TaxContext context;

        public Repository(TaxContext context)
        {
            this.context = context;
        }

        public List<Country> GetCountryList()
        {
            var countryList = context.Country.Include("Taxes");

            return countryList.ToList();
        }

        public bool SaveTax(Tax tax)
        {
            try
            {
                context.Tax.Add(tax);
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public bool SaveCountry(Country country)
        {
            try
            {
                context.Country.Add(country);
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return false;
            }
            

            return true;
        }
    }
}
