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
            Properties.Resources.Horse,
            Properties.Resources.King,
            Properties.Resources.Knight,
            Properties.Resources.Queen,
            Properties.Resources.Rook,
            Properties.Resources.Solider
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
                    Controls.Add(board[i, j]);
                    if ((i + j) % 2 == 0) board[i, j].BackColor = Color.BurlyWood;
                    else board[i, j].BackColor = Color.White;
                }
            }
            Resize();
            Place();
        }

        private void buttonClicked(object sender, EventArgs e)
        {
            MessageBox.Show("ok");
        }

        private void Place()
        {
            Random rand = new Random();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    int r = rand.Next(res.Count);
                    board[i, j].Image = res[r];
                }
            }
        }

        private void Resize()
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
