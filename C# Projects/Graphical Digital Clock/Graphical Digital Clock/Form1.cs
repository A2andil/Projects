using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphical_Digital_Clock
{
    public partial class Form1 : Form
    {
        string path = @"C:\Users\Eng Ahmed Kandil\Desktop\Digits\";
        Timer t = new Timer();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t.Interval = 1000;
            t.Tick += new EventHandler(t_tick);
            t.Start();
        }
        void t_tick(object sender, EventArgs args)
        {
            int[] time = {DateTime.Now.Hour/10,DateTime.Now.Hour%10,
                          DateTime.Now.Minute/10,DateTime.Now.Minute%10,
                           DateTime.Now.Second/10,DateTime.Now.Second%10};

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics Graphic = Graphics.FromImage(bmp);

            for (int i = 0; i < time.Length; i++)
            {
                int extra = i/2*20;
                Image img = Image.FromFile(path + time[i] + ".png");
                Graphic.DrawImage(img, 10+i*32+extra, 10, 32, 46);
                pictureBox1.Image = bmp;
            }
        }
    }
}
