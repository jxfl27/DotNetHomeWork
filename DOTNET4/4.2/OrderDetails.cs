using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class OrderDetails
    {
        public List<Entry> Entries { get; set; } = new List<Entry>();

        public OrderDetails(List<Entry> _odl)
        {
            this.Entries = _odl;
        }

        public OrderDetails()
        {

        }

        public override string ToString()
        {
            string msg = "";
            foreach (Entry odl in Entries)
            {
                msg += "商品名: " + odl.Name + "  单价: " + odl.Price + " 数量: " + odl.Num + "  总价: " + odl.TotalPrice + "\n";
            }
            return msg;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderDetails detail &&
                   EqualityComparer<List<Entry>>.Default.Equals(Entries, detail.Entries) &&
                   EqualityComparer<List<Entry>>.Default.Equals(Entries, detail.Entries);
        }

        public override int GetHashCode()
        {
            int hashCode = -2014347154;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Entry>>.Default.GetHashCode(Entries);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Entry>>.Default.GetHashCode(Entries);
            return hashCode;
        }
    }
}
