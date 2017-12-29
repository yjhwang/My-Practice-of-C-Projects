using System;

namespace ASCIItable
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.Write("\n    ");
            for (int i = 0; i <= 15; i++) Console.Write("   {0:X}", i);

            for (int i = 32; i <= 126; i++)
            {
                if (i % 16 == 0) Console.Write("\n{0:X}  ", i);
                Console.Write("   {0}", (char)i);

            }
        }
    }
}
