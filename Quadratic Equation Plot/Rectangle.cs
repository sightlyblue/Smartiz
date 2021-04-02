//TODO: input refactor
//TODO: zérushelyek kirajzolása
//TODO: tick-ek és rács rajzolása
//TODO: skálázás állítása külön + és - gombokkal

//TODO: grafikus integrálszámítás
//TODO: polinomok kirajzolása
//TODO: pplinomok analízise: x0, min/max -> numerikusv v. diszkrét megoldás?

//TODO: kör kirajzolása Circle hívás nélkül
//TODO: kocka kirajzolása izometrikusan
//TODO: kocka forgatása Y tengely körül

namespace Quadratic_Equation_Plot
{
    public struct Rectangle
    {
        public int Left;
        public int Top;
        public int Width;
        public int Height;

        public Rectangle(int left, int top, int width, int height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }
    }
}