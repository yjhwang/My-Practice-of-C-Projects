using System;

namespace x9
{
    class MainClass
    {
        public static void Main(string[] args)
        {
			int[,] a = new int[9,9];
			for (int i = 1; i <= 9; i++)  //  1, 2... , 9
			{
				for (int j = 1; j <= 9; j++) // 1, 2, ... , 9
				{
                    a[i - 1, j - 1] = i * j;
					Console.Write("a[{0},{1}] = {2}, \t", i, j, a[i-1, j-1]);
				}
				Console.WriteLine("\n");
			}
            //
			for (int i = 0; i <= 8; i++)  // 0, 1, 2... , 8
			{
				for (int j = 0; j <= 8; j++) // 0, 1, 2, ... , 8
				{
                    a[i, j] = (i+1) * (j+1);
					Console.Write("a[{0},{1}] = {2}, \t", i, j, a[i, j]);
				}
				Console.WriteLine("\n");
			}
			Console.Read();
        }
    }
}
