using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
