using System;


namespace p2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = { 1, 4, 5, 14, 13,98 };
            int max = a[0];
            int min = a[0];
            double sum = 0;
            double ave = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] > max)
                {
                    max = a[i];
                }
                if (a[i] < min)
                {
                    min = a[i];
                }
                sum = sum + a[i];
                ave = sum / a.Length;
            }

            Console.Write("max:"+sum+"\n"+"min:"+min + "\n" + "sum:"+sum + "\n"+"ave:" +ave);
        }
    }
}