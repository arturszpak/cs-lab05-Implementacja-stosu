using System;

namespace Stos
{
    class Program
    {
        static void Main(string[] args)
        {
            StosWTablicy<string> s = new StosWTablicy<string>(8);
            for(int i=0; i< 90; i++){
                s.Push("test");
            }

            foreach (var x in s.ToArray())
                Console.WriteLine(x);

            Console.WriteLine();
        }
    }
}
