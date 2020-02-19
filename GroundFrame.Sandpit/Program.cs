using GroundFrame.Core.Queuer;
using System;
using System.IO;

namespace GroundFrame.Sandpit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            try
            {
                //string ConfigJSON = File.ReadAllText(@"C:\Users\tcaceres\Desktop\GF.json");

                //QueuerProcess Test = new QueuerProcess("testappAPIKEY", "testuserAPIKEY", "", "localhost", ConfigJSON, false);
                //string JSON = Test.ToJSON();

                QueuerProcess Test1 = new QueuerProcess("6ea2a14fdadf4b0589612f4f8cf90d82", "localhost");
                //Test1.SaveToDB();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }

            Console.WriteLine("Finished - Press any key");
            Console.ReadKey(); 
        }
    }
}
