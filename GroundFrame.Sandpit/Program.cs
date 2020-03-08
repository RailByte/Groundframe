using GroundFrame.Core.Queuer;
using System;
using System.IO;

namespace GroundFrame.Sandpit
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");
            QueuerProcess Test = null;

            Console.WriteLine(string.Compare("EBALDCK", "BALDOCK"));

            //GroundFrame.Core.GFSqlConnector SQL = GroundFrame.Core.Globals.GetGFSqlConnector("testappAPIKEY", "testadminuserAPIKEY", "localhost");

            //GroundFrame.Core.SimSig.Version Version = new Core.SimSig.Version("Version 4.15", "The current default version", 4.15M , SQL);
            //Version.SaveToSQLDB();

            try
            {
                string ConfigJSON = File.ReadAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\GF.json");

                Test = new QueuerProcess("testuserAPIKEY", "testappAPIKEY", "", "localhost", ConfigJSON, true);
                string JSON = Test.ToJSON();


                //QueuerProcess Test1 = new QueuerProcess("6ea2a14fdadf4b0589612f4f8cf90d82", "localhost");
                //Test1.ExecuteProcess();

                QueuerProcess Test1 = new QueuerProcess(Test.Key, "localhost");

                while (Test1.Status != QueuerResponseStatus.Success && Test1.Status != QueuerResponseStatus.CompletedWithWarning)
                {
                    System.Threading.Thread.Sleep(1000);
                }

                Console.WriteLine($"Finished!! - {Test1.Response.ResponseMessage}");
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
