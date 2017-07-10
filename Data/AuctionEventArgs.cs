using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllegroFinder.Data
{
    public class AuctionEventArgs : EventArgs
    {
        public List<Auction> Auctions { get; set; }

        public AuctionEventArgs(List<Auction> auctions)
        {
            this.Auctions = auctions;
        }
    }
}
