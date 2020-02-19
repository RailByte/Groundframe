using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace GroundFrame.Core.SimSig
{
    /// <summary>
    /// Class which represents a  SimSig simulation together with it's extension data of eras, and locations
    /// </summary>
    public class SimulationExtension : Simulation
    {
        #region Constants
        #endregion Constants

        #region Private Variables

        private readonly GFSqlConnector _SQLConnector; //Stores the Connector to the Microsoft SQL Database 
        private List<SimulationEra> _Eras; //Stores the Eras available in the Simulation
        private LocationCollection _Locations; //Stores the locations available in the Simulation

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the Eras available in the Simulation
        /// </summary>
        public List<SimulationEra> Eras { get { return this._Eras; } }

        /// <summary>
        /// Gets the Locations available in the Simulation
        /// </summary>
        public LocationCollection Locations { get { return this._Locations; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new Simulation Extension from the GroundFrame.SQL database
        /// </summary>
        /// <param name="ID">The ID of the GroundFrame.SQL database simulation record</param>
        /// <param name="SQLConnector">The GFSqlConnector to the GroundFrame.SQL database</param>
        public SimulationExtension(int ID, GFSqlConnector SQLConnector) : base(ID, SQLConnector)
        {
            //Validate Arguments
            ArgumentValidation.ValidateSQLConnector(SQLConnector, Globals.UserSettings.GetCultureInfo());

            this._SQLConnector = new GFSqlConnector(SQLConnector);
            //Load the data
            this.LoadSimErasFromSQLDB();
            this._Locations = new LocationCollection(this, SQLConnector);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Refreshes the simulation extension data from the GroundFrame.SQL database
        /// </summary>
        public new void RefreshFromSQLDB()
        {
            base.RefreshFromSQLDB();
            this.LoadSimErasFromSQLDB();
            this._Locations = new LocationCollection(this, this._SQLConnector);
        }

        //Loads the Simulation Eras from the GroundFrame.SQL database into the Era List.
        private void LoadSimErasFromSQLDB()
        {
            this._Eras = new List<SimulationEra>();

            try
            {
                //Open the Connection
                this._SQLConnector.Open();
                //Set Command
                using SqlCommand Cmd = this._SQLConnector.SQLCommand("simsig.Usp_GET_TSIMERA_BY_SIM", CommandType.StoredProcedure);
                //Add Parameters
                Cmd.Parameters.Add(new SqlParameter("@sim_id", base.ID));
                //Execute the Query
                SqlDataReader DataReader = Cmd.ExecuteReader();

                while (DataReader.Read())
                {
                    this._Eras.Add(new SimulationEra(DataReader));
                }
            }
            catch (Exception Ex)
            {
                throw new ApplicationException($"An error has occurred trying to read Simulation Eras for Sim ID {0} from the GroundFrame.SQL database.", Ex);
            }
            finally
            {
                this._SQLConnector.Close();
            }
        }

        #endregion Methods
    }
}
