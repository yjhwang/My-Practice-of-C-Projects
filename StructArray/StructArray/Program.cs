using System;

namespace StructArray
{
    class MainClass
    {
		struct students //宣告結構
		{
			public string num;
			public string name;
			public int score;
		}
        public static void Main(string[] args)
        {
            students[] csharp = new students[30];

			Random r = new Random();

			for (int i = 0; i <= 29; i++)
			{
				csharp[i].score = r.Next(50, 101); // 模擬產生50-100的分數
				csharp[i].num= "A106" + i.ToString("00#");
                csharp[i].name = "Jack" + i.ToString("00#");
			}
			Console.WriteLine("排序前：");
			for (int i = 0; i <= 29; i++)
			{
				Console.Write("{0} = {1}\t", csharp[i].name, csharp[i].score);
				if ((i + 1) % 5 == 0) Console.WriteLine();
			}
			Console.WriteLine("\n排序後：");

            // 泡沫排序法

            for (int i = 0; i <= 28; i++)
            {
                for (int j = i + 1; j <= 29; j++)
                {
                    if (csharp[i].score < csharp[j].score) 
                    {
                        students temp = csharp[i];
                        csharp[i] = csharp[j];
                        csharp[j] = temp;
                    }
                }
            }
			

			for (int i = 0; i <= 29; i++)
			{
				Console.Write("{0} = {1}\t", csharp[i].name, csharp[i].score);
				if ((i + 1) % 5 == 0) Console.WriteLine();
			}
        
        }
    }
}
