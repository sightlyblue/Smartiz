using Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snow_Catcher
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
            tongue = LoadImage("tongue.png");

            Size(600, 600);
            Background(21, 34, 56);


        }
    }
}
