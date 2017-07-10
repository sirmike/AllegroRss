using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AllegroFinder.Interfaces;

namespace AllegroFinder.Base
{
    public abstract class ConnectorBase
    {
        protected IAllegroServiceProvider serviceProvider;

        public ConnectorBase(IAllegroServiceProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider", "this parameter cannot be null");
            }

            this.serviceProvider = provider;
        }
    }
}
