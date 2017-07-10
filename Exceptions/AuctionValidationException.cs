using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllegroFinder.Exceptions
{
    public class AuctionValidationException : Exception
    {
        private string fieldName;

        public string FieldName
        {
            get
            {
                return fieldName;
            }
        }

        public AuctionValidationException(string fieldName)
            : base(string.Format("Auction validation failed - field {0} has wrong value.", fieldName))
        {
            this.fieldName = fieldName;
        }
    }
}
