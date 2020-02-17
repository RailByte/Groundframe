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

            //string ConfigJSON = File.ReadAllText(@"C:\Users\tcaceres\Desktop\GF.json");

            //QueuerProcess Test = new QueuerProcess("testappAPIKEY", "testuserAPIKEY", "", "localhost", ConfigJSON, false);
            //string JSON = Test.ToJSON();

            QueuerProcess Test = new QueuerProcess("e9c71071db12491bbd6db1bc5d033b3d", "localhost");

            Console.ReadKey(); 
        }
    }
}
