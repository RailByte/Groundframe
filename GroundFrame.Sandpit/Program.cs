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
            
            try
            {
                //string ConfigJSON = File.ReadAllText(@"C:\Users\tcaceres\Desktop\GF.json");

                //QueuerProcess Test = new QueuerProcess("testappAPIKEY", "testuserAPIKEY", "", "localhost", ConfigJSON, false);
                //string JSON = Test.ToJSON();

                QueuerProcess Test1 = new QueuerProcess("0938cf1d32214a2f876676c265432c61", "localhost");
                Test1.SaveToDB();
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
