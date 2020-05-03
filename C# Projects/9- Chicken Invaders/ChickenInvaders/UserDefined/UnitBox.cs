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
        public int eggLandCount;
        public UnitBox(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            SizeMode = PictureBoxSizeMode.StretchImage;
            BackColor = Color.Transparent;
            eggLandCount = 0;
        }

        public void resetLand()
        {
            eggLandCount = 0;
        }
    }
}
