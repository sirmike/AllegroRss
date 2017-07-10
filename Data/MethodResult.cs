using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllegroFinder.Data
{
    public class MethodResult
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public bool Success
        {
            get
            {
                return string.IsNullOrEmpty(ErrorCode);
            }
        }
    }
}
