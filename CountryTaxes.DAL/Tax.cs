using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryTaxes.DAL
{
    public class Tax
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CountryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TaxRate { get; set; }
    }
}
