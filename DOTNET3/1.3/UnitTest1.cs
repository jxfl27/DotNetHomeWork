using Microsoft.VisualStudio.TestTools.UnitTesting;
using _1._1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._2
{
    [TestClass]
    public class UnitTest1
    {
        private Order order1 = new Order()
        {
            ID = 1000001,
            name = "Jerome",
            ods = new List<OrderDetails>()
                {
                    new OrderDetails("phone",1000),
                    new OrderDetails("PC",8000)
                }
        };
        private Order order2 = new Order()
        {
            ID = 1000002,
            name = "Peter",
            ods = new List<OrderDetails>()
                {
                    new OrderDetails("phone",2000),
                    new OrderDetails("PC",10000)
                }
        };
        private Order order3 = new Order()
        {
            ID = 1000003,
            name = "Kold",
            ods = new List<OrderDetails>()
                {
                    new OrderDetails("PC",20000)
                }
        };
        private OrderService service;

        [TestInitialize]
        public void InitializeOrderList()
        {
            service = OrderService.Instance();
            service.OrderList.Clear();
            service.Add(order1);
            service.Add(order2);
        }

        [TestMethod]
        public void QueryOrderTest()
        {
            try
            {
                var order = service.Search(order1);
                Console.WriteLine(order.ToString());

                order = service.Search(order3);
                Console.WriteLine(order.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        [TestMethod]
        public void SortTest()
        {
            service.Add(order3);
            service.Sort();
            var correct = new List<Order>
            {
                order1,order2,order3
            };
            CollectionAssert.Equals(service.OrderList, correct);

            service.Sort((a, b) => string.Compare(a.name, b.name));
            correct = new List<Order>
            {
                order1,order3,order2
            };
            CollectionAssert.Equals(service.OrderList, correct);
        }


    }
}
