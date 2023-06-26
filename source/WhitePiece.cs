using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class WhitePiece : Piece
    {
        public WhitePiece(int x, int y, int z) : base(x, y, z)
        {
            this.Image = Properties.Resources.white;
        }

        public override PieceType GetPieceType()
        {
            return PieceType.WHITE;
        }
    }
}
