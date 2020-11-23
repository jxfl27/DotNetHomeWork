using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class Order
    {
        public int Id { get; set; } = 0;
        public string Client { get; set; } = "";
        public double Cost { get; set; } = 0;

        private OrderDetails od = new OrderDetails();
        public OrderDetails Od
        {
            get { return od; }
            set
            {
                Cost = 0;
                foreach (Entry odl in od.Entries)
                {
                    Cost += odl.TotalPrice;
                }
            }
        }

        public Order() { }
        public Order(int id, string client, OrderDetails od)
        {
            this.Id = id;
            this.Client = client;
            this.od = od;
            Cost = 0;
            foreach (Entry odl in od.Entries)
            {
                Cost += odl.TotalPrice;
            }
        }

        public override string ToString()
        {
            string msg = "编号: " + Id + " 客户: " + Client + "  花费: " + Cost;
            return msg;
        }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   Id == order.Id &&
                   Id == order.Id;
        }

        public override int GetHashCode()
        {
            int hashCode = -1496434976;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            return hashCode;
        }
    }
}
