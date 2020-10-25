using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace _1._1
{
    public class Order
    {
        public int ID { get; set; }
        public string name { get; set; }
        public List<OrderDetails> ods { get; set; }
        public Order()
        {
            ods = new List<OrderDetails>();
        }
        public override bool Equals(object obj)
        {

            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }
            else
            {
                var order = obj as Order;
                if (order.ID != this.ID || order.name != this.name)
                {
                    return false;
                }
                return order.ods == this.ods;
            }
           
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(ID.GetHashCode(), name.GetHashCode(), ods.GetHashCode());
        }

        public int CompareTo(object x)
        {
            if (ID == ((Order)x).ID)
            {
                return 0;
            }
            return ID < ((Order)x).ID ? -1 : 1;
        }

        public override string ToString()
        {
            return base.ToString()+"ID:"+ID+"CustomerName:"+name+ods.ToString();
        }
    }
}
