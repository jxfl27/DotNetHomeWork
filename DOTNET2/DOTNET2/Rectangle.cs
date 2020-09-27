using System;
using System.Drawing;

namespace p1
{
    class Rectangle : shape
    {
        double length { get; set; }
        double width  { get; set; }
        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
        }

        public override void Init()
        {
            string s = "";
            Console.WriteLine("PLEASE ENTER THE LENGTH:");
            s = Console.ReadLine();
            length = double.Parse(s);
            Console.WriteLine("PLEASE ENTER THE WIDTH:");
            s = Console.ReadLine();
            width = double.Parse(s);
        }
        public override bool judge()
        {
            if (length > 0 && width > 0)
            {
                Console.WriteLine("TRUE");
                return true;
            }
            Console.WriteLine("FALSE");
            return false;
        }
        public override double Area()
        {
            if (judge()) return length * width;
            else return 1;
        }
    }
}
