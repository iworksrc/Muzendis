using System.Collections.Generic;
using IO.Swagger.Models;
using Muzendis.Context;

namespace Muzendis
{
    public class InMemoryTemporaryStorage : ICountriesContext
    {

        private ICollection<Country> db = new List<Country>();

        public bool CreateCountry(Country country)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteCountry(int id)
        {
            throw new System.NotImplementedException();
        }

        public Country GetCountry(int id)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Country> SearchCountries(string countryName, string term)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateCountry(Country country)
        {
            throw new System.NotImplementedException();
        }
    }
}