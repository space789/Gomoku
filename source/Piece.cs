using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    abstract class Piece : PictureBox
    {
        private static readonly int IMAGE_WIDTH = 30;

        public Piece(int x, int y, int z)
        {
            this.BackColor = Color.Transparent;
            this.Location = new Point(x - IMAGE_WIDTH / 2, y - IMAGE_WIDTH / 2);
            this.Size = new Size(IMAGE_WIDTH, IMAGE_WIDTH);
            this.Name = ("PiecePictureBox" + z);
        }

        public abstract PieceType GetPieceType();
    }
}
