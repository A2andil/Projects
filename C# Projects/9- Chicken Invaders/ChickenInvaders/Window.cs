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
        private int rockSpeed = 10, bulletSpeed = 3, chickenHeight = 100,
           count = 0, dt = 1, chickenLeftSpeed = 1, startPchicken = 0,
            live = 3, score = 0;

        public UnitBox _rocket;

        private List<UnitBox> _bullets;

        private List<UnitBox> _liveHearts = new List<UnitBox>();

        Random rand = new Random();

        //chickens
        private Bitmap _bossRed;
        private List<Bitmap> _bossRedPieces;
        private UnitBox[,] _chickens = new UnitBox[3, 8];
        private int[] topChickenRowPosition = new int[3];      

        //eggs
        private Bitmap _brokenEgg;
        private List<Bitmap> _brokenEggs;
        private List<UnitBox> _eggs = new List<UnitBox>();


        public Window()
        {
            InitializeComponent();

            _rocket = new UnitBox(100, 100);
            _rocket.Left = this.Width / 2 - _rocket.Width / 2;
            _rocket.Top = this.Height - _rocket.Height;
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


        #region global functions
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
        #endregion

        #region keydowns
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
        #endregion


        #region chicken
        private void createChickens()
        {
            Bitmap img = _bossRedPieces[0];
            chickenHeight = img.Height;
            for (int i = 0; i < 3; i++)
            {
                topChickenRowPosition[i] = i * 100 + chickenHeight;
                for (int j = 0; j < 8; j++)
                {
                    UnitBox chicken = new UnitBox(img.Width, chickenHeight);
                    chicken.Image = _bossRedPieces[1];
                    chicken.Left = j * 100;
                    chicken.Top = i * 100 + chickenHeight;
                    _chickens[i, j] = chicken;
                    Controls.Add(chicken);
                }
            }

        }

        private void chickeTimer_Tick(object sender, EventArgs e)
        {
            if (startPchicken + 800 > this.Width) chickenLeftSpeed = -1;
            else if (startPchicken <= 0) chickenLeftSpeed = 1;

            startPchicken += chickenLeftSpeed;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (_chickens[row, col] == null) continue;
                    _chickens[row, col].Image = _bossRedPieces[count];
                    _chickens[row, col].Left += chickenLeftSpeed;
                }
            }

            count = count + dt;
            dt = count == 9 ? -1 : (count == 0 ? 1 : dt);
        }
        #endregion


        #region Timer, Collision and Creation Eggs
        private void eggTimr_Tick(object sender, EventArgs e)
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
        }

        private void createRandEgg()
        {

            List<UnitBox> rows = new List<UnitBox>();
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 8; j++)
                    if (_chickens[i, j] != null)
                        rows.Add(_chickens[i, j]);

            //select random chicken last level to drop egg from it
            UnitBox chicken = rows[rand.Next(rows.Count)];


            UnitBox egg = new UnitBox(10, 20);
            egg.Image = new Bitmap(Properties.Resources.egg);
            egg.Left = chicken.Left + chicken.Width / 2 - egg.Width / 2;
            egg.Top = chicken.Top + chicken.Height;
            _eggs.Add(egg);
            Controls.Add(egg);
        }

        private void decreaseLive()
        {
            live--;
            _liveHearts[live].Image
                = new Bitmap(Properties.Resources.d_heart);
            if (live < 1)
                endGame(new Bitmap(Properties.Resources.lose));
        }

        private void endGame(Bitmap bitmap)
        {
            tm.Stop(); chickeTimer.Stop(); eggTimer.Stop();
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

        #endregion


        #region bullets and collion and win or lose
        private void tm_tick(object sender, EventArgs e)
        {
            for (int i = 0; i < _bullets.Count; i++)
                _bullets[i].Top -= bulletSpeed;

            collision();
            win();
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
            removeBullets();
        }

        private void removeBullets()
        {
            for (int i = 0; i < _bullets.Count; i++)
                if (!(_bullets[i].Top >= -10))
                    _bullets.RemoveAt(i);
        }

        private void collision()
        { 
            for (int i = 0; i < _bullets.Count; i++)
            {
                UnitBox bullet = _bullets[i];
                int selectedRow = -1;
                for (int l = topChickenRowPosition.Length - 1; l >= 0 ; l--)
                    if (bullet.Top >= topChickenRowPosition[l] 
                        && bullet.Top <= topChickenRowPosition[l] + chickenHeight)
                    {      
                        selectedRow = l;
                        break;
                    }
                if (selectedRow != -1)
                {
                    for (int j = 0; j < _chickens.GetLength(1); j++)
                    { 
                        UnitBox chicken = _chickens[selectedRow, j];
                        if (chicken != null 
                            && checkCollision(bullet, chicken, selectedRow, j, i))
                        {
                            score += 10;
                            label1.Text = "Score: " + score.ToString();
                            break;
                        }
                    }
                }
            }
        }

        private bool checkCollision(UnitBox bullet, UnitBox chicken, int rw, int j
            , int i) {
            if (bullet.collision(chicken))
            {
                Controls.Remove(bullet);
                Controls.Remove(chicken);
                _bullets[i] = _bullets[_bullets.Count - 1];
                _bullets.RemoveAt(_bullets.Count - 1);
                Controls.Remove(_chickens[rw, j]);
                _chickens[rw, j] = null;
                return true;
            }
            return false;
        }

        private void win()
        {
            if (score == 240) endGame(new Bitmap(Properties.Resources.win));
        }
        #endregion

    }
}
