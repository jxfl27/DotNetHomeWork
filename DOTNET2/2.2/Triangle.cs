using System;
using System.Collections.Generic;
using System.Text;

namespace p2
{
    class Triangle:shape
    {
        double side1{ get; set; }
        double side2 { get; set; }
        double side3 { get; set; }

    public Triangle(double s1,double s2, double s3)
        {
            this.side1 = s1;
            this.side2 = s2;
            this.side3 = s3;
        }

        public override void Init()
        {
            string s = "";
            Console.WriteLine("PLEASE ENTER THE FIRST SIDE:");
            s = Console.ReadLine();
            side1 = double.Parse(s);
            Console.WriteLine("PLEASE ENTER TEH SECOND SIDE ");
            s = Console.ReadLine();
            side2 = double.Parse(s);
            Console.WriteLine("PLEASE ENTER THE THIRD SIDE");
            s = Console.ReadLine();
            side3 = double.Parse(s);
        }

        public override bool judge()
        {
            if ((side1 + side2 > side3) && (side1 + side3 > side2) && (side2 + side3 > side1) && side1 > 0 && side2 > 0 && side3 > 0)
            {
                Console.WriteLine("TRUE");
                return true;
            }
            Console.WriteLine("FALSE");
            return false;
        }
        public override double Area()
        {
            if (judge())
            {
                double p = (side1 + side2 + side3) / 2;
                return Math.Sqrt(p * (p - side1) * (p - side2) * (p - side3));
            }
            return 1;
        }
    }
}
