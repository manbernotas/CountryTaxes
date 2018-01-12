using CountryTaxes.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountryTaxes.BL
{
    public class TaxManager
    {
        private TaxContext context;
        private Repository repository;

        public TaxManager(TaxContext context)
        {
            repository = new Repository(context);
            this.context = context;
        }

        /// <summary>
        /// Returns all specific country taxes
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public List<Tax> GetCountryTaxes(string countryName)
        {
            var countryList = repository.GetCountryList();
            var countryTaxes = countryList.FirstOrDefault(x => x.Name == countryName).Taxes;

            return countryTaxes;
        }

        public List<string> GetCountryNamesList()
        {
            var countryList = repository.GetCountryList();

            return countryList.Select(x => x.Name).ToList();
        }

        /// <summary>
        /// Returns taxes applied in certain country at the giving date
        /// </summary>
        /// <param name="countryName"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public decimal GetCountryTaxAtDate(string countryName, DateTime date)
        {
            var countryList = repository.GetCountryList();
            var country = countryList.FirstOrDefault(x => x.Name == countryName);

            var tax = country.Taxes
                .OrderBy(t => t.StartDate)
                .ThenByDescending(t => t.EndDate)
                .Last(t => t.StartDate <= date && t.EndDate >= date)
                .TaxRate;

            return tax;
        }
    }
}
