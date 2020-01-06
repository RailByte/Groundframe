using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GroundFrame.Classes
{

    public class SimulationCollection : IEnumerable<Simulation>
    {
        #region Constants
        #endregion Constants

        #region Private Variables
        private List<Simulation> _Simulations = new List<Simulation>(); //List to store all Simulations
        private GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        #endregion Private Variables

        #region Properties
        public IEnumerator<Simulation> GetEnumerator() { return this._Simulations.GetEnumerator(); }

        #endregion Properties

        #region Constructors
        /// <summary>
        /// Instantiates a SimulationCollection object the supplied GroundFrame.SQL Connection
        /// </summary>
        /// <param name="SQLConnector"></param>
        public SimulationCollection(GFSqlConnector SQLConnector)
        {
            //Set the SQL Connector
            this._SQLConnector = SQLConnector;
            //Get the simulations
            this.GetAllSimulationsFromSQLDB();
        }

        /// <summary>
        /// Gets the total number of simulations in the collection
        /// </summary>
        public int Count { get { return this._Simulations.Count; } }

        #endregion Constructors

        #region Methods
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the Simulation for the supplied Index
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public Simulation IndexOf(int Index)
        {
            return this._Simulations[Index];
        }

        private void GetAllSimulationsFromSQLDB()
        {
            this._Simulations = new List<Simulation>();

            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_GET_TSIM", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@id", 0));
                SqlDataReader DataReader = Cmd.ExecuteReader();

                //Read the records
                while (DataReader.Read())
                {
                    if (DataReader.HasRows == false)
                    {
                        //If no record around then 
                        throw new NoRowsException($"No Simulation Records Found");
                    }
                    else
                    {
                        //Parse the DataReader into the object
                        this._Simulations.Add(new Simulation(DataReader, this._SQLConnector));
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to retrieve all Simuation records from the GroundFrame.SQL database.", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        #endregion Methods
    }
}
