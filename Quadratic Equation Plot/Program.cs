using Processing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

//TODO: input refactor  _/
//TODO: zérushelyek kirajzolása  _/
//TODO: tick-ek és rács rajzolása  _/
//TODO: skálázás állítása külön + és - gombokkal  _/

//TODO: grafikus integrálszámítás
//TODO: polinomok kirajzolása  _/
//TODO: polinomok analízise: x0, min/max -> numerikusv v. diszkrét megoldás?

//TODO: kör kirajzolása Circle hívás nélkül
//TODO: kocka kirajzolása izometrikusan
//TODO: kocka forgatása Y tengely körül

namespace Quadratic_Equation_Plot
{

    public class Program : SmartizSketch
    {
        [STAThread]
        static void Main()
        {
            new Program().Start();
        }

        //readonly List<double> Polinom = new List<double>() { 1 / 2 + 6 / 7d, -1 / 14d, -13 / 14d, 1 / 14d, 1 / 14d };

        readonly List<double> _polinom = new List<double>() { 0, 0, 1 };
        double scale = 50;
        int TposX = 20;
        int TposY = 40;
        int RposX;
        const int ButtonWidth = 20;
        const int ButtonHeight = 10;
        Button Button1;
        Button Button2;
        Button Button3;
        Button Button4;
        Button Button5;
        Button Button6;
        Button ButtonPlus;
        Button ButtonMinus;
        //RposX = TposX + 50; //Variable assignment := statement

        public override void Setup()
        {
            Size(800, 600);
            Background(0);
            RposX = TposX + 60;
            Button1 = new Button(RposX, TposY - 15, ButtonWidth, ButtonHeight, Direction.Up); //20 := expression
            Button2 = new Button(RposX, TposY - 15 + ButtonHeight, ButtonWidth, ButtonHeight, Direction.Down);
            Button3 = new Button(RposX, TposY + 25, ButtonWidth, ButtonHeight, Direction.Up);
            Button4 = new Button(RposX, TposY + 25 + ButtonHeight, ButtonWidth, ButtonHeight, Direction.Down);
            Button5 = new Button(RposX, TposY + 65, ButtonWidth, ButtonHeight, Direction.Up);
            Button6 = new Button(RposX, TposY + 65 + ButtonHeight, ButtonWidth, ButtonHeight, Direction.Down);
            ButtonPlus = new Button(Width - 60, TposY - 15, ButtonWidth + 10, ButtonHeight + 10, Direction.Up, Symbol.Sign);
            ButtonMinus = new Button(Width - 60, TposY - 15 + ButtonHeight + 10, ButtonWidth + 10, ButtonHeight + 10, Direction.Down, Symbol.Sign);
        }

        public override void DrawFrame()
        {
            Background(0);
            DrawCoordinateSystem();
            DrawDashboard();
            DrawPolinom(Polinom.Diff(_polinom));
            //DrawRoots(a, b, c);
        }

        public void DrawCoordinateSystem()
        {
            Stroke(255);
            StrokeWeight(1);
            Line(Width / 2, 0, Width / 2, Height);
            Line(0, Height / 2, Width, Height / 2);

            for (double i = 0; i < Width; i += 10)
            {
                Stroke(113, 114, 120, 80);
                Line(0, i, Width, i);
                Stroke(255);
                Line(Width / 2 - 2, i, Width / 2 + 2, i);
                Stroke(113, 114, 120, 80);
                Line(i, 0, i, Height);
                Stroke(255);
                Line(i, Height / 2 - 2, i, Height / 2 + 2);
            }

            /*
            double stepX = Width / (scale*10);
            double stepY = Height / (scale*10);

            for (double i = 0; i < Height; i += stepY)
            {
                Stroke(113, 114, 120, 150);
                Line(0, i, Width, i);
                Stroke(255);
                Line(Width / 2 - 2, i, Width / 2 + 2, i);
            }

            for (double i = 0; i < Width; i += stepX)
            {
                Stroke(113, 114, 120, 150);
                Line(i, 0, i, Height);
                Stroke(255);
                Line(i, Height / 2 - 2, i, Height / 2 + 2);
            }
            */
        }

        private void DrawDashboard()
        {
            Fill(255);
            TextSize(15);

            //Text("f(x) = " + a + "x^2 + " + b + "x + " + c, TposX, TposY-20);

            Text("A = " + Math.Round(_polinom[0]), TposX, TposY);
            DrawButton(Button1);
            DrawButton(Button2);

            Text("B = " + Math.Round(_polinom[1]), TposX, TposY + 40);
            DrawButton(Button3);
            DrawButton(Button4);

            Text("C = " + Math.Round(_polinom[2]), TposX, TposY + 80);
            DrawButton(Button5);
            DrawButton(Button6);

            Text("Scale: " + Math.Round(scale), Width - 150, TposY);
            DrawButton(ButtonPlus);
            DrawButton(ButtonMinus);
        }

