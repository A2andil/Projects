using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChickenInvaders.UserDefined
{
    public class UnitBox : PictureBox
    {
        public int eggLandCount = 0, eggdownSpeed = 2;
        public UnitBox(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            SizeMode = PictureBoxSizeMode.StretchImage;
            BackColor = Color.Transparent;
        }

        public void resetLand()
        {
            eggLandCount = 0;
        }

        public bool collision(PictureBox x)
        {
            int left = x.Left - Width
                , right = x.Width + x.Left,
                top = x.Top - x.Height,
                bottom = x.Top + x.Height;

            if (Left >= left && Left <= right
                && Top >= top && Top <= bottom)
                return true;
            return false;
        }
    }
}
