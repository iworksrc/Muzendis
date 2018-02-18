using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IO.Swagger.Models;
using Microsoft.EntityFrameworkCore;

namespace Muzendis.Context
{
    public class CountriesContext : ICountriesContext
    {
        private CountriesDB db;

        public CountriesContext(CountriesDB db)
        {
            this.db = db;
        }

        public bool CreateCountry(Country country)
        {
            try
            {
                db.Countries.Add(country);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                // some logging
                return false;
            }
            
        }

        public bool DeleteCountry(int id)
        {
            Country _country = (from c in this.db.Countries where c.Id == id select c).FirstOrDefault();
            
            if (_country == null)
            {
                return false;
            }
            else
            {
                try
                {
                    db.Countries.Remove(_country);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    // some logging
                    return false;
                }
            }
            
        }

        public Country GetCountry(int id)
        {
            return db.Countries
                .Include(country => country.Cities)
                .FirstOrDefault(country => country.Id == id);
        }

        public ICollection<Country> SearchCountries(string countryName, string term)
        {
            if(!String.IsNullOrEmpty(countryName) && !String.IsNullOrEmpty(term))
            {
                //TODO try rewrite to pure SQL

                var countries =
                    (from city in db.Cities
                     from country in db.Countries
                     where
                        city.Name.Contains(term) &&
                        country.Cities.Any(_city => _city.Name == city.Name) &&
                        country.Name == countryName
                     select country
                     )
                    .Include(country => country.Cities)
                    .ToList();

                try
                {
                    var country = countries.First();
                    country.Cities = country.Cities.Where(_city => _city.Name.ToLower().Contains(term.ToLower())).ToList();
                }
                catch (Exception ex)
                {
                    return countries;
                }

                return countries;
            }
            else if (!String.IsNullOrEmpty(countryName))
            {
                return (from country in db.Countries where country.Name == countryName select country)
                    .Include(country => country.Cities)
                    .ToList();
            }
            else if (!String.IsNullOrEmpty(term))
            {
                //TODO try rewrite to pure SQL
                
                var countries =
                    (from city in db.Cities
                     from country in db.Countries
                     where
                        city.Name.Contains(term) &&
                        country.Cities.Any(_city => _city.Name == city.Name)
                     select country
                     )
                    .Include(country => country.Cities)
                    .ToList();

                foreach(var country in countries)
                {
                    country.Cities = country.Cities.Where(_city => _city.Name.ToLower().Contains(term.ToLower())).ToList();
                }

                return countries;
            }
            else
            {
                return db.Countries
                    .Include(country => country.Cities)
                    .ToList();
            }


        }

        public bool UpdateCountry(Country country)
        {
            try
            {
                db.Countries.Update(country);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // do some logging
                return false;
            }
        }
    }
}
