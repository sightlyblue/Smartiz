using Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowboard
{
    //TODO: height and width as coonstructor parameter
    public class Tree : DrawableObject
    {
        public int Width { get; }
        public int Height { get; }

        public Tree(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public override void Draw()
        {
            SmartizSketch.Fill(0, 200, 0);
            SmartizSketch.Triangle(0, 0, Width/2, Height, -Width/2, Height);
        }
    }

}
