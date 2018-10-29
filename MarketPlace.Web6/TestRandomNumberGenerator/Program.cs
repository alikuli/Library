using AliKuli.UtilitiesNS.RandomNumberGeneratorNS;
using System;

namespace TestRandomNumberGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var gen = new RandomNoGenerator(5);
            var nosLst = gen.GenerateNumbers(10, 10000);
            int count = 0;
            foreach (var item in nosLst)
            {
                count++;
                Console.WriteLine("{1}. Number = {0}", item, count);

            }

            Console.WriteLine("Program Ended!");
            Console.ReadLine();

        }
    }
}
