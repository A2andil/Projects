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

        public Chess()
        {
            InitializeComponent();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
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
            MessageBox.Show("ok");
        }

        private void place()
        {
            //Soliders
            for (int j = 0; j < board.GetLength(1); j++)
            {
                board[1, j].Image = res[0];
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
