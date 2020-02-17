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

            //string ConfigJSON = File.ReadAllText(@"C:\Users\timca\Desktop\GF.json");

            //QueuerProcess Test = new QueuerProcess("testappAPIKEY", "testuserAPIKEY", "", "localhost", ConfigJSON, false);
            //string JSON = Test.ToJSON();

            QueuerProcess Test = new QueuerProcess("c2ed28f576f04075bbb0c016055163c0", "localhost");

            Console.ReadKey(); 
        }
    }
}
