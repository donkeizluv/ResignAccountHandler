using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var datetimeText = "23/04/2017";
            //DateTime.TryParseExact(datetimeText.Replace(@"//", @"/"),
            //"dd/MM/yyyy",
            //CultureInfo.InvariantCulture,
            //DateTimeStyles.None,
            //out var resignDate);


            //Console.WriteLine((DateTime.Today - resignDate).TotalDays.ToString());
            //Console.ReadLine();



            var exchangePs = new ExchangePowershellWrapper();
            Console.WriteLine("Enter to continue!");
            Console.ReadLine();
            try
            {
                var result = exchangePs.SetAutoReply("helpdesk.handler", "i am auto for auto is i");
                foreach (var line in result)
                {
                    Console.WriteLine(line.ToString());
                }
                Console.WriteLine("Done!");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                Console.ReadLine();
            }




        }
    }
}
