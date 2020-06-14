using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _12__Flappy_Bird
{
    public partial class FlappyBird : Form
    {

        int idx = 0, n = 0, count = 0, p_idx = 700, top = 50, grav = 1;

        private void FlappyBird_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) grav -= 10;
        }

        private void FlappyBird_Load(object sender, EventArgs e)
        {
            tm.Start();
        }

        Bitmap _bg = Properties.Resources.day0;

        List<Bitmap> birds = new List<Bitmap>
        {
            Properties.Resources.bluebird_upflap,
            Properties.Resources.bluebird_downflap,
        };

        public FlappyBird()
        {
            InitializeComponent();
        }

        private void tm_Tick(object sender, EventArgs e)
        {
            count = (count + 1) % 700;
            Bitmap bmp = new Bitmap(_bg.Width, _bg.Height);

            Graphics gg = Graphics.FromImage(bmp);
            Bitmap bg = count < 300 ? Helper.days[n] : Helper.nights[n];

            gg.DrawImage(bg, 0, 0, _bg.Width, _bg.Height);
            gg.Dispose();

            #region draw on background
            Bitmap drawMap = new Bitmap(_bg.Width, _bg.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(birds[idx++ % birds.Count], _bg.Width / 2, top, 20, 20);
            #endregion


            #region start raseaf
            g.DrawImage(Helper.bases[n], 0, 350, 700, 50);
            #endregion

            #region start consequences
            g.DrawImage(Properties.Resources.pipe_green, p_idx, 250, 30, 100);
            #endregion

            _bg = bmp; PB.Image = bmp;
            n = (n + 1) % Helper.days.Count;
            p_idx = p_idx < -30 ? 700 : p_idx - 1;
            grav = Math.Min(grav + 1, 2);
            top += grav;
        }
    }
}