using System;
using System.Collections.Generic;
using System.Linq;

namespace Processing
{
    public class Snowflake
    {
        public double X;
        public double Y;
        public double z;
        public double Vy;
        public int op;
    }

    public class Program : SmartizSketch
    {
        [STAThread]
        static void Main()
        {
            new Program().Start();
        }

        Snowflake[] snowflakes;

        PImage tongue;
        double tX;
        double tY;
        double tWidth;
        double tHeight;
        double tMargin;
        int tSpeed = 0;
        int counter;
        double timer;

        public override void Setup()
        {
            tongue = LoadImage("imgs\\tongue.png");

            Size(600, 600);
            Background(21, 34, 56);

            snowflakes = new Snowflake[30];

            counter = snowflakes.Length - 10;
            timer = 1000 * (counter + 10);

            for (int i = 0; i < snowflakes.Length; i++)
            {
                snowflakes[i] = new Snowflake();
                snowflakes[i].X = Random(Width - 10);
                snowflakes[i].Y = Random(Height);
                snowflakes[i].z = Random(7.12);
                snowflakes[i].Vy = Random(1, 5);
                snowflakes[i].op = 255;
            }
            tWidth = Width / 4;
            tHeight = Height / 4;
            tMargin = tWidth / 4;
            tX = Width / 3;
            tY = Height - tHeight + 30;
        }

        public override void DrawFrame()
        {
            if (!IsGameover())
            {
                timer -= DeltaTime;
            }
            Background(21, 34, 56);
            DrawTongue();
            MoveTongue();
            foreach (var s in snowflakes)
            {
                MoveSnowflake(s);
                UpdateSnowflake(s);
                DrawSnowflake(s);
            }
            DrawDashboard();
        }

        private void DrawDashboard()
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
                Text("YOU WON! :)", Width / 2 - 100, Height / 2);
            }
            else if (timer < 0)
            {
                Text("YOU LOST! :(", Width / 2 - 100, Height / 2);
            }
        }

        private void DrawSnowflake(Snowflake s)
        {
            StrokeWeight(1);
            Stroke(255, 255, 255, Math.Max(0, s.op));
            NoFill();
            Line(s.X, s.Y, s.X, s.Y - 2 * s.z);
            Line(s.X, s.Y, s.X + 2 * s.z, s.Y - 1.5 * s.z);
            Line(s.X, s.Y, s.X + 2 * s.z, s.Y + 1.5 * s.z);
            Line(s.X, s.Y, s.X, s.Y + 2 * s.z);
            Line(s.X, s.Y, s.X - 2 * s.z, s.Y - 1.5 * s.z);
            Line(s.X, s.Y, s.X - 2 * s.z, s.Y + 1.5 * s.z);

            Circle(s.X, s.Y - 2.5 * s.z, s.z);
            Circle(s.X + 2 * s.z, s.Y - 1.5 * s.z, s.z);
            Circle(s.X + 2 * s.z, s.Y + 1.5 * s.z, s.z);
            Circle(s.X, s.Y + 2.5 * s.z, s.z);
            Circle(s.X - 2 * s.z, s.Y - 1.5 * s.z, s.z);
            Circle(s.X - 2 * s.z, s.Y + 1.5 * s.z, s.z);

            Circle(s.X, s.Y, 2.5 * s.z);
            Circle(s.X, s.Y, 1.5 * s.z);

            Circle(s.X, s.Y - 2.5 * s.z, 0.75 * s.z);
            Circle(s.X + 2 * s.z, s.Y - 1.5 * s.z, 0.75 * s.z);
            Circle(s.X + 2 * s.z, s.Y + 1.5 * s.z, 0.75 * s.z);
            Circle(s.X, s.Y + 2.5 * s.z, 0.75 * s.z);
            Circle(s.X - 2 * s.z, s.Y - 1.5 * s.z, 0.75 * s.z);
            Circle(s.X - 2 * s.z, s.Y + 1.5 * s.z, 0.75 * s.z);

            Circle(s.X, s.Y, s.z);
        }

        private void MoveSnowflake(Snowflake s)
        {
            s.X = Sin(20 * s.Y) + s.X;
            s.Y += s.Vy;
        }

        private bool IsOnTongue(Snowflake s)
        {
            return s.Y > tY + tHeight / 2 && s.X > tX + tMargin && s.X < tX + tWidth - tMargin && s.Y < Height - tMargin;
        }

        private void UpdateSnowflake(Snowflake s)
        {
            if (IsOnTongue(s))
            {
                s.op -= 60;
                s.Vy -= 0.5;
                if (s.op < 255 && !IsGameover())
                {
                    counter--;
                }
            }
            else if (s.Y > Height + s.z)
            {
                s.Y = -100;
                s.X += Random(-30, 30);
                s.op = 255;
            }
        }

        private void DrawTongue()
        {
            Image(tongue, tX, tY, tWidth, tHeight);
        }

        private void MoveTongue()
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

        private bool IsGameover()
        {
            return counter == 0 || timer < 0;
        }

    }
}
