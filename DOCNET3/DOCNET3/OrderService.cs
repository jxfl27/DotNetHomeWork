using System;
using System.Collections.Generic;
using System.Text;

namespace DOCNET3
{
    class OrderService
    {
        public List<Order> OrderList;
        static private OrderService instance;
        public OrderService()
        {
            OrderList = new List<Order>();
        }
        static public ref OrderService Instance()
        {
            if (instance == null)
            {
                instance = new OrderService();
            }
            return ref instance;
        }
        public void AddOrder(Order order)
        {
            OrderList.Add(order);
        }
        public void RemoveOrder(Order order)
        {
            QueryOrder(order);
            OrderList.Remove(order);
        }
        public void ModifyOrder(Order order, Order newOrder)
        {
            int index = OrderList.IndexOf(order);
            if (index == -1)
            {
                throw new Exception("There's no such order");
            }
            else
            {
                OrderList[index] = newOrder;
            }
        }
        public Order QueryOrder(Order order)
        {
            int index = OrderList.IndexOf(order);
            if (index == -1)
            {
                throw new Exception("There's no such order");
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
