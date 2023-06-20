using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class PredictPiece : Piece
    {
        public PredictPiece(int x, int y, int z) : base(x, y, z)
        {
            this.Image = Properties.Resources.Predict;
        }
        public override PieceType GetPieceType()
        {
            return PieceType.Predict;
        }
    }
}
