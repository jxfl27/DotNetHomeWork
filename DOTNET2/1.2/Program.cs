using System;

namespace p2
{
    class Program
    {
        static void Main(string[] args)
        {
                double sum = 0;
                shape[] arr = SFP.create(10);
                for (int i = 0; i < 10; i++)
                {
                    sum += arr[i].Area();
                }
                Console.WriteLine(sum);
        }
    }
}
