using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    public partial class PredictPiece : Component
    {
        public PredictPiece()
        {
            InitializeComponent();
        }

        public PredictPiece(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
