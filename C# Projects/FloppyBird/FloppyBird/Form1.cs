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
        int idx = 0, n = 0;

        Bitmap _bg = Properties.Resources.background_day;

        List<Bitmap> birds = new List<Bitmap>
        {
            Properties.Resources.bluebird_upflap,
            Properties.Resources.bluebird_downflap,
        };

        List<Bitmap> bgs = new List<Bitmap>
        {
           Properties.Resources.day0, Properties.Resources.day1,
           Properties.Resources.day2, Properties.Resources.day3,
           Properties.Resources.day4, Properties.Resources.day5,
           Properties.Resources.day6, Properties.Resources.day7,
           Properties.Resources.day8, Properties.Resources.day9,
           Properties.Resources.day10, Properties.Resources.day11,
           Properties.Resources.day12, Properties.Resources.day13,
           Properties.Resources.day14, Properties.Resources.day15,
           Properties.Resources.day16, Properties.Resources.day17,
           Properties.Resources.day18, Properties.Resources.day19,
           Properties.Resources.day20, Properties.Resources.day21,
           Properties.Resources.day22, Properties.Resources.day23,
           Properties.Resources.day24, Properties.Resources.day25,
           Properties.Resources.day26, Properties.Resources.day27,
           Properties.Resources.day28, Properties.Resources.day29,
           Properties.Resources.day30, Properties.Resources.day31,
           Properties.Resources.day32, Properties.Resources.day33,
           Properties.Resources.day34, Properties.Resources.day35,
           Properties.Resources.day36, Properties.Resources.day37,
           Properties.Resources.day38, Properties.Resources.day39,
           Properties.Resources.day40, Properties.Resources.day41,
           Properties.Resources.day42, Properties.Resources.day43,
           Properties.Resources.day44, Properties.Resources.day45,
           Properties.Resources.day46, Properties.Resources.day47,
           Properties.Resources.day48, Properties.Resources.day49,
           Properties.Resources.day50, Properties.Resources.day51,
           Properties.Resources.day52, Properties.Resources.day53,
           Properties.Resources.day54, Properties.Resources.day55,
           Properties.Resources.day56, Properties.Resources.day57,
           Properties.Resources.day58, Properties.Resources.day59,
           Properties.Resources.day60, Properties.Resources.day61,
           Properties.Resources.day62, Properties.Resources.day63,
           Properties.Resources.day64, Properties.Resources.day65,
           Properties.Resources.day66, Properties.Resources.day67,
           Properties.Resources.day68, Properties.Resources.day69,
           Properties.Resources.day70, Properties.Resources.day71,
           Properties.Resources.day72, Properties.Resources.day73,
           Properties.Resources.day74, Properties.Resources.day75,
           Properties.Resources.day76, Properties.Resources.day77,
           Properties.Resources.day78, Properties.Resources.day79,
           Properties.Resources.day80, Properties.Resources.day81,
           Properties.Resources.day82, Properties.Resources.day83,
           Properties.Resources.day84, Properties.Resources.day85,
           Properties.Resources.day86, Properties.Resources.day87,
           Properties.Resources.day88, Properties.Resources.day89,
           Properties.Resources.day90, Properties.Resources.day91,
           Properties.Resources.day92, Properties.Resources.day93,
           Properties.Resources.day94, Properties.Resources.day95,
           Properties.Resources.day96, Properties.Resources.day97,
           Properties.Resources.day98, Properties.Resources.day99,
           Properties.Resources.day100, Properties.Resources.day101,
           Properties.Resources.day102, Properties.Resources.day103,
           Properties.Resources.day104, Properties.Resources.day105,
           Properties.Resources.day106, Properties.Resources.day107,
           Properties.Resources.day108, Properties.Resources.day109,
           Properties.Resources.day110, Properties.Resources.day111,
           Properties.Resources.day112, Properties.Resources.day113,
           Properties.Resources.day114, Properties.Resources.day115,
           Properties.Resources.day116, Properties.Resources.day117,
           Properties.Resources.day118, Properties.Resources.day119,
           Properties.Resources.day120, Properties.Resources.day121,
           Properties.Resources.day122, Properties.Resources.day123,
           Properties.Resources.day124, Properties.Resources.day125,
           Properties.Resources.day126, Properties.Resources.day127,
           Properties.Resources.day128, Properties.Resources.day129,
           Properties.Resources.day130, Properties.Resources.day131,
           Properties.Resources.day132, Properties.Resources.day133,
           Properties.Resources.day134, Properties.Resources.day135,
           Properties.Resources.day136, Properties.Resources.day137,
           Properties.Resources.day138, Properties.Resources.day139,
           Properties.Resources.day140, Properties.Resources.day141,
           Properties.Resources.day142, Properties.Resources.day143,
           Properties.Resources.day144, Properties.Resources.day145,
           Properties.Resources.day146, Properties.Resources.day147,
           Properties.Resources.day148, Properties.Resources.day149,
           Properties.Resources.day150, Properties.Resources.day151,
           Properties.Resources.day152, Properties.Resources.day153,
           Properties.Resources.day154, Properties.Resources.day155,
           Properties.Resources.day156, Properties.Resources.day157,
           Properties.Resources.day158, Properties.Resources.day159,
           Properties.Resources.day160, Properties.Resources.day161,
           Properties.Resources.day161, Properties.Resources.day162,
           Properties.Resources.day162, Properties.Resources.day163,
           Properties.Resources.day164, Properties.Resources.day165,
           Properties.Resources.day166, Properties.Resources.day167,
           Properties.Resources.day168, Properties.Resources.day169,
           Properties.Resources.day170, Properties.Resources.day171,
           Properties.Resources.day172, Properties.Resources.day173,
           Properties.Resources.day174, Properties.Resources.day175,
           Properties.Resources.day176, Properties.Resources.day177,
           Properties.Resources.day178, Properties.Resources.day179,
           Properties.Resources.day180, Properties.Resources.day181,
           Properties.Resources.day182, Properties.Resources.day183,
           Properties.Resources.day184, Properties.Resources.day185,
           Properties.Resources.day186, Properties.Resources.day187,
           Properties.Resources.day188, Properties.Resources.day189,
           Properties.Resources.day190, Properties.Resources.day191,
           Properties.Resources.day192, Properties.Resources.day193,
           Properties.Resources.day194, Properties.Resources.day195,
           Properties.Resources.day196, Properties.Resources.day197,
           Properties.Resources.day198, Properties.Resources.day199,
           Properties.Resources.day200, Properties.Resources.day201,
           Properties.Resources.day202, Properties.Resources.day203,
           Properties.Resources.day204, Properties.Resources.day205,
           Properties.Resources.day206, Properties.Resources.day207,
           Properties.Resources.day208, Properties.Resources.day209,
           Properties.Resources.day210, Properties.Resources.day211,
           Properties.Resources.day212, Properties.Resources.day213,
           Properties.Resources.day214, Properties.Resources.day215,
           Properties.Resources.day216, Properties.Resources.day217,
           Properties.Resources.day218, Properties.Resources.day219,
           Properties.Resources.day220, Properties.Resources.day221,
           Properties.Resources.day222, Properties.Resources.day223,
           Properties.Resources.day224, Properties.Resources.day225,
           Properties.Resources.day226, Properties.Resources.day227,
           Properties.Resources.day228, Properties.Resources.day229,
           Properties.Resources.day230, Properties.Resources.day231,
           Properties.Resources.day232, Properties.Resources.day233,
           Properties.Resources.day234, Properties.Resources.day235,
           Properties.Resources.day236, Properties.Resources.day237,
           Properties.Resources.day238, Properties.Resources.day239,
           Properties.Resources.day240, Properties.Resources.day241,
           Properties.Resources.day242, Properties.Resources.day243,
           Properties.Resources.day244, Properties.Resources.day245,
           Properties.Resources.day246, Properties.Resources.day247,
           Properties.Resources.day248, Properties.Resources.day249,
           Properties.Resources.day250, Properties.Resources.day251,
           Properties.Resources.day252, Properties.Resources.day253,
           Properties.Resources.day254, Properties.Resources.day255,
           Properties.Resources.day256, Properties.Resources.day257,
           Properties.Resources.day258, Properties.Resources.day259,
           Properties.Resources.day260, Properties.Resources.day261,
           Properties.Resources.day261, Properties.Resources.day262,
           Properties.Resources.day262, Properties.Resources.day263,
           Properties.Resources.day264, Properties.Resources.day265,
           Properties.Resources.day266, Properties.Resources.day267,
           Properties.Resources.day268, Properties.Resources.day269,
           Properties.Resources.day270, Properties.Resources.day271,
           Properties.Resources.day272, Properties.Resources.day273,
           Properties.Resources.day274, Properties.Resources.day275,
           Properties.Resources.day276, Properties.Resources.day277,
           Properties.Resources.day278, Properties.Resources.day279,
           Properties.Resources.day280, Properties.Resources.day281,
           Properties.Resources.day282, Properties.Resources.day283,
           Properties.Resources.day284, Properties.Resources.day285,
           Properties.Resources.day286, Properties.Resources.day287
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

            Graphics gg = Graphics.FromImage(tmp);

            gg.DrawImage(bgs[n++ % bgs.Count], 0, 0, _bg.Width, _bg.Height);
            gg.Dispose();
            //for (int x = _bg.Width - 1; x < _bg.Width; x++)
            //    for (int y = 0; y < _bg.Height; y++)
            //        tmp.SetPixel(x, y, _bg.GetPixel(1 - (_bg.Width - x), y));

            //for (int x = 1; x < _bg.Width; x++)
            //    for (int y = 0; y < _bg.Height; y++)
            //        tmp.SetPixel(x - 1, y, _bg.GetPixel(x, y));

            #region draw on background
            Bitmap drawMap = new Bitmap(_bg.Width, _bg.Height);
            Graphics g = Graphics.FromImage(tmp);
            g.DrawImage(birds[idx++ % birds.Count], _bg.Width / 2, _bg.Height / 2, 20, 20);
            #endregion

            _bg = tmp; pictureBox1.Image = tmp;
        }
    }
}
