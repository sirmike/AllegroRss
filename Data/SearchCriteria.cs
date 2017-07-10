using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllegroFinder.Data
{
    public class SearchCriteria
    {
        public bool ShowBaloon { get; set; }
        public bool ClearBeforeSearch { get; set; }
    }

    public class SearchResult
    {
        public List<Auction> Auctions { get; set; }
        public bool ShowBaloon { get; set; }
        public MethodResult AllegroMethodResult { get; set; }
    }
}
