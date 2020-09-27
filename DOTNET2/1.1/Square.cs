using System;
using System.Collections.Generic;
using System.Text;

namespace p1
{
    class Square:shape
    {
        double side { get; set; }
        public Square(double s)
        {
            this.side = s;
        }
        public override void Init()
        {
            string s = "";
            Console.WriteLine("PLEASE ENTER THE LENGTH OF SIDE:");
            s = Console.ReadLine();
            side = double.Parse(s);
        }
        public override bool judge()
        {
            if (side > 0)  return true;
            else return false;
        }
        public override double Area()
        {
            if (judge()) return side * side;
            return 1;
        }

    }
}
