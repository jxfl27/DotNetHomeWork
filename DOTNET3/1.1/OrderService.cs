using System;
using System.Collections.Generic;
using System.Text;

namespace _1._1
{
    public class OrderService
    {
        public List<Order> OrderList;
        static private OrderService Servicer;
        public OrderService()
        {
            OrderList = new List<Order>();
        }
        static public ref OrderService Instance()
        {
            if (Servicer == null)
            {
                Servicer = new OrderService();
            }
            return ref Servicer;
        }
        public void Add(Order order)
        {
            OrderList.Add(order);
        }
        public void Delete(Order order)
        {
            Search(order);
            OrderList.Remove(order);
        }
        public void Modify(Order order, Order newOrder)
        {
            int index = OrderList.IndexOf(order);
            if (index == -1)
            {
                throw new Exception("NULL");
            }
            else
            {
                OrderList[index] = newOrder;
            }
        }
        public Order Search(Order order)
        {
            int index = OrderList.IndexOf(order);
            if (index == -1)
            {
                throw new Exception("NULL");
            }
            else
            {
                return OrderList[index] as Order;
            }
        }
        public void Sort(Func<Order, Order, int> func = null)
        {
            if (func == null)
            {
                OrderList.Sort();
            }
            else
            {
                OrderList.Sort((x, y) => func(x, y));
            }
        }
    }
 }
