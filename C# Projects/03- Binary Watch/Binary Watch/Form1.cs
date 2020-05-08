using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Binary_Watch
{
    public partial class Form1 : Form
    {
        Timer t = new Timer(); 

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t.Interval = 1000;

            t.Tick += new EventHandler(this.t_tick);

            t.Start(); 
        }

        private int power(int m, int n)
        {
            if (n == 0)
                return 1;
            return m * power(m, n - 1);
        }

        private void showTime(int[] v, Label[] l, int n)
        {
            
            //start from last index because when store number in array we reverse it
            for (int i = v.Length - 1; i >= 0; i--)
            {
                v[i] = power(2, i); //we can use Math.pow()

                if (v[i] <= n)
                {
                    n -= v[i];
                    l[i].ForeColor = Color.Coral; //this digit has value 1 should color it coral 
                }
                else
                    l[i].ForeColor = Color.DimGray; //this digit has value 0
            }
        }

        private void t_tick(object sender, EventArgs args)
        {
            #region Show Watch in lablel
            int hh = DateTime.Now.Hour, 
                mm = DateTime.Now.Minute, 
                ss = DateTime.Now.Second; 

            string period = "AM";
            if (hh == 0)
                hh += 12;
            else if (hh > 12)
            {
                hh -= 12;
                period = "PM";
            }

            lblH.Text ="H : "+(hh<10?"0":"")+ hh;

            lblM.Text = "M : " + (mm < 10 ? "0" : "") + mm;

            lblS.Text = "S : " + (ss < 10 ? "0" : "") + ss;

            this.Text = "" + hh + ":" + mm + ":" + ss;

            lblPeriod.Text = period;

            #endregion

            #region Hours
            int[] hours = new int[4];
            Label[] lblHour = new Label[] { h1, h2, h4, h8 };
            showTime(hours, lblHour, hh);
            #endregion

            #region Minutes
            //like hours
            int[] Minutes = new int[6];
            Label[] lblMinute = new Label[] { m1, m2, m4, m8, m16, m32 };
            showTime(Minutes, lblMinute, mm);
            #endregion


            #region Minutes
            //like hours
            int[] Seconds = new int[6];
            Label[] lblSecond = new Label[] { s1, s2, s4, s8, s16, s32 };
            showTime(Seconds, lblSecond, ss);
            #endregion

        }
    }
}
