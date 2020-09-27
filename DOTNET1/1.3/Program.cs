using System;

namespace _1._3
{

        class Program
        {
            static void Main(string[] args)
            {
                Eratosthenes era = new Eratosthenes();
                era.init();
                era.show();
                string stop = Console.ReadLine();
            }
        }

        //构造函数初始化数组并计算
        class Eratosthenes
        {
            int[] array = new int[100];
            public Eratosthenes() { }

            public void init()
            {
                for (int i = 1; i < 101; i++) array[i - 1] = i;
                this.calculate();
            }

            //检验是否可被整除
            private void calculate()
            {
                for (int i = 2; i < 100; i++)
                {
                    for (int j = i; j < 100; j++)
                    {
                        if ((array[j] != 0) && (array[j] % i == 0)) array[j] = 0;
                    }
                }
            }

            public void show()
            {
                foreach (int a in array)
                    if (a != 0)
                        Console.Write(a + " ");
            }
        }

}
