using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChickenInvaders
{
    public partial class ChickenInvaders : Form
    {
        private int bulletSpeed = 3, chickenHeight = 100,
           count = 0, dt = 1, chickenLeftSpeed = 1, leftMostPosition = 0,
           live = 3, score = 0;

        public Piece _rocket;

        private List<Piece> _bullets;

        private List<Piece> _liveHearts = new List<Piece>();

        Random rand = new Random();

        //chickens
        private Bitmap _mainChickenImage;
        private List<Bitmap> _chickenFrames;
        private Piece[,] _chickens = new Piece[3, 8];
        private int[] topChickenRow = new int[3];      

        //eggs
        private Bitmap _mainBrokenEgg;
        private List<Bitmap> _brokenEggs;
        private List<Piece> _eggs = new List<Piece>();


        public ChickenInvaders()
        {
            InitializeComponent();
            intial();
        }

        private void intial()
        {
            _rocket = new Piece(100, 100);
            _rocket.Left = this.Width / 2 - _rocket.Width / 2;
            _rocket.Top = this.Height - _rocket.Height;
            _rocket.Image = Properties.Resources.ship;
            this.Controls.Add(_rocket);

            _bullets = new List<Piece>();

            //split chicken
            _mainChickenImage = new Bitmap(Properties.Resources.bossRed);
            _chickenFrames = new List<Bitmap>();
            dividIntoImages(_mainChickenImage, _chickenFrames, 10);

            //split broken egg
            _mainBrokenEgg = new Bitmap(Properties.Resources.eggBreak);
            _brokenEggs = new List<Bitmap>();
            dividIntoImages(_mainBrokenEgg, _brokenEggs, 8);

            createChickens();

            liveHearts(live);
        }

        private void liveHearts(int l)
        {
            Bitmap heart = new Bitmap(Properties.Resources.heart);
            for (int i = 0; i < 3; i++)
            {
                _liveHearts.Add(new Piece(50, 50));
                _liveHearts[i].Image = heart;
                _liveHearts[i].Left = Width - (3 - i) * 45;
                Controls.Add(_liveHearts[i]);
            }
        }

        private void dividIntoImages(Bitmap Orignal, List<Bitmap> result, int n)
        {
            int w = Orignal.Width / n, h = Orignal.Height;
            for (int i = 0; i < n; i++)
            {
                int s = i * w;
                Bitmap piece = new Bitmap(w, h);
                for (int y = 0; y < h; y++)
                    for (int x = 0; x < w; x++)
                        piece.SetPixel(x, y, Orignal.GetPixel(x + s, y));
                result.Add(piece);
            }
        }

        private void ChickenInvaders_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) luanchBullet();
        }

        private void ChickenInvaders_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Left:
                    _rocket.Left -= 10;
                    break;
                case Keys.Right:
                    _rocket.Left += 10;
                    break;
                case Keys.Up:
                    _rocket.Top -= 10;
                    break;
                case Keys.Down:
                    _rocket.Top += 10;
                    break;
            }
        }

        private void createChickens()
        {
            Bitmap img = _chickenFrames[0];
            chickenHeight = img.Height;
            for (int i = 0; i < 3; i++)
            {
                topChickenRow[i] = i * 100 + chickenHeight;
                for (int j = 0; j < 8; j++)
                {
                    Piece chicken = new Piece(img.Width, chickenHeight);
                    chicken.Image = _chickenFrames[1];
                    chicken.Left = j * 100;
                    chicken.Top = i * 100 + chickenHeight;
                    _chickens[i, j] = chicken;
                    Controls.Add(chicken);
                }
            }

        }

        private void chickenTimer_Tick(object sender, EventArgs e)
        {
            if (leftMostPosition + 800 > this.Width) chickenLeftSpeed = -1;
            else if (leftMostPosition <= 0) chickenLeftSpeed = 1;

            leftMostPosition += chickenLeftSpeed;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (_chickens[row, col] == null) continue;
                    _chickens[row, col].Image = _chickenFrames[count];
                    _chickens[row, col].Left += chickenLeftSpeed;
                }
            }

            count = count + dt;
            dt = count == 9 ? -1 : (count == 0 ? 1 : dt);
        }


        private void eggTimr_Tick(object sender, EventArgs e)
        {
            int r = rand.Next(200);
            if (r == 5) launchRandomEgg();

            for (int i = 0; i < _eggs.Count; i++)
            {
                _eggs[i].Top += _eggs[i].eggdownSpeed;
                if (_rocket.collision(_eggs[i]))
                {
                    Controls.Remove(_eggs[i]); _eggs.RemoveAt(i);
                    decreaseLive();
                    break;
                }
                if (_eggs[i].Top >= Height - (_eggs[i].Height + 20))
                {
                    _eggs[i].eggdownSpeed = 0;
                    if (_eggs[i].eggLandCount / 2 < _brokenEggs.Count)
                    {
                        _eggs[i].Image = _brokenEggs[_eggs[i].eggLandCount % 2];
                        _eggs[i].eggLandCount += 1;
                    }
                    else
                    {
                        Controls.Remove(_eggs[i]);
                        _eggs.RemoveAt(i);
                    }
                }
            }
        }

        private void launchRandomEgg()
        {
            List<Piece> availabeChickens = new List<Piece>();
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 8; j++)
                    if (_chickens[i, j] != null)
                        availabeChickens.Add(_chickens[i, j]);

            //select random chicken to drop egg from it
            Piece chicken = availabeChickens[rand.Next(availabeChickens.Count)];

            Piece egg = new Piece(10, 20);
            egg.Image = new Bitmap(Properties.Resources.egg);
            egg.Left = chicken.Left + chicken.Width / 2 - egg.Width / 2;
            egg.Top = chicken.Top + chicken.Height;
            _eggs.Add(egg); Controls.Add(egg);
        }

        private void decreaseLive()
        {
            live -= 1;
            _liveHearts[live].Image = new Bitmap(Properties.Resources.d_heart);
            if (live < 1) endGame(new Bitmap(Properties.Resources.lose));
        }

        private void endGame(Bitmap bitmap)
        {
            tm.Stop(); chickenTimer.Stop(); eggTimer.Stop();
            Controls.Clear();
            Piece congratulations = new Piece(100, 100);
            congratulations.Click += new EventHandler(cls);
            congratulations.Image = bitmap;
            congratulations.Left = Width / 2 - congratulations.Width / 2;
            congratulations.Top = Height / 2 - congratulations.Height / 2;
            Controls.Add(congratulations);
        }


        private void cls(object sender, EventArgs e)
        {
            Close();
        }

        private void tm_tick(object sender, EventArgs e)
        {
            for (int i = 0; i < _bullets.Count; i++)
                _bullets[i].Top -= bulletSpeed;

            collision();
            if (score == 240) endGame(new Bitmap(Properties.Resources.win));
        }

        private void luanchBullet()
        {
            Piece _bullet = new Piece(10, 10);
            _bullet.Left = _rocket.Left + _rocket.Width / 2 - _bullet.Width / 2;
            _bullet.Top = _rocket.Top - _bullet.Height;
            _bullet.Image = Properties.Resources.b2;
            this.Controls.Add(_bullet);
            _bullets.Add(_bullet);
        }

        private void collision()
        { 
            for (int i = 0; i < topChickenRow.Length; i++)
            {
                //binary search in bullets
                int lo = 0, hi = _bullets.Count - 1, md, ans = -1;
                while (lo <= hi)
                {
                    md = lo + (hi - lo) / 2;
                    if (_bullets[md].Top >= topChickenRow[i])
                    {
                        hi = md - 1;
                        ans = md;
                    }
                    else lo = md + 1;
                }
                if (ans != -1 && _bullets[ans].Top >= topChickenRow[i]
                    && _bullets[ans].Top <= topChickenRow[i] + chickenHeight)
                {
                    int j = (_bullets[ans].Left + 9 - leftMostPosition) / 100;
                       if (j >= 0 && j < 8 && _chickens[i, j] != null
                        && _bullets[ans].collision(_chickens[i, j]))
                    {
                        Controls.Remove(_bullets[ans]);
                        _bullets.RemoveAt(ans);
                        Controls.Remove(_chickens[i, j]);
                        _chickens[i, j] = null;
                        score += 10;
                        lblScore.Text = "Score: " + score.ToString();
                    }
                }
            }
            if (_bullets.Count > 0 && _bullets[0].Top <= -10) _bullets.RemoveAt(0);
        }
    }
}
