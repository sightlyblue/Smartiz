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
    public struct Button
    {
        public Rectangle Bounds;

        public Direction Direction;

        public Symbol Symbol;


        public Button(int left, int top, int width, int height, Direction direction, Symbol symbol=Symbol.Arrow)
        {
            Bounds = new Rectangle(left, top, width, height);
            Direction = direction;
            Symbol = symbol;
        }
    }
}