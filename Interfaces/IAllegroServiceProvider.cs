using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllegroFinder.Data;

namespace AllegroFinder.Interfaces
{
    public interface IAllegroServiceProvider
    {
        string Login(string user, string password, int countryCode, string apiKey);
        List<Auction> Search(string sessionHandle, string title, decimal? priceMin, decimal? priceMax, bool onlyNew);
    }
}
