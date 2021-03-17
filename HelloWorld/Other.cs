using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Other
    {
        public static string Today()
        {
            return DateTime.Now.DayOfWeek.ToString();
        }
    }
}
