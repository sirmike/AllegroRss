using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllegroFinder.Exceptions;

namespace AllegroFinder.Data
{
    public class Auction : IEquatable<Auction>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? BuyNowPrice { get; set; }
        public TimeSpan TimeLeft { get; set; }
        public bool IsNew { get; set; }
        public int Offers { get; set; }

        public Auction()
        {
            IsNew = false;
        }

        public string AllegroLink
        {
            get
            {
                return string.Format("http://www.allegro.pl/show_item.php?item={0}", Id);
            }
        }

        public string TimeLeftAsString
        {
            get
            {
                return GetTimeLeftAsString(TimeLeft);
            }
        }

        public static string GetTimeLeftAsString(TimeSpan span)
        {
            if (span.Days > 0)
            {
                if (span.Days == 1)
                {
                    return "1 dzień";
                }
                else
                {
                    return string.Format("{0} dni", span.Days);
                }
            }
            if (span.Hours > 0)
            {
                return string.Format("{0} godz.", span.Hours);
            }
            if (span.Minutes > 0)
            {
                return string.Format("{0} min.", span.Minutes);
            }

            return "Mniej niż minuta";
        }

        public void Validate()
        {
            if (Id == 0)
            {
                throw new AuctionValidationException("Id");
            }
            if (string.IsNullOrEmpty(Name))
            {
                throw new AuctionValidationException("Name");
            }
        }

        #region IEquatable<Auction> Members

        public bool Equals(Auction other)
        {
            return Id == other.Id;
        }

        #endregion
    }
}
