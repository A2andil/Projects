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
        string path = @"Digits\";
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

        private void t_tick(object sender, EventArgs e)
        {
            int h, m, s;
            h = DateTime.Now.Hour;
            m = DateTime.Now.Minute;
            s = DateTime.Now.Second;

            int[] time = { h / 10, h % 10, m / 10, m % 10, s / 10, s % 10 };

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            Graphics graphic = Graphics.FromImage(bmp);

            for(int i=0;i<time.Length;i++)
            {
                int extra = 32 * i + i / 2 * 20;

                Image img = Image.FromFile(path + time[i] + ".png");

                graphic.DrawImage(img, 10 + extra, 10, 32, 46);
            }

            Font font = new Font("Arial", 30);
            SolidBrush brush = new SolidBrush(Color.Black);
            PointF point = new PointF(68.5f, 10);

            graphic.DrawString(":", font, brush, point);
            point.X = 154.5f;
            graphic.DrawString(":", font, brush, point);

            pictureBox1.Image = bmp;

            graphic.Dispose();
        }

        
    }
}
