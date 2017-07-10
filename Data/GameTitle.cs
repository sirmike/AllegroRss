using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllegroFinder.Data
{
    public class GameTitle : IEquatable<GameTitle>
    {
        public string Title { get; set; }
        public bool Checked { get; set; }

        #region IEquatable<GameTitle> Members

        public bool Equals(GameTitle other)
        {
            return Title.Equals(other.Title, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
}
