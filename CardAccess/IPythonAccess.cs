using System;
using System.Collections.Generic;
using System.Text;

namespace CardAccess
{
    interface IPythonAccess
    {
        IList<string> Execute(ScriptName script, string argument2 = "");
    }

    enum ScriptName
    {
        On,
        Off,
        Read,
        Write
    }
}
