using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloppyBird
{
    public partial class Form1 : Form
    {
        int idx = 0, dx = 1;

        Bitmap _bg = Properties.Resources.background_day;

        List<Bitmap> birds = new List<Bitmap>
        {
            Properties.Resources.bluebird_upflap,
            Properties.Resources.bluebird_downflap,
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.background_day;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap tmp = new Bitmap(_bg.Width, _bg.Height);
            // Graphics gg = Graphics.FromImage(tmp);

            //gg.DrawImage(_bg, _bg.Width - 1, 0, 1, _bg.Height);
            //gg.Dispose();
            for (int x = _bg.Width - 1; x < _bg.Width; x++)
                for (int y = 0; y < _bg.Height; y++)
                    tmp.SetPixel(x, y, _bg.GetPixel(1 - (_bg.Width - x), y));

            for (int x = 1; x < _bg.Width; x++)
                for (int y = 0; y < _bg.Height; y++)
                    tmp.SetPixel(x - 1, y, _bg.GetPixel(x, y));

            #region draw on background
            Bitmap drawMap = new Bitmap(_bg.Width, _bg.Height);
            Graphics g = Graphics.FromImage(drawMap);
            g.DrawImage(tmp, 0, 0);
            g.DrawImage(birds[idx++ % birds.Count], _bg.Width / 2, _bg.Height / 2, 20, 20);
            #endregion

            _bg = tmp; pictureBox1.Image = drawMap;
        }
    }
}
