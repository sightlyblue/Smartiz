using System;
using System.Collections.Generic;
using System.Linq;

namespace Processing
{
    public class Program : SmartizSketch
    {
        [STAThread]
        static void Main()
        {
            SnowflakeClass.LoadPictures();
            new Program().Start();
        }

        SnowflakeClass[] Snowflakes;
        Tongue Tongue;
        int Counter;
        double Timer;
        bool TouchedRed;

        public override void Setup()
        {
            Size(600, 600);
            Background(21, 34, 56);



            Tongue = new Tongue(Width, Height);
            Snowflakes = new SnowflakeClass[50];

            Counter = Snowflakes.Length / 2;
            Timer = 1000 * (Snowflakes.Length);

            TouchedRed = false;

            for (int i = 0; i < Snowflakes.Length; i++)
            {
                Snowflakes[i] = new SnowflakeClass(Random(Width - 10), Random(Height/2), Random(20,50), Random(1, 5), i%10 == 0);
            }
        }

        public override void DrawFrame()
        {
            if (!IsGameover())
            {
                Timer -= DeltaTime;
                Background(21, 34, 56);
                Tongue.Draw();
                Tongue.Move(Width);
                foreach (var snow in Snowflakes)
                {
                    snow.Move();
                    UpdateSnowflake(snow);
                    snow.Draw();
                }
                DrawDashboard();
            }
        }

        private void DrawDashboard()
        {
            Fill(255);
            TextSize(25);
            Text("Timer: " + Round(Timer / 1000), 20, 40);
            Text("Counter: " + Counter, Width - 150, 40);
            Fill(255);

            TextSize(40);
            if (Counter == 0)
            {
                Text("YOU WON! :)", Width / 2 - 100, Height / 2);
            }
            else if (Timer < 0 || TouchedRed)
            {
                Text("YOU LOST! :(", Width / 2 - 100, Height / 2);
            }
        }

        private void UpdateSnowflake(SnowflakeClass snow)
        {
            if (IsOnTongue(snow))
            {
                if (snow.Enemy)
                {
                    TouchedRed = true;
                }
                snow.Melt();

                if (snow.IsMelted() && !IsGameover())
                {
                    Counter--;
                    snow.Refresh();
                }
            }
            else if (IsOutsideCanvas(snow))
            {
                snow.Refresh();
            }
        }

        private bool IsOutsideCanvas(SnowflakeClass snow)
        {
            return snow.Y > Height + snow.Size;
        }

        private bool IsOnTongue(SnowflakeClass snow)
        {
            return snow.Y > Tongue.Y + Tongue.Height / 2 && snow.X > Tongue.X + Tongue.Margin && snow.X < Tongue.X + Tongue.Width - Tongue.Margin && snow.Y < Height - Tongue.Margin;
        }

        public override void KeyReleased()
        {
            Tongue.Speed = 0;
        }

        public override void KeyPressed()
        {
            if (KeyCode == KC_LEFT)
            {
                Tongue.Speed = -5;
            }
            else if (KeyCode == KC_RIGHT)
            {
                Tongue.Speed = 5;
            }
        }

        private bool IsGameover()
        {
            return Counter == 0 || Timer < 0 || TouchedRed;
        }

    }
}