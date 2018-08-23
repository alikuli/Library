using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AliKuli.Extentions;
using AliKuli;
using AliKuli.UtilitiesNS;

namespace TestingPhoneNumbersConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PhoneNumbersUtility util = new PhoneNumbersUtility("03314474120", "PK");
            Console.WriteLine("Phone Number {0}",util.CompletePhoneNumber);
            Console.WriteLine();
            Console.WriteLine("***  END ******");
            Console.ReadLine();
        }
    }
}
