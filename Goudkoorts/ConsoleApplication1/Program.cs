using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goudkoorts.Controller;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.simpleVersion();
            Console.WriteLine(controller.show());
            Console.ReadLine();
        }
    }
}
