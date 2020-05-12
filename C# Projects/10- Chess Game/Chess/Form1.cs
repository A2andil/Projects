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

        private int[] dx = new int[4] { 1, -1, 0, 0 };
        private int[] dy = new int[4] { 0, 0, -1, 1 };

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
                    board[i, j].FlatStyle = FlatStyle.Flat;
                    board[i, j].FlatAppearance.BorderSize = 1;
                    if ((i + j) % 2 == 0) board[i, j].BackColor = Color.BurlyWood;
                    else board[i, j].BackColor = Color.White;
                    board[i, j].FlatAppearance.BorderColor = board[i, j].BackColor;
                    Controls.Add(board[i, j]);
                }
            }
            resize();
            place();
        }

        private bool safe(int row, int col)
            => row < 8 && col < 8 && row >= 0 && col >= 0;

        private void buttonClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int col = btn.Left / 70, row = btn.Top / 70;
            Moves.Clear();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    board[i, j].BackColor
                        = (i + j) % 2 == 0 ? Color.BurlyWood : Color.White;
                    board[i, j].FlatAppearance.BorderColor = board[i, j].BackColor;
                }
            int fRow;
            switch (boardValues[row, col])
            {
                case 0:
                    fRow = boardValues[row + 1, col];
                    if (fRow == -1)
                        Moves.Add(new Point(col, row + 1));
                    if (row == 1 && boardValues[row + 2, col] == -1 && fRow == -1)
                        Moves.Add(new Point(col, row + 2));
                    if (safe(row + 1, col + 1) && boardValues[row + 1, col + 1] > 5)
                        Moves.Add(new Point(col + 1, row + 1));
                    if (safe(row + 1, col - 1) && boardValues[row + 1, col - 1] > 5)
                        Moves.Add(new Point(col - 1, row + 1));
                    break;
                case 6:
                    fRow = boardValues[row - 1, col];
                    if (fRow == -1)
                        Moves.Add(new Point(col, row - 1));
                    if (row == 6 && boardValues[row - 2, col] == -1 && fRow == -1)
                        Moves.Add(new Point(col, row - 2));
                    if (safe(row - 1, col - 1) && boardValues[row - 1, col - 1] < 6
                        && boardValues[row - 1, col - 1] != -1)
                        Moves.Add(new Point(col - 1, row - 1));
                    if (safe(row - 1, col + 1) && boardValues[row - 1, col + 1] < 6
                        && boardValues[row - 1, col + 1] != -1)
                        Moves.Add(new Point(col + 1, row - 1));
                    break;
                case 3:
                case 9:
                    for (int i = 0; i < dx.Length; i++)
                    {
                        int cl = col, rw = row;
                        while (safe(cl + dx[i], rw + dy[i]))
                        {
                            cl += dx[i]; rw += dy[i];
                            Moves.Add(new Point(cl, rw));
                        }
                    }
                    break;
                case 1:
                case 7:
                    int[] dk = new int[2] { 1, -1 };
                    for (int i = 0; i < dk.Length; i++)
                        for (int j = 0; j < dk.Length; j++)
                        {
                            int cl = col, rw = row;
                            while (safe(cl + dk[i], rw + dk[j]))
                            {
                                cl += dk[i]; rw += dk[j];
                                Moves.Add(new Point(cl, rw));
                            }
                        }
                    break;
                case 2:
                case 8:
                    int[] dh = new int[4] { -1, 1, -2, 2 };
                    for (int i = 0; i < dh.Length; i++)
                    {
                        int comp = 3 - Math.Abs(dh[i]);
                        if (safe(row + comp, col + dh[i]))
                            Moves.Add(new Point(col + dh[i], row + comp));
                        if (safe(row - comp, col + dh[i]))
                            Moves.Add(new Point(col + dh[i], row - comp));
                    }
                    break;
                case 4:
                case 10:
                    for (int i = 0; i < dx.Length; i++)
                    {
                        int cl = col, rw = row;
                        while (safe(cl + dx[i], rw + dy[i]))
                        {
                            cl += dx[i]; rw += dy[i];
                            Moves.Add(new Point(cl, rw));
                        }
                    }
                    int[] dd = new int[2] { 1, -1 };
                    for (int i = 0; i < dd.Length; i++)
                        for (int j = 0; j < dd.Length; j++)
                        {
                            int cl = col, rw = row;
                            while (safe(cl + dd[i], rw + dd[j]))
                            {
                                cl += dd[i]; rw += dd[j];
                                Moves.Add(new Point(cl, rw));
                            }
                        }
                    break;
            }
            for (int i = 0; i < Moves.Count; i++)
            {
                board[Moves[i].Y, Moves[i].X].BackColor = Color.Coral;
                board[Moves[i].Y, Moves[i].X].FlatAppearance.BorderColor 
                    = Color.Coral;
            }
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
            boardValues[0, 0] = boardValues[0, 7] = 3;

            board[7, 0].Image = board[7, 7].Image = res[9];
            boardValues[7, 0] = boardValues[7, 7] = 9;

            //Knight
            board[0, 1].Image = board[0, 6].Image = res[1];
            boardValues[0, 1] = boardValues[0, 6] = 1;

            board[7, 1].Image = board[7, 6].Image = res[7];
            boardValues[7, 1] = boardValues[7, 6] = 7;

            //Horse
            board[0, 2].Image = board[0, 5].Image = res[2];
            boardValues[0, 2] = boardValues[0, 5] = 2;

            board[7, 2].Image = board[7, 5].Image = res[8];
            boardValues[7, 2] = boardValues[7, 5] = 8;

            //Queen
            board[0, 3].Image = res[4];
            boardValues[0, 3] = 4;

            board[7, 3].Image = res[10];
            boardValues[7, 3] = 10;

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

        private void Chess_Load(object sender, EventArgs e)
        {

        }
    }
}
