using System;

namespace Stos
{
    class Program
    {
        static void Main(string[] args)
        {
             StosWTablicy<string> s = new StosWTablicy<string>(2);
            s.Push("km");
            s.Push("aa");
            s.Push("xx");
            foreach (var x in s.ToArray())
                Console.WriteLine(x);

            foreach (var x in ((StosWTablicy<string>)s).TopToBottom)
                Console.WriteLine(x);

            var stosToArray = s.ToArray();
            var stosToArrayReadOnly = s.ToArrayReadOnly();
            stosToArray[0] = "test";

            Console.WriteLine();
        }
    }
}
