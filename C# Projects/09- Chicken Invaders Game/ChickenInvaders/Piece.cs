using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChickenInvaders
{
    public class Piece : PictureBox
    {
        public int eggLandCount = 0, eggdownSpeed = 2;

        public Piece(int width, int height)
        {
            Width = width;
            Height = height;
            SizeMode = PictureBoxSizeMode.StretchImage;
            BackColor = Color.Transparent;
        }

        public void resetLand() => eggLandCount = 0;
        

        public bool collision(Piece enemy) => Bounds.IntersectsWith(enemy.Bounds);
    }
}
