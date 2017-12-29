using System;

namespace ArraySort
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int[] score = new int[30];
            string[] stu_num = new string[30];
            Random r = new Random();

            for (int i = 0; i <= score.GetUpperBound(0); i++)
            {
                score[i] = r.Next(50, 101); // 模擬產生50-100的分數
                stu_num[i] = "Stu_" + i.ToString("0#");
            }
			Console.WriteLine("排序前：");
            for (int i = 0; i <= 29; i++)
            {
                Console.Write("{0} = {1}\t", stu_num[i], score[i]);
                if ((i + 1) % 5 == 0) Console.WriteLine(); 
            }
            Console.WriteLine("\n排序後：");
           
            Array.Sort(score,stu_num); // 依score由小到大排序
			for (int i = 0; i <= 29; i++)
			{
				Console.Write("{0} = {1}\t", stu_num[i], score[i]);
				if ((i + 1) % 5 == 0) Console.WriteLine();
			}


        }
    }
}
