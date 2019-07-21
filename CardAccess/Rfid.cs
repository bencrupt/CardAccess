﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CardAccess
{
    class Rfid : IRfid
    {
        private IPythonAccess pa;

        public Rfid()
        {
            pa = new PythonAccess();
        }

        public IList<string> Read()
        {
            var res = pa.Execute(ScriptName.Read);
            return res;
        }

        public void Write(string content)
        {
            pa.Execute(ScriptName.Write, content);
        }
    }
}
