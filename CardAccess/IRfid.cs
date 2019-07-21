using System;
using System.Collections.Generic;
using System.Text;

namespace CardAccess
{
    interface IRfid
    {
        IList<string> Read();
        void Write(string content);
    }
}
