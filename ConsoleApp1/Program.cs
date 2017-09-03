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

            

            while (true)
            {
                DateTime time = System.DateTime.Now;
                if (time < System.DateTime.Now)
                {
                   
                    Console.WriteLine(time);
                }
            }
                
            
        }
    }
}
