using System;

namespace test
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int i = 10;
            int k;
            //k = ++i;

            ++i;
            k = i;


            Console.WriteLine("k={0}, i = {1}", k, i);

        }
    }
}
