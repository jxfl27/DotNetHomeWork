using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class GoodsList
    {
        public List<Goods> goodList { get; } = new List<Goods>()
        {
            new Goods("苹果", 5),
            new Goods("香蕉", 4),
            new Goods("橘子", 10),
            new Goods("梨", 30),
            new Goods("草莓", 6),
            new Goods("葡萄", 7)
        };

        public GoodsList()
        {

        }

        public GoodsList(List<Goods> gl, string operation)
        {
            if (operation == "REPLACE")
            {
                this.goodList = gl;
            }
            else if (operation == "ADD")
            {
                foreach (Goods p in gl)
                {
                    this.goodList.Add(p);
                }
            }
            else Console.WriteLine("ERROR");
        }
    }
}
