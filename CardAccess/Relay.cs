using System;
using System.Collections.Generic;
using System.Text;

namespace CardAccess
{
    class Relay : IRelay
    {
        private int controlPin;
        private bool? isOn;
        private IPythonAccess pa;

        public Relay(int controlPin)
        {
            this.controlPin = controlPin;
            pa = new PythonAccess();
        }
        public void TurnOff()
        {
            if (!isOn.HasValue || isOn.Value)
                pa.Execute(ScriptName.Off, controlPin.ToString());
            isOn = false;
        }

        public void TurnOn()
        {
            if (!isOn.HasValue || !isOn.Value)
                pa.Execute(ScriptName.On, controlPin.ToString());
            isOn = true;
        }
    }
}
