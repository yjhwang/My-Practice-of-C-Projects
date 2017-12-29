using System;

namespace method1
{
    class MainClass
    {
        public static double MyBMI(int height, int weight)
        {
			//BMI值計算公式:    BMI = 體重(公斤) / 身高平方(公尺)
			double h = height / 100.0;
            double bmi = weight / (h * h);
            return bmi;
        }
        public static void Main(string[] args)
        {
            Console.Write("Please input your height(cm):");
            int h1 = int.Parse(Console.ReadLine());
			Console.Write("Please input your Weight(kg):");
            int w1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Your BMI = {0}", MyBMI(h1, w1));

            Console.Read();
        }
    }
}
