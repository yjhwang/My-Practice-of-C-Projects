using System;

namespace matrix2x3
{
    class MainClass
    {
        public static void Main(string[] args)
        {

			// 宣告矩陣 A[m,s]
			int[,] A = new int[2,3] { { 10, 20, 30 }, { 30, 40, 50 } };

            // B[s,n]
             int[,] B = new int[3,2] { { 10, 20 },{20, 30 }, { 30, 40} };
            // int[,] B = new int[3, 4] { { 10, 20, 10, 20 }, { 20, 30, 20, 30 }, { 30, 40, 30, 40 } };

            int m = A.GetUpperBound(0) + 1;
            int s = A.GetUpperBound(1) + 1;
            int n = B.GetUpperBound(1) + 1;
            // C[m,n] = A[m,s] * B[s,n]
            int[,] C = new int[m, n];

            int i, j, k;
			//Printout Matrix A
			Console.WriteLine("Matrix A:");
			Console.WriteLine("  A.GetUpperBound(0)={0}", A.GetUpperBound(0));
			Console.WriteLine("  A.GetUpperBound(1)={0}", A.GetUpperBound(1));

			for (i = 0; i < m; i++)
			{
				for (j = 0; j < s; j++)
				{
					Console.Write("  {0}", A[i, j]);
				}
				Console.WriteLine("\n");
			}

			//Printout Matrix B
            Console.WriteLine("Matrix B:");
			Console.WriteLine("  B.GetUpperBound(0)={0}", B.GetUpperBound(0));
			Console.WriteLine("  B.GetUpperBound(1)={0}", B.GetUpperBound(1));


			for (i = 0; i < s ; i++)
			{
				for (j = 0; j < n; j++)
				{
					Console.Write("  {0}", B[i, j]);
				}
				Console.WriteLine("\n");
			}
            Console.Read();
            // 矩陣相乘
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {
                    for (k = 0; k < s; k++)
                    {
                    C[i, j] += A[i, k] * B[k, j];  // 累加
                        // C[i,j] = C[i,j] + (A[i, k] * B[k, j]);

					}
                }
			}

			//Printout Matrix C
			Console.WriteLine("\nMatrix C:");
			for (i = 0; i < m; i++)
			{
				for (j = 0; j < n; j++)
				{
					Console.Write("  {0}", C[i, j]);
				}
				Console.WriteLine();
			}

        }
    }
}
