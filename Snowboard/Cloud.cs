using Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowboard
{
    public class Cloud : DrawableObject
    {
        public override void Draw()
        {
            SmartizSketch.Fill(255, 255, 255);
            SmartizSketch.Circle(X, Y, 30);
        }
    }
}
