using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class Cell
    {
        public Figure figure;

        public Cell()
        {
            this.figure = null;
        }

        public Cell(Figure figure)
        {
            this.figure = figure;
        }

        public bool MoveFigure(Cell target)
        {
            if (target.figure == null)
            {
                target.figure = figure;
                figure = null;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
