using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    class btn_bx : Button
    {
        public btn_bx(int x, int y)
        {
            Location = new Point(x, y);
            Size = new Size(20, 20);
            BackColor = Color.Red;
            Enabled = false;
        }
    }
}
