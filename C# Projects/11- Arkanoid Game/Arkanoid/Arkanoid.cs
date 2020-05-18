using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arkanoid
{
    public partial class Arkanoid : Form
    {
        bool[,] bricks;

        PictureBox _paddle, _ball;

        int _paddleDx = 10, dx = 2, dy = 2;

        public Arkanoid()
        {
            InitializeComponent();
            createPaddle();
            createBall();
            drawBricks();
        }

        void createPaddle()
        {
            _paddle = new PictureBox();
            _paddle.Size = new Size(140, 20);
            _paddle.SizeMode = PictureBoxSizeMode.StretchImage;
            _paddle.Image = Properties.Resources.paddle;
            _paddle.Location = new Point(Width / 2 - _paddle.Width / 2
                , Height - _paddle.Height);
            Controls.Add(_paddle);
        }

        void createBall()
        {
            _ball = new PictureBox();
            _ball.Size = new Size(16, 16);
            _ball.SizeMode = PictureBoxSizeMode.StretchImage;
            _ball.Image = Properties.Resources.ball;
            _ball.Location
                = new Point(_paddle.Left + _paddle.Width / 2 - _ball.Width / 2
                , _paddle.Top - _ball.Height);
            Controls.Add(_ball);
        }

        void drawBricks()
        {
            Random rand = new Random();

            List<Bitmap> bricksImages = new List<Bitmap>
            {
                Properties.Resources._0,
                Properties.Resources._1,
                Properties.Resources._2,
                Properties.Resources._3,
                Properties.Resources._4,
                Properties.Resources._5,
                Properties.Resources._6
            };

            resize(bricksImages, 70, 30);

            bricks = new bool[4, 10];
            for (int i = 0; i < bricks.GetLength(0); i++)
                for (int j = 0; j < bricks.GetLength(1); j++)
                {
                    bricks[i, j] = true;
                    Label brick = new Label();
                    brick.Location = new Point(50 + j * 70, i * 30 + 50);
                    brick.Size = new Size(70, 30);
                    brick.Image = Properties.Resources.paddle;
                    brick.Image = bricksImages[rand.Next() % bricksImages.Count];
                    Controls.Add(brick);
                }
        }

        private void Arkanoid_KeyDown(object sender, KeyEventArgs e)
        {
            int x = _paddle.Location.X;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    x -= _paddleDx;
                    break;
                case Keys.Right:
                    x += _paddleDx;
                    break;
                case Keys.Space:
                    tm.Start();
                    break;
            }
            if (x >= 0 && x <= Width - _paddle.Width)
            {
                _paddle.Left = x;
                if (!tm.Enabled)
                    _ball.Left = x + _paddle.Width / 2 - _ball.Width / 2;
            }
        }

        private void tm_Tick(object sender, EventArgs e)
        {
            dx = _ball.Left <= 0 || _ball.Left >= Width - _ball.Width? -dx : dx;
            dy = _ball.Top <= 0 || _ball.Top >= Height - _ball.Height ? -dy : dy;
            _ball.Left -= dx;
            _ball.Top -= dy;
        }

        void resize(List<Bitmap> images, int width, int height)
        {
            Graphics graph;
            for (int i = 0; i < images.Count; i++)
            {
                Bitmap bmp = new Bitmap(width, height);
                graph = Graphics.FromImage(bmp);
                graph.DrawImage(images[i], 0, 0, width, height);
                graph.Dispose();
                images[i] = bmp;
            }
        }
    }
}
