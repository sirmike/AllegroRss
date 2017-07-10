using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Serialization.Formatters;
using System.ServiceModel;

using AllegroFinder.Interfaces;
using AllegroFinder.Enums;

namespace AllegroFinder.Data
{
    public class AllegroServiceProvider : IAllegroServiceProvider
    {
        private AllegroWebApiService service = new AllegroWebApiService();

        private long GetVersion(int countryCode, string apiKey)
        {
            long verKey = 0;
            service.doQuerySysStatus((int)SysVar.AllegroWebApi, countryCode, apiKey, out verKey);
            return verKey;
        }

        public static SearchOptType PrepareSearchQuery(string title, decimal? priceMin, decimal? priceMax, bool onlyNew)
        {
            SearchOptType searchQuery = new SearchOptType();

            searchQuery.searchcategory = AppSettings.Default.SearchCategory;
            if (priceMin.HasValue)
            {
                searchQuery.searchpricefrom = (float)priceMin.Value;
            }

            if (priceMax.HasValue)
            {
                searchQuery.searchpriceto = (float)priceMax.Value;
            }

            searchQuery.searchstring = title;
            searchQuery.searchoptions = 0;
            if (onlyNew)
            {
                searchQuery.searchoptions += 4;
            }

            return searchQuery;

        }

        #region IAllegroServiceProvider Members

        public string Login(string user, string password, int countryCode, string apiKey)
        {
            long verKey = GetVersion(countryCode, apiKey);
            int hashOffset;
            long serverTime;

            return service.doLogin(user, password, countryCode, apiKey, verKey, out hashOffset, out serverTime);
        }

        public List<Auction> Search(string sessionHandle, string title, decimal? priceMin, decimal? priceMax, bool onlyNew)
        {
            const int maxQueryResultLength = 50;

            SearchOptType query = PrepareSearchQuery(title, priceMin, priceMax, onlyNew);

            int featuredAuctionsCount = 0;
            SearchResponseType []response = null;

            List<Auction> result = new List<Auction>();

            int currentOffset = -1;
            while (response == null || response.Length == maxQueryResultLength)
            {
                query.searchoffset = ++currentOffset;

                service.doSearch(sessionHandle, query, out featuredAuctionsCount, out response);
                foreach (SearchResponseType responseType in response)
                {
                    Auction auction = new Auction();
                    auction.Name = responseType.sitname;
                    auction.Price = Convert.ToDecimal(responseType.sitprice);
                    auction.Offers = responseType.sitbidcount;
                    auction.Id = responseType.sitid;
                    auction.TimeLeft = TimeSpan.FromSeconds(responseType.sittimeleft);
                    if (responseType.sitisbuynow == 1)
                    {
                        auction.BuyNowPrice = Convert.ToDecimal(responseType.sitbuynowprice);
                    }
                    result.Add(auction);

                }
            }

            return result;
        }

        #endregion
    }
}
