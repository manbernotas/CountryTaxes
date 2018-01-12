using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryTaxes.DAL
{
    public class TaxContext : DbContext
    {
        public TaxContext(DbContextOptions<TaxContext> options) : base(options) { }

        public DbSet<Country> Country { get; set; }
        public DbSet<Tax> Tax { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>();
            modelBuilder.Entity<Tax>();
        }
    }
}
