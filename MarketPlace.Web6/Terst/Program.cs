using BreadCrumbsLibraryNS.Models;
using BreadCrumbsLibraryNS.Programs;
using System;

namespace Terst
{
    class Program
    {
        static void Main(string[] args)
        {
            BreadCrumbManager bcm = new BreadCrumbManager();
            BreadCrumb bc = new BreadCrumb(@"C:\Test1\", "Home");
            BreadCrumb bc1 = new BreadCrumb(@"C:\Test2\", "First");
            BreadCrumb bc2 = new BreadCrumb(@"C:\Test3\", "Second");
            BreadCrumb bc3 = new BreadCrumb(@"C:\Test4\", "Third");
            BreadCrumb bc4 = new BreadCrumb(@"C:\Test5\", "Fourth");
            BreadCrumb bc5 = new BreadCrumb(@"C:\Test6\", "Fifth");
            BreadCrumb bc6 = new BreadCrumb(@"C:\Test7\", "Sixth");
            BreadCrumb bc7 = new BreadCrumb(@"C:\Test8\", "Seventh");
            BreadCrumb bc8 = new BreadCrumb(@"C:\Test9\", "Eighth");
            BreadCrumb bc9 = new BreadCrumb(@"C:\Test10\", "Ninth");
            BreadCrumb bc10 = new BreadCrumb(@"C:\Test11\", "Tenth");
            bcm.Push(bc);
            bcm.Push(bc1);
            bcm.Push(bc2);
            bcm.Push(bc3);
            bcm.Push(bc4);
            bcm.Push(bc5);
            bcm.Push(bc6);
            bcm.Push(bc7);
            bcm.Push(bc8);
            bcm.Push(bc9);
            bcm.Push(bc10);

            Console.WriteLine("{0} {1}", 1, bcm.Render());

            bcm.Pop();
            Console.WriteLine("{0} {1}", 1, bcm.Render());
            bcm.Pop();
            Console.WriteLine("{0} {1}", 2, bcm.Render());
            bcm.Pop();
            Console.WriteLine("{0} {1}", 3, bcm.Render());
            bcm.Pop();
            Console.WriteLine("{0} {1}", 4, bcm.Render());
            bcm.Pop();
            Console.WriteLine("{0} {1}", 5, bcm.Render());
            bcm.Pop();
            Console.WriteLine("{0} {1}", 6, bcm.Render());
            bcm.Pop();
            Console.WriteLine("{0} {1}", 7, bcm.Render());
            bcm.Pop();
            Console.WriteLine("{0} {1}", 8, bcm.Render());
            bcm.Pop();
            Console.WriteLine("{0} {1}", 9, bcm.Render());
            bcm.Pop();
            Console.WriteLine("{0} {1}", 10, bcm.Render());
            bcm.Pop();
            Console.WriteLine("{0} {1}", 11, bcm.Render());
            bcm.Pop();
            Console.WriteLine("{0} {1}", 12, bcm.Render());
            Console.WriteLine("*** END ***");
            Console.ReadLine();

        }


    }
}
