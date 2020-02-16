using GroundFrame.Queuer;
using System;
using System.IO;

namespace GroundFrame.Sandpit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string JSON = File.ReadAllText(@"C:\Users\timca\Desktop\GF.json");

            QueuerProcess Test = new QueuerProcess("testappAPIKEY", "", "testuserAPIKEY", JSON);

            Console.ReadKey();
        }
    }
}
