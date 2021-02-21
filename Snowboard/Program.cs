using Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snowboard
{
    class Program : SmartizSketch
    {
        [STAThread]
        static void Main()
        {
            new Program().Start();
        }

        private Color _backgroundColor = new Color(0, 0, 150);

        private List<DrawableObject> _objects = new List<DrawableObject>();

        public override void Setup()
        {


            Size(800, 600);
            Background(_backgroundColor);
            for (var i = 0; i < 3; ++i)
            {
                _objects.Add(new Tree { X = 150 * (i + 1) + 50, Y = 150 });
            }
            _objects.Add(new Cloud { X = 50, Y = 50 });
        }

        public override void DrawFrame()
        {
            foreach (var o in _objects)
            {
                o.Draw();
            }
        }



    }







    class Teszt
    {
        static void Haho()
        {

            var gina = new Human(Gender.Female);
            var gg = gina.Gender;

            var viktor = new Human(Gender.Male);
            var vg = viktor.GetBmi();
        }
    }


    public enum Gender { Male, Female }

    public class God : Human
    {
        public static God _theOneAndOnly = new God();

        public static God Create()
        {
            return _theOneAndOnly;
        }

        private God() : base(Gender.Male)
        {

        }
    }

    public class Human
    {
        public int Weight { get; private set; }

        public int Height { get; private set; }

        public Gender Gender { get; }

        private Human()
        {
            this.Weight = 4;
            this.Height = 20;
        }

        public Human(Gender gender) : this()
        {
            this.Gender = gender;
        }

        public void Feed(int foodAmount)
        {
            this.Weight += foodAmount / 2;
        }

        public int GetBmi()
        {
            return (int)(Weight / Math.Pow(Height, 2));
        }
    }
}
