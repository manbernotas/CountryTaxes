using System;
using System.Collections.Generic;
using CountryTaxes.BL;
using Microsoft.AspNetCore.Mvc;

namespace CountryTaxes.Controllers
{
    [Route("api/[controller]")]
    public class TaxesController : Controller
    {
        private readonly DAL.TaxContext context;
        private TaxManager taxManager;

        public TaxesController(DAL.TaxContext context)
        {
            this.context = context;
            taxManager = new TaxManager(this.context);
        }
        // GET api/taxes
        [HttpGet]
        public List<string> GetCountryNamesList()
        {
            return taxManager.GetCountryNamesList();
        }

        // GET api/taxes/Estonia
        [HttpGet("{countryName}")]
        public List<DAL.Tax> GetCountryTaxes(string countryName)
        {
            return taxManager.GetCountryTaxes(countryName);
        }

        // GET api/taxes/Estonia/2016-05-04
        [HttpGet("{countryName}/{date}")]
        public decimal GetCountryTaxesAtDate(string countryName, DateTime date)
        {
            return taxManager.GetCountryTaxAtDate(countryName, date);
        }

        // POST api/taxes/schedule/Estonia/2016-05-04/monthly/0.5
        [HttpPost("schedule/{country}/{startDate}/{period}/{taxRate}")]
        public IActionResult Post([FromRoute]string country,
            [FromRoute]DateTime startDate,
            [FromRoute]string period,
            [FromRoute]decimal taxRate)
        {
            if (taxManager.CreateTaxSchedule(country, startDate, period, taxRate))
            {
                return StatusCode(200);
            }

            return StatusCode(400);
        }

        // POST api/taxes/create/Estonia
        [HttpPost("create/{country}")]
        public IActionResult Post([FromRoute]string country)
        {
            if (taxManager.CreateCountry(country))
            {
                return StatusCode(200);
            }

            return StatusCode(400);
        }
    }
}
