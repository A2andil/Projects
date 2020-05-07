using ChickenInvaders.UserDefined;
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
    public partial class Window : Form
    {
        private int rockSpeed = 10, bulletSpeed = 3,
           count = 0, dt = 1, chickenLeftSpeed = 1, startPchicken = 0;

        public UnitBox _rocket;
        private List<UnitBox> _bullets;

        private Bitmap _bossRed;
        private List<Bitmap> _bossRedPieces;

        private Bitmap _brokenEgg;
        private List<Bitmap> _brokenEggs;

        //Random
        Random rand = new Random();

        //chickens
        private UnitBox[,] _chickens = new UnitBox[3, 8];

        //eggs
        private List<UnitBox> _eggs = new List<UnitBox>();


        //live 
        private int live = 3, score = 0;
        private List<UnitBox> _liveHearts = new List<UnitBox>();

        //chickens last level
        private UnitBox[] lastLevelChickens = new UnitBox[8];

        public Window()
        {
            InitializeComponent();
            //rocket
            Opacity = 20;
            _rocket = new UnitBox(100, 100);
            _rocket.Left = this.Width / 2 - _rocket.Width / 2;
            _rocket.Top = this.Height - _rocket.Height;
            _rocket.BackColor = Color.Transparent;
            _rocket.Image = Properties.Resources.ship;
            this.Controls.Add(_rocket);
            _bullets = new List<UnitBox>();

            //split chicken
            _bossRed = new Bitmap(Properties.Resources.bossRed);
            _bossRedPieces = new List<Bitmap>();
            dividIntoImages(_bossRed, _bossRedPieces, 10);

            //split broken egg
            _brokenEgg = new Bitmap(Properties.Resources.eggBreak);
            _brokenEggs = new List<Bitmap>();
            dividIntoImages(_brokenEgg, _brokenEggs, 8);

            //create chickens
            createChickens();

            //fill live
            fillLive(live);
        }

        private void fillLive(int l)
        {
            Bitmap heart = new Bitmap(Properties.Resources.heart);
            for (int i = 0; i < 3; i++)
            {
                _liveHearts.Add(new UnitBox(50, 50));
                _liveHearts[i].Image = heart;
                _liveHearts[i].Left = Width - (3 - i) * 45;
                Controls.Add(_liveHearts[i]);
            }
        }

        private void createChickens()
        {
            Bitmap img = _bossRedPieces[0];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    UnitBox chicken = new UnitBox(img.Width, img.Height);
                    chicken.Image = _bossRedPieces[1];
                    chicken.Left = j * 100;
                    chicken.Top = i * 100 + img.Height;
                    _chickens[i, j] = chicken;
                    Controls.Add(chicken);
                    if (i == 2) lastLevelChickens[j] = chicken;
                }
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                leftRocket();
            else if (e.KeyCode == Keys.Right)
                rightRocket();
            else if (e.KeyCode == Keys.Up)
                _rocket.Top -= rockSpeed;
            else if (e.KeyCode == Keys.Down)
                _rocket.Top += rockSpeed;
            else if (e.KeyCode == Keys.Space)
                luanchBullet();
        }

        private void leftRocket()
        {
            _rocket.Left -= rockSpeed;
        }

        private void rightRocket()
        {
            _rocket.Left += rockSpeed;
        }

        private void luanchBullet()
        {
            UnitBox _bullet = new UnitBox(10, 10);
            _bullet.Left
                = _rocket.Left + _rocket.Width / 2 - _bullet.Width / 2;
            _bullet.Top = _rocket.Top - _bullet.Height;
            _bullet.Image = Properties.Resources.b2;
            this.Controls.Add(_bullet);
            _bullets.Add(_bullet);
            playSound();
            removeBullets();
        }

        private void removeBullets()
        {
            for (int i = 0; i < _bullets.Count; i++)
                if (!(_bullets[i].Top >= -10))
                    _bullets.RemoveAt(i);
        }

        private void tm3_Tick(object sender, EventArgs e)
        {

            int r = rand.Next(500);
            if (r == 5) createRandEgg();

            for (int i = 0; i < _eggs.Count; i++)
            {
                _eggs[i].Top += _eggs[i].eggdownSpeed;
                if (_rocket.collision(_eggs[i]))
                {
                    Controls.Remove(_eggs[i]);
                    _eggs.RemoveAt(i);
                    decreaseLive();
                    break;
                }
                if (_eggs[i].Top >= this.Height - (_eggs[i].Height + 20))
                {
                    _eggs[i].eggdownSpeed = 0;
                    if (_eggs[i].eggLandCount / 2 < _brokenEggs.Count)
                    {
                        _eggs[i].Image
                            = _brokenEggs[_eggs[i].eggLandCount % 2];
                        _eggs[i].eggLandCount++;
                    }
                    else
                    {
                        Controls.Remove(_eggs[i]);
                        _eggs.RemoveAt(i);
                    }

                }
            }
            win();
        }

        private void decreaseLive()
        {
            live--;
            _liveHearts[live].Image
                = new Bitmap(Properties.Resources.d_heart);
            if (live < 1)
                endGame(new Bitmap(Properties.Resources.lose));
        }

        private void win()
        {
            bool winner = false;
            for (int i = 0; i < lastLevelChickens.Length; i++)
                if (lastLevelChickens[i] != null)
                {
                    winner = true;
                    break;
                }
            if (!winner) endGame(new Bitmap(Properties.Resources.win));
        }

        private void endGame(Bitmap bitmap)
        {
            tm.Stop(); tm3.Stop();
            Controls.Clear();
            UnitBox congratulations = new UnitBox(100, 100);
            congratulations.Click += new EventHandler(cls);
            congratulations.Image = bitmap;
            congratulations.Left
                = Width / 2 - congratulations.Width / 2;
            congratulations.Top
                = Height / 2 - congratulations.Height / 2;
            Controls.Add(congratulations);
        }

        private void cls(object sender, EventArgs e)
        {
            Close();
        }

        private void createRandEgg()
        {
            List<int> rows = new List<int>();
            for (int i = 0; i < lastLevelChickens.Length; i++)
                if (lastLevelChickens[i] != null) rows.Add(i);

            //select random chicken last level to drop egg from it
            int r = rows[rand.Next(rows.Count)];


            UnitBox egg = new UnitBox(10, 20);
            egg.Image = new Bitmap(Properties.Resources.egg);
            egg.Left = lastLevelChickens[r].Left
                + lastLevelChickens[r].Width / 2 - egg.Width / 2;
            egg.Top = lastLevelChickens[r].Top
                    + lastLevelChickens[r].Height;
            _eggs.Add(egg);
            Controls.Add(egg);
        }

        private void Window_Load(object sender, EventArgs e)
        {

        }

        private void tm_tick(object sender, EventArgs e)
        {

            for (int i = 0; i < _bullets.Count; i++)
                _bullets[i].Top -= bulletSpeed;

            if (startPchicken + 800 > this.Width)
                chickenLeftSpeed = -1;
            else if (startPchicken <= 0)
                chickenLeftSpeed = 1;

            //enemies
            startPchicken += chickenLeftSpeed;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 8 ;col++)
                {
                    if (_chickens[row, col] == null) continue;
                    _chickens[row, col].Image = _bossRedPieces[count];
                    _chickens[row, col].Left += chickenLeftSpeed;
                }
            }

            //_enemy.Image = _bossRedPieces[count];
            count = count + dt;
            dt = count == 9 ? -1 : (count == 0 ? 1 : dt);

            //collision
            collision();
        }

        private void collision()
        {
            for (int i = 0; i < lastLevelChickens.Length; i++)
            {
                UnitBox chicken = lastLevelChickens[i];
                for (int j = 0; j < _bullets.Count && chicken != null; j++)
                {
                    if (checkCollision(_bullets[j], chicken, i, j))
                    {
                        score += 10;
                        label1.Text = "Score: " + score.ToString();
                        break;
                    }
                }
            }
        }

        private bool checkCollision(UnitBox bullet, UnitBox chicken, int i, int j)
        {
            if (bullet.collision(chicken))
            {
                Controls.Remove(bullet);
                Controls.Remove(chicken);
                _bullets[j] = _bullets[_bullets.Count - 1];
                _bullets.RemoveAt(_bullets.Count - 1);
                lastLevelChickens[i] = null;
                for (int x = 2; x >= 1; x--)
                {
                    if (_chickens[x, i] != null)
                    {
                        lastLevelChickens[i] = _chickens[x - 1, i];                    Controls.Remove(_chickens[x, i]);
                        _chickens[x, i] = null;
                        break;
                    } 
                }
                return true;
            }
            return false;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            //_rocket.Image = Properties.Resources.a2;
        }

        private void playSound()
        {
            //Thread _thread = new Thread(delegate ()
            //{
            //    Stream str = Properties.Resources.a;
            //    System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
            //    snd.PlaySync();
            //    SoundPlayer player = new SoundPlayer();
            //    player.SoundLocation = Properties.Resources.bulletSound;
            //    player.Load();
            //    player.Play();
            //});
            //_thread.Start();
        }
    }
}
