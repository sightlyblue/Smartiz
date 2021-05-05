using NetProcessing;

namespace Processing
{
    public class Tongue : DrawableObject
    {
        public Sketch.PImage tongue { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Margin { get; set; }
        public int Speed { get; set; }

        public Tongue(int width, int height)
        {
            tongue = Sketch.LoadImage("imgs\\plant.png");
            this.Width = width / 4;
            this.Height = height / 4;
            this.Margin = Width / 4;
            this.X = width / 3;
            this.Y = height - Height + 30;
        }

        public override void Draw()
        {
            SmartizSketch.Image(tongue, X, Y, Width, Height);
        }

        public void Move(int width)
        {
            X += Speed;
            if (X <= -50)
            {
                X = -50;
            }
            if (X >= width - 80)
            {
                X = width - 80;
            }
        }
    }
}