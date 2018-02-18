using IO.Swagger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muzendis.Context
{
    public interface ICountriesContext
    {
        ICollection<Country> SearchCountries(string countryName, string term);
        Country GetCountry(int id);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(int id);

    }
}
