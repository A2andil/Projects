using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digial_Watch
{
    public partial class Form1 : Form
    {
        Timer T = new Timer();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            T.Interval = 1000;
            T.Tick += new EventHandler(t_tick);
            T.Start();
        }
        private void t_tick(object Sender,EventArgs args)
        {
            int h, m, s;
            h = DateTime.Now.Hour;
            m = DateTime.Now.Minute;
            s = DateTime.Now.Second;

            label1.Text = h.ToString() + ":" + m.ToString() + ":" + s.ToString();
        }
    }
}
