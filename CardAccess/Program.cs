using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CardAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            IRfid rfid = new Rfid();
            IRelay relay = new Relay(int.Parse(Properties.Resources.RelayPin));
            IList<IMember> memberCol = Member.Refresh();
            //relay.TurnOn();
            var isOdd = false;
            while (true)
            {
                var contentCol = rfid.Read();
                if (contentCol.Count > 1)
                {
                    var tag = contentCol[0];
                    var content = contentCol[1];
                    var member = memberCol.Where(o => o.TagNumber == tag).FirstOrDefault();
                    if (member != null)
                    {
                        if (member.Access.Any(o => o.Equals(Properties.Resources.MachineName)))
                        {
                            relay.TurnOn();
                            Console.Write($"Operated by {member.FirstName} {member.LastName}");
                        }
                        else
                        {
                            relay.TurnOff();
                            Console.Write($"{member.FirstName} {member.LastName} does not have access");
                        }
                    }
                    else
                    {
                        relay.TurnOff();
                        Console.Write("No operator");
                    }
                }
                else
                {
                    relay.TurnOff();
                    Console.Write("Failed reading tag");
                }
                if (isOdd)
                    Console.Write(".");
                Console.Write("                                         \r");
                isOdd = !isOdd;
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
    }
}
