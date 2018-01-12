using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryTaxes.DAL
{
    public class Country
    {
        public Country() { }

        public Country(int id, string name, List<Tax> taxes)
        {
            Id = id;
            Name = name;
            Taxes = taxes;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public List<Tax> Taxes { get; set; } = new List<Tax>();
    }
}
