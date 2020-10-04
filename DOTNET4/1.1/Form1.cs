using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOTNET4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Graphics graphics;
        public double th1 { get; set; }
        public double th2 { get; set; }
        public double per1 { get; set; }
        public double per2 { get; set; }
        public int n { get; set; }
        public double leng { get; set; }

        Pen mypen = new Pen(Color.Blue, (float)2);

        public Color GetColor;

        private void button1_Click(object sender, EventArgs e)
        {
            mypen.Color = GetColor;
            if (graphics == null)  graphics = this.panel1.CreateGraphics();
            drawCayleyTree(n, 200, 310, leng, -Math.PI / 2);
        }
        void drawCayleyTree(int n, double x0, double y0, double leng, double th)
        {
            if (n == 0) return;
            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);

            drawLine(x0, y0, x1, y1);

            drawCayleyTree(n - 1, x1, y1, per1 * leng, th + th1 * Math.PI / 180);
            drawCayleyTree(n - 1, x1, y1, per2 * leng, th - th2 * Math.PI / 180);
        }
        void drawLine(double x0,double y0, double x1,double y1)
        {
            graphics.DrawLine(mypen, (int)x0, (int)y0, (int)x1, (int)y1);
        }

        public void transform(string penColor)
        {
            string[] rgbArray = penColor.Split(',');
            int[] intArray = new int[3];
            intArray = Array.ConvertAll<string, int>(rgbArray, s => int.Parse(s));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            th1 = 30;th2 = 20;per1 = 0.6;per2 = 0.7;leng = 100;n = 10; 
            textBox1.DataBindings.Add("Text", this, "n");
            textBox2.DataBindings.Add("Text", this, "leng");
            textBox3.DataBindings.Add("Text", this, "th1");
            textBox4.DataBindings.Add("Text", this, "th2");
            textBox5.DataBindings.Add("Text", this, "per1");
            textBox6.DataBindings.Add("Text", this, "per2");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                GetColor = colorDialog1.Color;
                //GetColor就是用户选择的颜色，接下来就可以使用该颜色了
            }
        }
    }
}
