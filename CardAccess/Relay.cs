using System;
using System.Collections.Generic;
using System.Text;

namespace CardAccess
{
    class Relay : IRelay
    {
        private int controlPin;
        private IPythonAccess pa;

        public Relay(int controlPin)
        {
            this.controlPin = controlPin;
            pa = new PythonAccess();
            IsOn = true;
            TurnOff();
        }

        public bool IsOn { get; set; }
        public void TurnOff()
        {
            if (IsOn)
                pa.Execute(ScriptName.Off, controlPin.ToString());
            IsOn = false;
        }

        public void TurnOn()
        {
            if (!IsOn)
                pa.Execute(ScriptName.On, controlPin.ToString());
            IsOn = true;
        }
    }
}
