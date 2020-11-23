using System;
using System.Collections.Generic;
using System.Text;

namespace p2
{
    abstract class shape
    {
        public abstract void Init();
        public abstract bool judge();
        public abstract double Area();
    }
}
