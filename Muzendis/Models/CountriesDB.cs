using IO.Swagger.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muzendis.Context
{
    public class CountriesDB : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        public CountriesDB(DbContextOptions<CountriesDB> options)
            : base(options)
        {
        }
    }
}
