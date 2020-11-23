using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Store
{
    public class OrderService
    {
        private int id = 0;

        public List<Order> Orders { get; set; } = new List<Order>();
        private readonly GoodsList pl = new GoodsList();

        public OrderService(GoodsList pl)
        {
            this.pl = pl;
        }

        public void AddOrder(string client, List<ClientDemand> demand)
        {
            List<Entry> odTemp = new List<Entry>();
            foreach (ClientDemand c in demand)
            {
                var price = from product in pl.goodList where product.GoodsName == c.Name select product.Price;
                System.Diagnostics.Debug.WriteLine(price);
                System.Diagnostics.Debug.WriteLine(c.Name);
                System.Diagnostics.Debug.WriteLine(c.Num);
                Entry ol = new Entry(c.Name, price.First(), c.Num);
                odTemp.Add(ol);
            }
            OrderDetails od = new OrderDetails(odTemp);
            Order o = new Order(id, client, od);
            Orders.Add(o);
            id++;
        }

        public void DeleteOrder(string client)
        {
            foreach (Order o in Orders)
            {
                if (o.Client == client)
                    try
                    {
                        Orders.Remove(o);
                        return;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
            }
            Console.WriteLine("ERROR");
        }

        public List<Order> GetOrder(int id = -1, string goodsName = "", string client = "", double money = -1)
        {
            List<List<Order>> tempList = new List<List<Order>>();
            List<Order> res = new List<Order>();
            if (client != "")
            {
                List<Order> list = this.Query(o => o.Client == client);
                tempList.Add(list);
            }
            if (id != -1)
            {
                List<Order> list = this.Query(o => o.Id == id);
                tempList.Add(list);
            }
            if (goodsName != "")
            {
                List<Order> list = this.Query(o => o.Client == client);
                tempList.Add(list);
            }
            if (money != -1)
            {
                List<Order> list = this.Query(o => o.Cost >= money);
                tempList.Add(list);
            }
            if (tempList.Count == 0) return null;
            foreach(Order o in tempList[0])
            {
                for(int i = 1; i < tempList.Count; i++)
                {
                    if (!tempList[i].Contains(o)) break;
                    if (i == tempList.Count - 1) res.Add(o);
                }
            }
            return res;
        }

        public List<Order> Query(Func<Order, bool> condition)
        {
            var query = Orders.Where(o => condition(o));
            return query.ToList();
        }

        public void UpdateOrder(string client, List<ClientDemand> l)
        {
            foreach (Order o in Orders)
            {
                if (o.Client == client)
                {
                    foreach (ClientDemand cd in l)
                    {
                        var product = from odl in o.Od.Entries where odl.Name == cd.Name select odl;
                        if (product.Any())
                        {
                            product.First().Num = cd.Num;
                        }
                        else
                        {
                            var price = from productWant in pl.goodList where productWant.GoodsName == cd.Name select productWant.Price;
                            Entry odl = new Entry(cd.Name, price.First(), cd.Num);
                            o.Od.Entries.Add(odl);
                        }
                    }
                }
                Console.WriteLine("ERROR");
            }
        }

        public void Sort(Comparison<Order> c)
        {
            Orders.Sort(c);
        }

        public string ToStringLong(string client)
        {
            string msg1 = "", msg2 = "";
            foreach (Order o in Orders)
            {
                if (client == o.Client)
                {
                    msg1 = o.ToString();
                    msg2 = o.Od.ToString();
                }
            }
            if (msg1 == "" && msg2 == "") Console.WriteLine("ERROR");
            return msg1 + "\n" + msg2;
        }

        public string ToStringShort(string client)
        {
            string msg1 = "";
            foreach (Order o in Orders)
            {
                if (client == o.Client)
                {
                    msg1 = o.ToString();
                }
            }
            if (msg1 == "") Console.WriteLine("ERROR");
            return msg1;
        }

        public void Export(string filename)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Order>));
            if (Path.GetExtension(filename) != "xml"){
                filename = filename + ".xml";
            }
            using (FileStream fs = new FileStream(filename, FileMode.Create))
                xml.Serialize(fs, Orders);
        }

        public void Import(string filename)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Order>));
            if (Path.GetExtension(filename) != "xml")
            {
                throw new ArgumentException("This is not a xml file");
            }
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    List<Order> olTemp = (List<Order>)xml.Deserialize(fs);
                    Orders = olTemp;
                }
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
    }
}
