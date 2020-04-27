using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class TicTacToe : Form
    {
        List<Button> lst;
        int PlayerRole = 0;
        public TicTacToe()
        {
            InitializeComponent();
            lst = new List<Button>
            {
                btn1, btn2, btn3,
                btn4, btn5, btn6,
                btn7, btn8, btn9
            };
            Reset();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Reset();
        }

        void Reset()
        {
            for (int i = 0; i < lst.Count; i++)
            {
                lst[i].Text = "";
            }
            PlayerRole = 0;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text.Equals(""))
            {
                ((Button)sender).Text = PlayerRole % 2 == 0 ? "X" : "O";
                PlayerRole++;
                CheckWinner();
            }
            else
                MessageBox.Show("Please Select Right Button");
        }

        private void CheckWinner()
        {
            /*
             * 0 1 2
             * 3 4 5
             * 6 7 8
             */

            string Empty = "";
            for (int i = 0; i < 3; i++)
            {
                string center = lst[i * 3 + 1].Text;
                //Check Rows
                if (center == lst[i * 3].Text && center == lst[i * 3 + 2].Text)
                {
                    if (center == Empty)
                        continue;
                    MessageBox.Show(center + " Is The Winner");
                    return;
                }
                //Check Columns
                center = lst[3 + i].Text;
                if (lst[i].Text == center && lst[6 + i].Text == center)
                {
                    if (center == Empty)
                        continue;
                    MessageBox.Show(center + " Is The Winner");
                }    
            }
            //check back diagonal 0, 4, 8 => i * 3 + i & i = 0,1,2
            if (lst[4].Text == lst[0].Text && lst[4].Text == lst[8].Text)
            {
                if (lst[4].Text != Empty)
                    MessageBox.Show(lst[4].Text + " Is The Winner");
            }
            //check front diagonal 2, 4, 6 => i*3-i & i = 1, 2, 3
            if (lst[4].Text == lst[2].Text && lst[4].Text == lst[6].Text)
            {
                if (lst[4].Text != Empty)
                    MessageBox.Show(lst[4].Text + " Is The Winner");
            }
        }
    }
}
