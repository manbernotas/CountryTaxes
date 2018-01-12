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

        /// <summary>
        /// Returns all countries names list
        /// </summary>
        /// <returns></returns>
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
            decimal tax = 0;

            if (country != null && date != DateTime.MinValue)
            {
                tax = country.Taxes
                .OrderBy(t => t.StartDate)
                .ThenByDescending(t => t.EndDate)
                .Last(t => t.StartDate <= date && t.EndDate >= date)
                .TaxRate;
            }

            return tax;
        }

        /// <summary>
        /// Creates country object if same does not exist in the database
        /// and calls repository method to save it to database
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public bool CreateCountry(string countryName)
        {
            var countryList = repository.GetCountryList();

            if (countryList.FirstOrDefault(country => country.Name == countryName) == null)
            {
                var country = new Country()
                {
                    Name = countryName,
                };

                if (repository.SaveCountry(country))
                {
                    return true;
                }
            }

            return false; 
        }

        /// <summary>
        /// Returns countryId
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public int GetCountryId(string countryName)
        {
            var countryList = repository.GetCountryList();
            var countryId = 0;
            try
            {
                countryId = countryList.FirstOrDefault(country => country.Name == countryName).Id;
            }
            catch(Exception)
            {
                return countryId;
            }

            return countryId;
        }

        /// <summary>
        /// Creates tax object and calls repository method to save it to database
        /// </summary>
        /// <param name="countryName"></param>
        /// <param name="startDate"></param>
        /// <param name="period"></param>
        /// <param name="taxRate"></param>
        public bool CreateTaxSchedule(string countryName, DateTime startDate, string period, decimal taxRate)
        {
            var countryId = GetCountryId(countryName);

            if (countryId != 0 && startDate != DateTime.MinValue)
            {
                var endDate = new DateTime();

                switch (period.ToLower())
                {
                    case "yearly":
                        endDate = startDate.AddYears(1).AddDays(-1);
                        break;
                    case "monthly":
                        endDate = startDate.AddMonths(1).AddDays(-1);
                        break;
                    case "weekly":
                        endDate = startDate.AddDays(6);
                        break;
                    case "daily":
                        endDate = startDate;
                        break;
                    default:
                        endDate = startDate;
                        break;
                }

                var tax = new Tax()
                {
                    CountryId = countryId,
                    StartDate = startDate,
                    EndDate = endDate,
                    TaxRate = taxRate,
                };

                if (repository.SaveTax(tax))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
