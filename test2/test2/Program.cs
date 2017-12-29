using System;

namespace test2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int[] a = { 1, 2, 3 };
            Console.WriteLine("index = {0}", Array.IndexOf(a, 0));

        }
    }
}
