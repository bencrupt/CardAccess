using System;
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
            if (res != null && res.Count > 1)
            {
                res[0] = res[0].Trim();
                res[1] = res[1].Trim();
                if (res[0] == "None" || res[1] == "None")
                {
                    res[0] = string.Empty;
                    res[1] = string.Empty;
                }
            }
            return res;
        }

        public void Write(string content)
        {
            pa.Execute(ScriptName.Write, content);
        }
    }
}
