using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;

namespace CardAccess
{
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Option(Description = "Pin number to control relay", ShortName = "p")]
        [Required]
        public int Pin { get; }
        [Option(Description = "Machine ID", ShortName = "m")]
        [Required]
        public string MachineId { get; }
        [Option(Description = "Admin mode", ShortName = "a")]
        public bool Admin { get; }

        private void OnExecute()
        {
            using (var log = new Log())
            {
                log.Write($"Starting up with pin {Pin.ToString()} on machine ID {MachineId}{(Admin ? " in admin mode" : string.Empty)}.");
                DateTime adminModeStarted = DateTime.MinValue;
                IRfid rfid = new Rfid();
                IRelay relay = new Relay(Pin); //17
                while (true)
                {
                    var contentCol = rfid.Read();
                    if (contentCol.Count > 1)
                    {
                        var tag = contentCol[0];
                        var content = contentCol[1];
                        if (!string.IsNullOrWhiteSpace(tag))
                        {
                            if (content.Contains(MachineId))
                            {
                                if (!relay.IsOn)
                                {
                                    relay.TurnOn();
                                    log.Write($"Machine {MachineId} turned on with tag {tag}.");
                                }
                            }
                            else
                            {
                                if (DateTime.Now < adminModeStarted.AddSeconds(5))
                                {
                                    // Someone without access try to access machine within 5 seconds since admin removed its tag, access is granted
                                    content += MachineId;
                                    rfid.Write(content);
                                    log.Write($"Access to machine ID {MachineId} was granted to tag {tag}.");
                                }
                                if (relay.IsOn)
                                {
                                    relay.TurnOff();
                                    log.Write($"Machine ID {MachineId} turned off.");
                                }
                            }

                            if (content.Contains("^"))
                            {
                                adminModeStarted = DateTime.Now;
                            }
                        }
                        else
                        {
                            if (relay.IsOn)
                            {
                                relay.TurnOff();
                                log.Write($"Machine ID {MachineId} turned off.");
                            }
                        }
                    }
                    else
                    {
                        if (relay.IsOn)
                        {
                            relay.TurnOff();
                            log.Write($"Machine ID {MachineId} failed to read tag and turned off.");
                        }
                        else
                        {
                            log.Write($"Machine ID {MachineId} failed to read tag.");
                        }
                    }
                    Thread.Sleep(TimeSpan.FromMilliseconds(200));
                }
            }
        }
    }
}
