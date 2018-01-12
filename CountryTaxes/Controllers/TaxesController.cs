using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
