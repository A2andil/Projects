using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Arkanoid
{
    public partial class Arkanoid : Form
    {
        PictureBox _paddle, _ball;

        int _paddleDx = 10, dx = 2, dy = 2, score = 0, live = 3;

        List<PictureBox> _hearts;

        public Arkanoid()
        {
            InitializeComponent();
            Intialize();
        }

        void Intialize()
        {
            createPaddle();
            createBall();
            drawBricks();
            createHearts();
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

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 10; j++)
                {
                    Label brick = new Label();
                    brick.Tag = "brick";
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
            dy = _ball.Top <= 0 ? -dy : dy;

            if (_ball.Top >= Height)
            {
                tm.Stop();
                _ball.Left = _paddle.Left + _paddle.Width / 2 - _ball.Width / 2;
                _ball.Top = _paddle.Top - _ball.Height;
                _hearts[3 - live].Image = Properties.Resources.d_heart;
                live -= 1;
                if (live <= 0)
                {
                    MessageBox.Show("Game over");
                    Controls.Clear();
                }
            }

            _ball.Left -= dx;
            _ball.Top -= dy;
            foreach (Control c in Controls)
                if (c.Tag != null && _ball.Bounds.IntersectsWith(c.Bounds))
                {
                    dy = -dy; caldx(c);
                    Controls.Remove(c);
                    score += 10;
                    lblScore.Text = "Score: " + score.ToString();
                    new Thread(() => {
                        new SoundPlayer(Properties.Resources.Arkanoid_SFX__1_)
                        .Play();
                    }).Start();
                }

            if (_ball.Bounds.IntersectsWith(_paddle.Bounds))
            {
                dy = Math.Abs(dy); caldx(_paddle);
                new Thread(() => {
                    new SoundPlayer(Properties.Resources.Arkanoid_SFX__5_)
                    .Play();
                }).Start();
            }
            if (score == 10 * 10 * 4) win();
        }

        void win()
        {
            tm.Stop();
            Controls.Clear();
            MessageBox.Show("Congratulations, you win!");
            score = 0; lblScore.Text = "Score: 0";
            Controls.Add(lblScore);
            Intialize();
        }

        void caldx(Control c)
        {
            dx = c.Left + c.Width / 2 < _ball.Left ?
                -Math.Abs(dx) : Math.Abs(dx);
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

        void createHearts()
        {
            _hearts = new List<PictureBox>();
            for (int i = 0; i < 3; i++)
            {
                PictureBox box = new PictureBox();
                box.Size = new Size(30, 30);
                box.Location = new Point(Width - (i + 1) * box.Width, 0);
                box.SizeMode = PictureBoxSizeMode.StretchImage;
                box.Image = Properties.Resources.heart;
                Controls.Add(box);
                _hearts.Add(box);
            }
        }
    }
}
