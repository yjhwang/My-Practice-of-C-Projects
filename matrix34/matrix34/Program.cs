using System;

namespace matrix34
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int[,] a = new int[,] { { 10, 20, 30, 40 }, { 5, 15, 25, 35 }, { 12, 24, 36, 48 } };
            for (int i = 0; i < 3; i++)  // 0, 1, 2
            {
                for (int j = 0; j < 4; j++) // 0, 1, 2, 3
                {
                    Console.Write("a[{0},{1}] = {2}, \t", i, j, a[i, j]);
                }
                Console.WriteLine("\n");
           }
			Console.WriteLine("a.GetUpperBound(0) = {0}", a.GetUpperBound(0));
			Console.WriteLine("a.GetUpperBound(1) = {0}", a.GetUpperBound(1));
			Console.Read();
        }
    }
}
