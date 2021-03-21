using System.Diagnostics;

namespace Processing
{
    public class SmartizSketch : NetProcessing.Sketch
    {
        public double DeltaTime { get; private set; }

        private readonly Stopwatch _sw = new Stopwatch();

        public static void Translate(double x, double y)
        {
            NetProcessing.Sketch.Translate((int)x, (int)y);
        }

        public static void Line(double x1, double y1, double x2, double y2)
        {
            NetProcessing.Sketch.Line((int)x1, (int)y1, (int)x2, (int)y2);
        }

        public static void Point(double x1, double y1)
        {
            NetProcessing.Sketch.Point((int)x1, (int)y1);
        }

        public static void Circle(double x, double y, double d)
        {
            NetProcessing.Sketch.Ellipse((int)x, (int)y, (int)d, (int)d);
        }

        public static void Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            NetProcessing.Sketch.Triangle((int)x1, (int)y1, (int)x2, (int)y2, (int)x3, (int)y3);
        }

        public static void Image(PImage img, double x, double y, double w, double h)
        {
            NetProcessing.Sketch.Image(img, (int)x, (int)y, (int)w, (int)h);
        }

        public override void Setup()
        {
            base.Setup();

            _sw.Start();
        }

        public sealed override void Draw()
        {
            DeltaTime = _sw.ElapsedMilliseconds;
            _sw.Restart();
            DrawFrame();
        }

        public virtual void DrawFrame()
        {
            //SKIP
        }
    }
}
