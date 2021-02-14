using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Processing
{
    public class SmartizSketch : NetProcessing.Sketch
    {
        public double DeltaTime { get; private set; }

        private readonly Stopwatch _sw = new Stopwatch();

        public static void Line(double x1, double y1, double x2, double y2)
        {
            NetProcessing.Sketch.Line((int)x1, (int)y1, (int)x2, (int)y2);
        }


        public void Circle(double x, double y, double d)
        {
            NetProcessing.Sketch.Ellipse((int)x, (int)y, (int)d, (int)d);
        }

        public void Image(PImage img, double x, double y, double w, double h)
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

    class Program : SmartizSketch
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new Program().Start();
        }

        double[] X;
        double[] Y;
        double[] z;
        int n = 30;
        double[] Vy;
        PImage tongue;
        double tX;
        double tY;
        double tWidth;
        double tHeight;
        double tMargin;
        int tSpeed = 0;
        int[] op;
        int counter;
        double timer;

        public override void Setup()
        {
            tongue = LoadImage("imgs\\tongue.png");

            Size(600, 600);
            Background(21, 34, 56);
            counter = n - 10;
            timer = 1000 * (counter + 10);

            X = new double[n];
            Y = new double[n];
            z = new double[n];
            Vy = new double[n];
            op = new int[n];

            for (int i = 0; i < n; i++)
            {
                X[i] = Random(Width - 10);
                Y[i] = Random(Height);
                z[i] = Random(7.12);
                Vy[i] = Random(1, 5);
                op[i] = 255;
            }
            tWidth = Width / 4;
            tHeight = Height / 4;
            tMargin = tWidth / 4;
            tX = Width / 3;
            tY = Height - tHeight + 30;
        }

        public override void DrawFrame()
        {
            if (!isGameover())
            {
                timer -= DeltaTime;
            }
            Background(21, 34, 56);
            drawTongue();
            moveTongue();
            for (int i = 0; i < n; i++)
            {
                moveSnowflake(i);
                updateSnowflake(i);
                drawSnowflake(i);
            }
            drawDashboard();
        }

        private void drawDashboard()
        {
            Fill(255);
            TextSize(25);
            Text("Timer: " + Round(timer / 1000), 20, 40);
            Text("Counter: " + counter, Width - 150, 40);
            Fill(255);
            //textStyle(BOLD);

            TextSize(40);
            if (counter == 0)
            {
                Text("YOU WIN! :)", Width / 2 - 100, Height / 2);
            }
            else if (timer < 0)
            {

                Text("YOU LOSE! :(", Width / 2 - 100, Height / 2);
            }
        }

        private void drawSnowflake(int i)
        {
            StrokeWeight(1);
            Stroke(255, 255, 255, Math.Max(0, op[i]));
            NoFill();
            Line(X[i], Y[i], X[i], Y[i] - 2 * z[i]);
            Line(X[i], Y[i], X[i] + 2 * z[i], Y[i] - 1.5 * z[i]);
            Line(X[i], Y[i], X[i] + 2 * z[i], Y[i] + 1.5 * z[i]);
            Line(X[i], Y[i], X[i], Y[i] + 2 * z[i]);
            Line(X[i], Y[i], X[i] - 2 * z[i], Y[i] - 1.5 * z[i]);
            Line(X[i], Y[i], X[i] - 2 * z[i], Y[i] + 1.5 * z[i]);

            Circle(X[i], Y[i] - 2.5 * z[i], z[i]);
            Circle(X[i] + 2 * z[i], Y[i] - 1.5 * z[i], z[i]);
            Circle(X[i] + 2 * z[i], Y[i] + 1.5 * z[i], z[i]);
            Circle(X[i], Y[i] + 2.5 * z[i], z[i]);
            Circle(X[i] - 2 * z[i], Y[i] - 1.5 * z[i], z[i]);
            Circle(X[i] - 2 * z[i], Y[i] + 1.5 * z[i], z[i]);

            Circle(X[i], Y[i], 2.5 * z[i]);
            Circle(X[i], Y[i], 1.5 * z[i]);

            Circle(X[i], Y[i] - 2.5 * z[i], 0.75 * z[i]);
            Circle(X[i] + 2 * z[i], Y[i] - 1.5 * z[i], 0.75 * z[i]);
            Circle(X[i] + 2 * z[i], Y[i] + 1.5 * z[i], 0.75 * z[i]);
            Circle(X[i], Y[i] + 2.5 * z[i], 0.75 * z[i]);
            Circle(X[i] - 2 * z[i], Y[i] - 1.5 * z[i], 0.75 * z[i]);
            Circle(X[i] - 2 * z[i], Y[i] + 1.5 * z[i], 0.75 * z[i]);

            Circle(X[i], Y[i], z[i]);
        }

        private void moveSnowflake(int i)
        {
            X[i] = Sin(20 * Y[i]) + X[i];
            Y[i] += Vy[i];
        }

        private bool isOnTongue(int i)
        {
            return Y[i] > tY + tHeight / 2 && X[i] > tX + tMargin && X[i] < tX + tWidth - tMargin && Y[i] < Height - tMargin;
        }

        private void updateSnowflake(int i)
        {
            if (isOnTongue(i))
            {
                op[i] -= 60;
                if (op[i] < 255 && !isGameover())
                {
                    counter--;
                }
            }
            else if (Y[i] > Height + z[i])
            {
                Y[i] = -100;
                X[i] += Random(-30, 30);
                op[i] = 255;
            }
        }

        private void drawTongue()
        {
            Image(tongue, tX, tY, tWidth, tHeight);
        }

        private void moveTongue()
        {
            tX += tSpeed;
            if (tX <= -50)
            {
                tX = -50;
            }
            if (tX >= Width - 50)
            {
                tX = Width - 50;
            }
        }

        public override void KeyReleased()
        {
            tSpeed = 0;
        }

        public override void KeyPressed()
        {
            if (KeyCode == KC_LEFT)
            {
                tSpeed = -5;
            }
            else if (KeyCode == KC_RIGHT)
            {
                tSpeed = 5;
            }
        }

        private bool isGameover()
        {
            return counter == 0 || timer < 0;
        }

    }
}
