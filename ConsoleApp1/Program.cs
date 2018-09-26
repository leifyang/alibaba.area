using ConsoleApp1.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***********START*************");
            Class1 class1 = new Class1();
            class1.m2();
            Console.WriteLine("~~~~~~~~~~~~~END~~~~~~~~~~~~~~~~");
            Console.ReadKey();
        }
    }
}
