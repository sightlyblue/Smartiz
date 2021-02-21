namespace Snowboard
{
    public abstract class DrawableObject
    {
        public double X { get; set; }
        public double Y { get; set; }

        public abstract void Draw();
    }
}
