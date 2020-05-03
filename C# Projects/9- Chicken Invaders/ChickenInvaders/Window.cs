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
        public UnitBox _rocket;
        private List<UnitBox> _bullets;
        private int rockSpeed = 10, bulletSpeed = 3, downSpeed = 2,
            count = 0, dt = 1, chickenLeftSpeed = 1, startPchicken = 0;

        private Bitmap _bossRed;
        private List<Bitmap> _bossRedPieces;

        private UnitBox _egg;
        private Bitmap _brokenEgg;
        private List<Bitmap> _brokenEggs;

        //chickens
        List<UnitBox>[] _chickens = new List<UnitBox>[3];



        public Window()
        {
            InitializeComponent();
            tm.Stop();
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

            //test egg
            _egg = new UnitBox(10, 12);
            //_egg.Left = _enemy.Left + _enemy.Width / 2 - _egg.Width / 2;
            //_egg.Top = _enemy.Top + _enemy.Height;
            _egg.Location = new Point(Width / 2, 100);
            _egg.Image = Properties.Resources.egg;
            Controls.Add(_egg);
            tm.Start();

            //create chickens
            createChickens();
        }

        private void createChickens()
        {
            Bitmap img = _bossRedPieces[0];
            for (int i = 0; i < 3; i++)
            {
                _chickens[i] = new List<UnitBox>();
                for (int j = 0; j < 8; j++)
                {
                    UnitBox chicken = new UnitBox(img.Width, img.Height);
                    chicken.Image = _bossRedPieces[1];
                    chicken.Left = j * 100;
                    chicken.Top = i * 100;
                    _chickens[i].Add(chicken);
                    Controls.Add(chicken);
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
            for (int row = 0; row < _chickens.Length; row++)
            {
                for (int col = 0; col < _chickens[row].Count; col++)
                {
                    _chickens[row][col].Image = _bossRedPieces[count];
                    _chickens[row][col].Left += chickenLeftSpeed;
                }
            }

            //_enemy.Image = _bossRedPieces[count];
            count = count + dt;
            dt = count == 9 ? -1 : (count == 0 ? 1 : dt);
            _egg.Top += downSpeed;

            //reset postion
            if (_egg.Top >= this.Height - (_egg.Height + 20))
            {
                downSpeed = 0;
                if (_egg.eggLandCount < _brokenEggs.Count)
                {
                    _egg.Image = _brokenEggs[_egg.eggLandCount];
                    _egg.eggLandCount++;
                }
                else
                {
                    _egg.Top = _egg.Height;
                    _egg.resetLand();
                    downSpeed = 2;
                    _egg.Image = new Bitmap(Properties.Resources.egg);
                }
            }

            //collision
            collision();
        }

        private void collision()
        {
            for (int row = 0; row < _chickens.Length; row++)
            {
                for (int col = 0; col < _chickens[row].Count; col++)
                {
                    for (int i = 0; i < _bullets.Count; i++)
                    {
                        if (checkCollision(_bullets[i],
                            _chickens[row][col], i, row, col))
                            break;
                    }
                }
            }
        }

        private bool checkCollision(UnitBox bullet, UnitBox chicken,
            int i, int row, int col)
        {
            int left = chicken.Left - bullet.Width
                , right = chicken.Width + chicken.Left,
                top = chicken.Top - bullet.Height,
                bottom = chicken.Top + chicken.Height;

            if (bullet.Left >= left && bullet.Left <= right
                && bullet.Top >= top && bullet.Top <= bottom)
            {
                Controls.Remove(bullet);
                Controls.Remove(chicken);
                _bullets.RemoveAt(i);
                _chickens[row].RemoveAt(col);
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
