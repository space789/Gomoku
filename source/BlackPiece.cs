﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class BlackPiece : Piece
    {
        public BlackPiece(int x, int y, int z) : base(x, y, z)
        {
            this.Image = Properties.Resources.black;
        }

        public override PieceType GetPieceType()
        {
            return PieceType.BLACK;
        }

    }
}
