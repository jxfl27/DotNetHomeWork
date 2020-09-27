using System;

namespace p1
{
    static class Program
    {        
        static void Main(string[] args)
        {
            while (true)
            {
                int num = 0;
                num = enter();
                for (int i = 2; i < num; i++)
                {
                    if ((num % i == 0) && calc(i))  Console.WriteLine(i);
                }
            }
        }

        //输入
        static int enter()
        {
            int num;
            Console.WriteLine("please enter：");
            num = int.Parse(Console.ReadLine());
            return num;
        }

        //计算该数是否为质因数
        static bool calc(int num)
        {
            for (int i = 2; i < num; i++)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }

    }
}