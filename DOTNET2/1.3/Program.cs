using System;

namespace p3
{
    class Program
    {
		static void Main(string[] args)
		{
			ModifyNode<int> list = new ModifyNode<int>();
			for (int i = 1; i <= 10; i++) list.Add(i);
			int max = list.first.Data;
			int min = list.first.Data;
			int sum = 0;
			list.ForEach((x) =>
			{
				Console.WriteLine(x);
				max = Math.Max(max, x);
				min = Math.Min(min, x);
				sum += x;
			});
			Console.WriteLine("Max:"+max+"*****Min:"+min+"*****Sum:"+sum);
		}
	}
}
