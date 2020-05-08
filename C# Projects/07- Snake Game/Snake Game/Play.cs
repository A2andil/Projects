using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Play : Form
    {
        public int dx = 0, dy = 0, score = 0, front = 0, back = 0;
        public Button[] snake = new Button[1500];
        private List<int> available = new List<int>();
        Random rand = new Random();
        public int[,] visit;
        public Timer t;
        public Play() {
            InitializeComponent();
            visit = new int[this.Height / 20, this.Width / 20];
            btn_bx hd = new btn_bx((rand.Next() % 50) * 20, (rand.Next() % 25) * 20);
            goal.Location = new Point((rand.Next() % 50) * 20, (rand.Next() % 25) * 20);
            snake[front] = hd; Controls.Add(hd);
            for (int i = 0; i < 25; i++)
                for (int j = 0; j < 50; j++) {
                    visit[i, j] = 0;
                    available.Add(i * 50 + j);
                }
            visit[hd.Location.Y / 20, hd.Location.X / 20] = 1;
            available.Remove((hd.Location.Y / 20) * 25 + hd.Location.X / 20);
            userPlay();
        }

        void userPlay() {
            t = new Timer(); t.Interval = 100;
            t.Tick += new EventHandler(move);
            t.Start();
        }

        private void move(object sender, EventArgs e)
        {
            int x = snake[front].Location.X, y = snake[front].Location.Y;
            if (dx == 0 && dy == 0) return;
            if (game_over(x + dx, y + dy)) {
                t.Stop();
                MessageBox.Show("Game Over");
                return;
            }
            if (collision(x + dx, y + dy)) {
                score += 1;
                if (hits((y + dy) / 20, (x + dx) / 20)) return;
                this.Text = score.ToString();
                btn_bx hd = new btn_bx(0, 0);
                front = (front - 1 + 1500) % 1500; snake[front] = hd;
                snake[front].Location = new Point(x + dx, y + dy);
                visit[hd.Location.Y / 20, hd.Location.X / 20] = 1;
                Controls.Add(hd); food_random();
            }
            else {
                if (hits((y + dy) / 20, (x + dx) / 20)) return;
                visit[snake[back].Location.Y / 20, snake[back].Location.X / 20] = 0;
                front = (front - 1 + 1500) % 1500;
                snake[front] = snake[back];
                snake[front].Location = new Point(x + dx, y + dy);
                back = (back - 1 + 1500) % 1500;
                visit[(y + dy) / 20, (x + dx) / 20] = 1;
            }
        }

        private bool hits(int x, int y) {
            int ret = visit[x, y];
            if (ret == 1) {
                t.Stop();
                MessageBox.Show("Snake hits it's self");
                return true;
            }
            return false;
        }

        private bool game_over(int x, int y) {
            bool ans = x < 0 || y < 0 || x > 980 || y > 480;
            return ans;
        }

        private void food_random() {
            available.Clear();
            for (int i = 0; i < 500 / 20; i++)
                for (int j = 0; j < 1000 / 20; j++)
                    if (visit[i, j] == 0)
                        available.Add(i * (1000 / 20) + j);
            int idx = rand.Next(0, available.Count - 1);
            goal.Location = new Point((available[idx] * 20) % 1000,
                ((available[idx] * 20) / 1000) * 20);
        }

        private Boolean collision(int x, int y) {
            if (x == goal.Location.X && y == goal.Location.Y)
                return true;
            return false;
        }

        private void Play_KeyDown(object sender, KeyEventArgs e) {
            dx = dy = 0;
            switch (e.KeyCode) {
                case Keys.Right:
                    dx = 20;
                    break;
                case Keys.Left:
                    dx = -20;
                    break;
                case Keys.Up:
                    dy = -20;
                    break;
                case Keys.Down:
                    dy = 20;
                    break;
            }
        }
    }
}
