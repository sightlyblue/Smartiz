using Processing;
using NetProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing
{
    public abstract class DrawableObject
    {
        public double X { get; set; }
        public double Y { get; set; }

        public abstract void Draw();
    }
}
