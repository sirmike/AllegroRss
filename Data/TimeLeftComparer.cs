using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllegroFinder.Data
{
    class TimeLeftComparer : IComparer<Auction>
    {
        #region IComparer<Auction> Members

        public int Compare(Auction x, Auction y)
        {
            return x.TimeLeft.CompareTo(y.TimeLeft);
        }

        #endregion
    }
}
