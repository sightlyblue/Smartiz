using Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowboard
{
    public class Sun : DrawableObject
    {
        public int Size { get; }

        public Sun(int size)
        {
            this.Size = size;
        }

        public override void Draw()
        {
            SmartizSketch.NoStroke();
            SmartizSketch.Fill(249, 215, 28);
            SmartizSketch.Circle(0, 0, Size);
        }
    }
}
