using McMaster.Extensions.CommandLineUtils;
//using Serilog;
//using Serilog.Sinks.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CardAccess
{
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Option(Description = "Pin number to control Relay", ShortName = "p")]
        [Required]
        public int Pin { get; }
        [Option(Description = "Machine name", ShortName = "m")]
        [Required]
        public string Machine { get; }
        [Option(Description = "Admin mode", ShortName = "a")]
        public bool Admin { get; }

        private void OnExecute()
        {
            //Log.Logger = new LoggerConfiguration().WriteTo.File("log-.txt", rollingInterval: RollingInterval.Month).CreateLogger();
            Console.WriteLine($"Starting up with pin {Pin.ToString()} on machine {Machine}{(Admin ? " in admin mode" : string.Empty)}.");
            DateTime adminModeStarted = DateTime.MinValue;
            IRfid rfid = new Rfid();
            IRelay relay = new Relay(Pin); //17
            var c = new Card();
            c.IsAdmin = true;
            Console.WriteLine(JsonSerializer.ToString(c, typeof(Card)));
            relay.TurnOff();
            bool isOn = false;
            //IList<IMember> memberCol = Member.Refresh();
            while (true)
            {
                var contentCol = rfid.Read();
                if (contentCol.Count > 1)
                {
                    var tag = contentCol[0];
                    if (tag.Trim() == "None")
                        tag = string.Empty;
                    var content = contentCol[1].Trim();
                    if (!string.IsNullOrWhiteSpace(tag))
                    {
                        if (content.Contains(Machine))
                        {
                            if (!isOn)
                            {
                                relay.TurnOn();
                                Console.WriteLine($"Machine {Machine} turned on with tag {tag}.");
                                isOn = true;
                            }
                        }
                        else
                        {
                            if (DateTime.Now < adminModeStarted.AddSeconds(5))
                            {
                                // Someone without access try to access machine within 5 seconds since admin removed its tag, access is granted
                                content = $"{content}*{Machine}";
                                Console.WriteLine(content);
                                rfid.Write(content);
                                Console.WriteLine($"Access to machine {Machine} was granted to tag {tag}.");
                            }
                            if (isOn)
                            {
                                relay.TurnOff();
                                Console.WriteLine($"Machine {Machine} turned off.");
                                isOn = false;
                            }
                        }

                        if (content.StartsWith("^"))
                        {
                            adminModeStarted = DateTime.Now;
                        }
                    }
                    else
                    {
                        if (isOn)
                        {
                            relay.TurnOff();
                            isOn = false;
                        }
                        Console.WriteLine($"Machine {Machine} has no tag.");
                    }
                }
                else
                {
                    if (isOn)
                    {
                        relay.TurnOff();
                        Console.WriteLine($"Machine {Machine} failed to read tag and turned off.");
                        isOn = false;
                    }
                    else
                    {
                        Console.WriteLine($"Machine {Machine} failed to read tag.");
                    }
                }
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
            }
        }
    }
}
