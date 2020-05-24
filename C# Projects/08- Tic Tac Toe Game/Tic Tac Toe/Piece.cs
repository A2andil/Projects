using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    enum States
    {
        X,
        O,
        F
    }

    class Piece : Button
    {
        public States state = States.F;
        public Piece(int x, int y)
        {
            Location = new System.Drawing.Point(x, y);
            Size = new System.Drawing.Size(100, 100);
        }
    }
}
