using System;

namespace dice2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int d1, d2;
            int m = 13;
            int[] dice = new int[m]; //宣告整數陣列
            Random r = new Random();
			int k;
            int[] aryn = new int[] { 1000, 5000, 10000, 20000 };
            foreach (int n in aryn)
            {
               
                Console.WriteLine("n = {0}", n);
                for (int i = 0; i <= 12; i++) dice[i] = 0; // 歸零
                for (int i = 1; i <= n; i++)
                {
                    d1 = r.Next(1, 7);
                    d2 = r.Next(1, 7);
                    k = d1 + d2;
                    // Console.WriteLine("{0} + {1} = {2}", d1, d2, k);
                    dice[k]++;
                }
                for (k = 2; k <= 12; k++)
                {
                    double p = (double)dice[k] / n * 100;
                    Console.Write("dice[{0}] = {1}\t {2:F1}% \t", k, dice[k], p);
                    int star = (int)(p * 4);
                    for (int i = 1; i <= star; i++) Console.Write("*");
                    Console.WriteLine();

                }
            }
            Console.Read();
        }
    }
}
