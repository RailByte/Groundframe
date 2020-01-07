using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using GroundFrame.Classes;

namespace GroundFrame.Sandpit
{
    class Program
    {
        static void Main()
        {
            try
            {
                Time Test = new Time(186030);

                Console.WriteLine(Test.FormattedShortTime);
                Console.WriteLine(Test.FormattedLongTime);
                Console.WriteLine(Test.DateAndTime.ToLongTimeString());
                Console.WriteLine(Test.DateAndTime.ToShortDateString());

                Test = new Time(186030, 189);

                Console.WriteLine(Test.FormattedShortTime);
                Console.WriteLine(Test.FormattedLongTime);
                Console.WriteLine(Test.DateAndTime.ToLongTimeString());
                Console.WriteLine(Test.DateAndTime.ToShortDateString());

                WTT TestWTT = new WTT(@"C:\Users\tcaceres\Downloads\Royston Weekday July 2018 Timetable.WTT");

                WTTLength TestLength = new WTTLength(1000);
                Console.WriteLine(TestLength.DecimalMiles);
                Console.WriteLine(TestLength.MilesAndChains);

                WTTTraction TestTraction = new WTTTraction();
                Console.WriteLine(TestTraction.SimSigID);

                GFSqlConnector SQL = new GFSqlConnector("dtestappAPIKEY", "testuserAPIKEY", @"(localdb)\MSSQLLocalDB", "GroundFrame.SQL");

                SimulationCollection SimColl = new SimulationCollection(SQL);

                SQL.Open();
                SqlCommand cmd = SQL.SQLCommand("SELECT test = CONVERT(INT,SESSION_CONTEXT(N'application'));", CommandType.Text);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}", reader.GetInt32(0));
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
                SQL.Close();

                Simulation Sim = new Simulation("Royston", "Royston Description", null, "royston", SQL);
                Sim.SaveToSQLDB();

                Console.WriteLine($"Simulation ID: {Sim.ID}");

            }
            catch (Exception Ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error has occured running GroundFrame.Sandpit:- {Ex.Message}");
            }
            
            Console.ReadKey();
        }
    }
}
