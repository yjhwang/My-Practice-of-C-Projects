using System;

namespace matrix2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
			int[,] a = new int[,] { { 3, 8, 6 }, { 7, 5 , 2} };
            int[,] b = new int[,] { { 5, 3 }, { 4, 2 }, {3, 3} };
			int[,] c = new int[3, 3];
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					c[i, j] = a[i, j] * b[j, i];
					Console.Write("c[{0},{1}]={2}\t", i, j, c[i, j]);
				}
                Console.WriteLine("\n");

			}
        }
    }
}
