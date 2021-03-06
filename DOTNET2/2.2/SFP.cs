﻿using System;
using System.Collections.Generic;
using System.Text;

namespace p2
{
    class SFP
    {
        public static shape[] create(int num)
        {
            int count = 0;
            shape[] shapes = new shape[num];
            Random rd = new Random();
            int rd1 = rd.Next(0, 10);
            int rd2 = rd.Next(0, 10 - rd1);
            int rd3 = 10 - rd1 - rd2;
            if (rd1 > 0) for (int i = 0; i < rd1; i++) { shapes[count] = new Rectangle(10 * rd.NextDouble(), 10 * rd.NextDouble()); count++; }
            if (rd2 > 0) for (int j = 0; j < rd2; j++) { shapes[count] = new Square(10 * rd.NextDouble()); count++; }
            if (rd3 > 0) for (int k = 0; k < rd3; k++) { shapes[count] = new Triangle(10 * rd.NextDouble(), 10 * rd.NextDouble(), 10 * rd.NextDouble()); count++; }
            return shapes;
        }
    }
}
