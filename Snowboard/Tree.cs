using Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowboard
{
    public class Tree : DrawableObject
    {
        public override void Draw()
        {
            SmartizSketch.Fill(0, 200, 0);
            SmartizSketch.Triangle(X, Y, X + 50, Y + 200, X - 50, Y + 200);
        }
    }

}
