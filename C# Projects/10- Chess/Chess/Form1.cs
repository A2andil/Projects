using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class Chess : Form
    {
        private Button[,] board = new Button[8, 8];
        private int[,] boardValues = new int[8, 8];

        private List<Bitmap> res = new List<Bitmap>
        {
            Properties.Resources._2Solidier,
            Properties.Resources._2Knight,
            Properties.Resources._2Horse,
            Properties.Resources._2Rook,
            Properties.Resources._2Queen,
            Properties.Resources._2King,
            Properties.Resources.Solider,
            Properties.Resources.Knight,
            Properties.Resources.Horse,
            Properties.Resources.Rook,
            Properties.Resources.Queen,
            Properties.Resources.King
        };

        private List<Point> Moves = new List<Point>();

        public Chess()
        {
            InitializeComponent();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    boardValues[i, j] = -1;
                    board[i, j] = new Button();
                    board[i, j].Size = new Size(70, 70);
                    board[i, j].Location = new Point(j * 70, i * 70);
                    board[i, j].Click += buttonClicked;
                    board[i, j].Cursor = Cursors.Hand;
                    Controls.Add(board[i, j]);
                    if ((i + j) % 2 == 0) board[i, j].BackColor = Color.BurlyWood;
                    else board[i, j].BackColor = Color.White;
                }
            }
            resize();
            place();
        }

        private void buttonClicked(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            int col = btn.Left / 70, row = btn.Top / 70;
            Moves.Clear();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (boardValues[i, j] == -1)
                        board[i, j].Text = "";
            switch (boardValues[row, col])
            {
                case 0:
                    int fRow = boardValues[row + 1, col];
                    if (fRow == -1)
                        Moves.Add(new Point(col, row + 1));
                    if (row == 1 && boardValues[row + 2, col] == -1 && fRow == -1)
                        Moves.Add(new Point(col, row + 2));
                    break;
            }
            for (int i = 0; i < Moves.Count; i++)
                board[Moves[i].Y, Moves[i].X].Text = "x";
        }

        private void place()
        {
            //Soliders
            for (int j = 0; j < board.GetLength(1); j++)
            {
                boardValues[1, j] = 0;
                board[1, j].Image = res[0];

                boardValues[6, j] = 6;
                board[6, j].Image = res[6];
            }
            //Rook
            board[0, 0].Image = board[0, 7].Image = res[3];
            board[7, 0].Image = board[7, 7].Image = res[9];
            //Knight
            board[0, 1].Image = board[0, 6].Image = res[1];
            board[7, 1].Image = board[7, 6].Image = res[7];
            //Horse
            board[0, 2].Image = board[0, 5].Image = res[2];
            board[7, 2].Image = board[7, 5].Image = res[8];
            //Queen
            board[0, 3].Image = res[4];
            board[7, 3].Image = res[10];
            //King
            board[0, 4].Image = res[5];
            board[7, 4].Image = res[11];
        }

        private void resize()
        {
            Graphics graphic;
            for (int i = 0; i < res.Count; i++)
            {
                Bitmap bmp = new Bitmap(70, 70);
                graphic = Graphics.FromImage(bmp);
                graphic.DrawImage(res[i], 0, 0, 70, 70);
                graphic.Dispose();
                res[i] = bmp;
            }
        }
    }
}
