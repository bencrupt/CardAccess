using System;
using System.Collections.Generic;
using System.Text;

namespace CardAccess
{
    class Card
    {
        public Card()
        {
            Access = new List<Access>();
        }
        public bool IsAdmin { get; set; }
        public IList<Access> Access { get; set; }
    }
}
