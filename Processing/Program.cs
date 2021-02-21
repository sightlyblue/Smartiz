using System;
using System.Collections.Generic;
using System.Linq;

namespace Processing
{
    class Program : SmartizSketch
    {
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
            if (!IsGameover())
            {
                timer -= DeltaTime;
            }
            Background(21, 34, 56);
            DrawTongue();
            MoveTongue();
            for (int i = 0; i < n; i++)
            {
                MoveSnowflake(i);
                UpdateSnowflake(i);
                DrawSnowflake(i);
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
                Text("YOU WIN! :)", Width / 2 - 100, Height / 2);
            }
            else if (timer < 0)
            {
                Text("YOU LOSE! :(", Width / 2 - 100, Height / 2);
            }
        }

        private void DrawSnowflake(int i)
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

        private void MoveSnowflake(int i)
        {
            X[i] = Sin(20 * Y[i]) + X[i];
            Y[i] += Vy[i];
        }

        private bool IsOnTongue(int i)
        {
            return Y[i] > tY + tHeight / 2 && X[i] > tX + tMargin && X[i] < tX + tWidth - tMargin && Y[i] < Height - tMargin;
        }

        private void UpdateSnowflake(int i)
        {
            if (IsOnTongue(i))
            {
                op[i] -= 60;
                Vy[i] -= 0.5;
                if (op[i] < 255 && !IsGameover())
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
