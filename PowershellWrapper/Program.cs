using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var exchangePs = new ExchangePowerShellWrapper("adm-hongln", "Dant@@760119");
            Console.WriteLine("Enter username to enable AutoReply:");
            var username = Console.ReadLine();
            Console.WriteLine($"Attemp to set auto reply to {username}");
            try
            {
                var pipe = exchangePs.GetAutoReplyPipe_V1(username, "bla bla bla");
                var results = pipe.Invoke();
                if (pipe.Error.Count > 0)
                {
                    var error = pipe.Error.Read() as Collection<ErrorRecord>;
                    if (error != null)
                    {
                        foreach (var er in error)
                        {
                            Console.WriteLine("[PowerShell]: Error in cmdlet: " + er.Exception.Message);
                        }
                    }
                }
                Console.WriteLine("done!");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                if(ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    Console.WriteLine(ex.InnerException.StackTrace);
                }
                Console.ReadLine();
            }

        }
    }
}
