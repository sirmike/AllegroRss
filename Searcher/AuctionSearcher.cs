using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AllegroFinder.Interfaces;
using AllegroFinder.Base;
using AllegroFinder.Data;
using System.Threading;
using System.Web.Services.Protocols;

namespace AllegroFinder.Searcher
{
    public class AuctionSearcher : ConnectorBase
    {
        private string sessionKey = null;

        public string Title { get; set; }
        public bool OnlyNew { get; set; }

        public AuctionSearcher(IAllegroServiceProvider provider, string sessionKey) : base(provider)
        {
            if (string.IsNullOrEmpty(sessionKey))
            {
                throw new ArgumentNullException("sessionKey", "this argument cannot be null or empty");
            }
            this.sessionKey = sessionKey;
        }

        public MethodResult DoSearch(out List<Auction> auctionsFound)
        {
            MethodResult result = new MethodResult();

            try
            {
                auctionsFound = serviceProvider.Search(sessionKey, Title, null, null, OnlyNew);
            }
            catch(SoapException ex)
            {
                result.ErrorMessage = ex.Message;
                result.ErrorCode = ex.Code.Name;
                auctionsFound = new List<Auction>();
            }

            return result;
        }
    }
}
