using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NetProcessing;
using Processing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing
{
    public class SnowflakeClass : DrawableObject
    {
        public int drop { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Size { get; }
        public double DefaultV { get; }
        public double Velocity { get; set; }
        public int Opacity { get; set; }
        public bool Enemy { get; set; }

        private static Random random = new Random();

        public SnowflakeClass(double X, double Y, double Size, double Velocity, bool Enemy)
        {
            drop = Enemy ? 0 : random.Next(3)+1;
            this.X = X;
            this.Y = Y;
            this.Size = Size;
            this.DefaultV = Velocity;
            this.Velocity = Velocity;
            this.Opacity = 4;
            this.Enemy = Enemy;
        }

        public static Sketch.PImage[,] raindrops = new Sketch.PImage[4,5];

        public static void LoadPictures()
        {
            raindrops[0, 0] = Sketch.LoadImage("imgs\\enemy.png");
            raindrops[1, 0] = Sketch.LoadImage("imgs\\rnd1.png");
            raindrops[2, 0] = Sketch.LoadImage("imgs\\rnd2.png");
            raindrops[3, 0] = Sketch.LoadImage("imgs\\rnd3.png");
            for (int i = 1; i < 5; i++)
            {
                raindrops[0,i] = SetImageAlpha(raindrops[0, 0], 255 - i * 60);
                raindrops[1,i] = SetImageAlpha(raindrops[1, 0], 255 - i * 60);
                raindrops[2,i] = SetImageAlpha(raindrops[2, 0], 255 - i * 60);
                raindrops[3,i] = SetImageAlpha(raindrops[3, 0], 255 - i * 60);
            }
        }

        public static Sketch.PImage SetImageAlpha (Sketch.PImage original, int alpha)
        {
            Sketch.PImage modified = original.Get(0,0,original.Width,original.Height);
            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Sketch.Color color = original.Get(x, y);
                    modified.Set(x, y, new Sketch.Color(Sketch.Red(color), Sketch.Green(color), Sketch.Blue(color), Math.Min(Sketch.Alpha(color), alpha)));
                }
            }
            return modified;
        }
        public override void Draw()
        {
            /*
            SmartizSketch.StrokeWeight(1);
            SmartizSketch.Stroke(255, Enemy ? 0 : 255, Enemy ? 0 : 255, Math.Max(0, Opacity));
            SmartizSketch.NoFill();
            SmartizSketch.Line(X, Y, X, Y - 2 * Size);
            SmartizSketch.Line(X, Y, X + 2 * Size, Y - 1.5 * Size);
            SmartizSketch.Line(X, Y, X + 2 * Size, Y + 1.5 * Size);
            SmartizSketch.Line(X, Y, X, Y + 2 * Size);
            SmartizSketch.Line(X, Y, X - 2 * Size, Y - 1.5 * Size);
            SmartizSketch.Line(X, Y, X - 2 * Size, Y + 1.5 * Size);

            SmartizSketch.Circle(X, Y - 2.5 * Size, Size);
            SmartizSketch.Circle(X + 2 * Size, Y - 1.5 * Size, Size);
            SmartizSketch.Circle(X + 2 * Size, Y + 1.5 * Size, Size);
            SmartizSketch.Circle(X, Y + 2.5 * Size, Size);
            SmartizSketch.Circle(X - 2 * Size, Y - 1.5 * Size, Size);
            SmartizSketch.Circle(X - 2 * Size, Y + 1.5 * Size, Size);

            SmartizSketch.Circle(X, Y, 2.5 * Size);
            SmartizSketch.Circle(X, Y, 1.5 * Size);

            SmartizSketch.Circle(X, Y - 2.5 * Size, 0.75 * Size);
            SmartizSketch.Circle(X + 2 * Size, Y - 1.5 * Size, 0.75 * Size);
            SmartizSketch.Circle(X + 2 * Size, Y + 1.5 * Size, 0.75 * Size);
            SmartizSketch.Circle(X, Y + 2.5 * Size, 0.75 * Size);
            SmartizSketch.Circle(X - 2 * Size, Y - 1.5 * Size, 0.75 * Size);
            SmartizSketch.Circle(X - 2 * Size, Y + 1.5 * Size, 0.75 * Size);

            SmartizSketch.Circle(X, Y, Size);*/
            int index = Math.Min(4, 4 - Opacity);
            SmartizSketch.Image(raindrops[drop,index], X, Y, Size, Size*139/98);
        }

        public void Move()
        {
            //X = Math.Sin(0.25*Y)*0.5 X;
            Y += Velocity;
        }

        public void Melt()
        {
            Opacity--;
            Velocity = Velocity * 0.75;
        }

        public bool IsMelted()
        {
            return Opacity < 0;
        }

        public void Refresh()
        {
            Y = -100;
            X += SmartizSketch.Random(-30, 30);
            Opacity = 4;
            Velocity = DefaultV;
        }
    }
}