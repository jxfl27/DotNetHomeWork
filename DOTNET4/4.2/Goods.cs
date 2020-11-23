using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class Goods
    {
        public string GoodsName { get; } = "";
        public double Price { get; } = 0;

        public Goods(string name, double price)
        {
            this.GoodsName = name;
            this.Price = price;
        }
    }
}
