using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Gomoku
{
    class PieceComputer : Label
    {
        private static readonly int IMAGE_WIDTH = 10;

        public PieceComputer(int x, int y, int z)
        {
            this.BackColor = Color.Yellow;
            this.ForeColor = Color.Black;
            if (z < 10)
            {
                this.Location = new Point(x - IMAGE_WIDTH / 2, y - IMAGE_WIDTH / 2);
                this.Size = new Size(IMAGE_WIDTH, IMAGE_WIDTH);
            }
            if (z < 100 && z >= 10)
            {
                this.Location = new Point((x - IMAGE_WIDTH / 2) - 2, y - IMAGE_WIDTH / 2);
                this.Size = new Size(IMAGE_WIDTH + 5, IMAGE_WIDTH);
            }
            if (z >= 100)
            {
                this.Location = new Point((x - IMAGE_WIDTH / 2) - 6, y - IMAGE_WIDTH / 2);
                this.Size = new Size(IMAGE_WIDTH + 12, IMAGE_WIDTH);
            }
            this.TextAlign = ContentAlignment.MiddleCenter;
            z--;
            this.Name = ("NumberLabel" + z);
            z++;
            this.Text = (z + "");
        }
    }
}
