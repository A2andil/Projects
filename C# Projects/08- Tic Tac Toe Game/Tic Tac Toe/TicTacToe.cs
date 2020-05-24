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
        Piece[,] board = new Piece[3, 3];

        int xScore = 0, oScore = 0, role = 0;

        Label lblScore = new Label();

        public TicTacToe()
        {
            InitializeComponent();
            intial();
        }

        private void intial()
        {
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = new Piece(j * 100, i * 100);
                    board[i, j].Click += play;
                    board[i, j].Cursor = Cursors.Hand;
                    Controls.Add(board[i, j]);
                }
            drawScore();
        }

        private void drawScore()
        {
            lblScore.Location = new Point(0, 320);
            lblScore.Width = 300;
            lblScore.TextAlign = ContentAlignment.MiddleCenter;
            lblScore.Text = "PlX : 0 - PlO : 0";
            Controls.Add(lblScore);
        }

        private void play(object sender, EventArgs e)
        {
            int i = ((Button)sender).Top / 100, j = ((Button)sender).Left / 100;

            if (board[i, j].state == States.F)
            {
                //if role true, it's X Player Role, else it's O role
                if (role % 2 == 0)
                {
                    board[i, j].Image = Properties.Resources.X;
                    board[i, j].state = States.X;
                }
                else
                {
                    board[i, j].Image = Properties.Resources.O;
                    board[i, j].state = States.O;
                }
                role += 1;
                checkWinner();
                if (role == 9) reset();
            }
            else
            {
                MessageBox.Show("This Place Not Correct");
            }
        }

        private void checkWinner()
        {
            //check rows
            for (int i = 0; i < 3; i++)
                if (board[i, 1].state == board[i, 0].state
                    && board[i, 1].state == board[i, 2].state
                    && board[i, 1].state != States.F)
                {
                    win(i, 1);
                    return;
                }

            //check cols
            for (int j = 0; j < 3; j++)
                if (board[1, j].state == board[0, j].state
                    && board[1, j].state == board[2, j].state
                    && board[1, j].state != States.F)
                {
                    win(1, j);
                    return;
                }

            //check diagonals
            if (board[0, 0].state == board[1, 1].state
                && board[1, 1].state == board[2, 2].state
                && board[1, 1].state != States.F)
                win(1, 1);
            else if (board[0, 2].state == board[1, 1].state
                && board[1, 1].state == board[2, 0].state
                && board[1, 1].state != States.F)
                win(1, 1);
        }

        private void win(int i, int j)
        {
            if (board[i, j].state == States.X) xScore += 1;
            else oScore += 1;
            lblScore.Text = "PlX : " + xScore.ToString() + " - PlO : " + oScore;
            reset();
        }

        void reset()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    board[i, j].Image = null;
                    board[i, j].state = States.F;
                }
            role = 0;
        }

    }
}
