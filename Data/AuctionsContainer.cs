using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllegroFinder.Data
{
    public class AuctionsContainer
    {
        private List<Auction> auctions = new List<Auction>();

        public List<Auction> Auctions
        {
            get
            {
                return auctions;
            }
        }

        public void Add(List<Auction> auctionsToAdd)
        {
            if (auctionsToAdd == null)
            {
                throw new ArgumentNullException("auctions", "this argument cannot be null");
            }

            foreach (Auction auction in auctionsToAdd)
            {
                if (!this.Auctions.Contains(auction))
                {
                    auction.IsNew = true;
                    this.Auctions.Add(auction);
                }
                else
                {
                    Auction found = this.Auctions.Find(delegate(Auction a) { return a.Id == auction.Id; });
                    found.BuyNowPrice = auction.BuyNowPrice;
                    found.Name = auction.Name;
                    found.Price = auction.Price;
                    found.TimeLeft = auction.TimeLeft;
                    found.IsNew = false;
                }
            }
        }

        public void Add(Auction auction)
        {
            if (auction == null)
            {
                throw new ArgumentNullException("auction", "this argument cannot be null");
            }
            if (!auctions.Contains(auction))
            {
                auction.IsNew = true;
                auctions.Add(auction);
            }
            else
            {
                Auction found = auctions.Find(delegate(Auction a) { return a.Id == auction.Id; });
                found.BuyNowPrice = auction.BuyNowPrice;
                found.Name = auction.Name;
                found.Price = auction.Price;
                found.TimeLeft = auction.TimeLeft;
                found.IsNew = false;
            }
        }

        public void Clear()
        {
            auctions.Clear();
        }

        public List<Auction> GetNewAuctions()
        {
            List<Auction> result = new List<Auction>();
            foreach (Auction a in Auctions)
            {
                if (a.IsNew)
                {
                    result.Add(a);
                }
            }
            return result;
        }
    }
}
