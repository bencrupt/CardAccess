using System;
using System.Collections.Generic;
using System.Text;

namespace CardAccess
{
    interface IRelay
    {
        bool IsOn { get; set; }

        void TurnOn();
        void TurnOff();
    }
}
