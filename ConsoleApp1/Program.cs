using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            do
            {
                Console.WriteLine("Adja meg, hogy a tömb hány elemet tartalmazzon!");
                n = Convert.ToInt32(Console.ReadLine());
            } while (n < 1 || n > 10);

            int[] numbers = new int[n];
            long sum = 0;
            for (int i = 0; i < n; i++)
            {
                do
                {
                    Console.WriteLine("Adja meg a(z)" + (i + 1) + ". számot!");
                    numbers[i] = Convert.ToInt32(Console.ReadLine());
                } while (numbers[i] > Math.Pow(10, 10) || numbers[1] < 0);
                sum += numbers[i];
            }
            Console.WriteLine("A számok összege: " + sum);
            Console.ReadKey();
        }
    }
}
