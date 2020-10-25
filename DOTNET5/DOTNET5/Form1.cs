using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOTNET5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Hashtable urls = new Hashtable();
        public int count = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            Crawler mycrawler = new Crawler();
            string startUrl = textBox1.Text;
            mycrawler.urls.Add(startUrl, false);
            new Thread(mycrawler.Crawl).Start();
            listBox2.Items.Add(urls[current]);
        }
    }
}