        public void DrawButton(Button b)
        {
            int s = 4;
            NoFill();
            Stroke(255);
            Rect(b.Bounds.Left, b.Bounds.Top, b.Bounds.Width, b.Bounds.Height);
            Fill(255);
            if (b.Symbol == Symbol.Arrow && b.Direction == Direction.Up)
            {
                Triangle(b.Bounds.Left + s, b.Bounds.Top + b.Bounds.Height - s, b.Bounds.Left + b.Bounds.Width / 2, b.Bounds.Top + s, b.Bounds.Left + b.Bounds.Width - s, b.Bounds.Top + b.Bounds.Height - s);
            }
            else if (b.Symbol == Symbol.Arrow && b.Direction == Direction.Down)
            {
                Triangle(b.Bounds.Left + s, b.Bounds.Top + s, b.Bounds.Left + b.Bounds.Width - s, b.Bounds.Top + s, b.Bounds.Left + b.Bounds.Width / 2, b.Bounds.Top + b.Bounds.Height - s);
            }
            else if (b.Symbol == Symbol.Sign && b.Direction == Direction.Up)
            {
                TextSize(30);
                Text("+", b.Bounds.Left + s + 3, b.Bounds.Top + b.Bounds.Height - s + 3);
            }
            else
            {
                TextSize(30);
                Text("–", b.Bounds.Left + s + 3, b.Bounds.Top + b.Bounds.Height - s + 3);
            }

        }

        public bool IsOn(Button b)
        {
            return PMouseX >= b.Bounds.Left && PMouseX <= b.Bounds.Left + b.Bounds.Width && PMouseY >= b.Bounds.Top && PMouseY <= b.Bounds.Top + b.Bounds.Height;
        }

        public override void MouseClicked()
        {
            if (IsOn(Button1))
            {
                _polinom[0] = Math.Round(_polinom[0] + 1, 1);
            }
            else if (IsOn(Button2))
            {
                _polinom[0] = Math.Round(_polinom[0] - 1, 1);
            }
            else if (IsOn(Button3))
            {
                _polinom[1] = Math.Round(_polinom[1] + 1, 1);
            }
            else if (IsOn(Button4))
            {
                _polinom[1] = Math.Round(_polinom[1] - 1, 1);
            }
            else if (IsOn(Button5))
            {
                _polinom[2] = Math.Round(_polinom[2] + 1, 1);
            }
            else if (IsOn(Button6))
            {
                _polinom[2] = Math.Round(_polinom[2] - 1, 1);
            }

            if (IsOn(ButtonPlus))
            {
                scale = scale *= 1.1;
            }
            else if (IsOn(ButtonMinus))
            {
                scale = scale /= 1.1;
            }
        }

        public void DrawPolinom(List<double> polinom)
        {
            double previousX = -10;
            double previousY = -10;

            for (var x = -1; x < Width; x++)
            {
                var y = Polinom.Value(polinom, (x - Width / 2) / scale);
                double finalY = -y * scale + Height / 2;
                if (finalY < Height + 100 && finalY >= -100)
                {
                    Stroke(80, 133, 188);
                    StrokeWeight(2);
                    Line(x, finalY, previousX, previousY);
                }
                previousX = x;
                previousY = finalY;
            }
        }



        //public void DrawRoots(List<double> polinom)
        //{
        //    var rX = Root(polinom, x);

        //    var px = rX + Width / 2;
        //    Stroke(200, 0, 0);
        //    StrokeWeight(10);
        //    TextSize(15);
        //    Point(px, Height / 2);
        //}

        

        //private object Root(List<double> polinom, double x)
        //{
        //    var y =
        //}
    }

    public static class Polinom
    {
        public static double[] Extremes(List<double> polinom)
        {
            var extremes = new List<double>(polinom.Count);
            var roots = Roots(Diff(polinom));
            for (int i = 0; i < roots.Length; i++)
            {
                var value1 = Value(polinom, roots[i] + 5);
                var value2 = Value(polinom, roots[i] - 5);
                if (Math.Sign(value1) != Math.Sign(value2))
                {
                    extremes.Add(roots[i]);
                }
            }
            return extremes.ToArray();
        }

        public static double[] Roots(List<double> list)
        {
            throw new NotImplementedException();
        }

        public static List<double> Diff(List<double> polinom)
        {
            var dPolinom = new List<double>();
            for (int i = 1; i < polinom.Count; i++)
            {
                dPolinom.Add(polinom[i] * i);
            }
            //Debug.WriteLine(string.Join(", ", dPolinom));
            return dPolinom;
        }

        public static double Value(List<double> polinom, double x)
        {
            var sum = 0d;
            for (int a = 0; a < polinom.Count; a++)
            {
                sum += polinom[a] * Math.Pow(x, a);
            }
            return sum;

            /* SQL-esen :)
            public double P(double x, List<double> c)
                => Enumerable.Range(0, c.Count)
                .Select(i => c[i] * Math.Pow(x, i))
                .Sum();
            */
        }
    }
}