using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes
{
    public class GFMongoConnector : IDisposable
    {
        #region Private Variables

        private MongoClient _MongoClient; //Private variable to store the MongoClient
        private string _Server; //Private vraiable to store the Server (name or IP)
        private string _Port; //Private variable to store the Port

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the MongoClient
        /// </summary>
        public MongoClient MongoClient { get { return this._MongoClient; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a new GFMongoConnector object from the supplied server and port
        /// </summary>
        /// <param name="Server">The server name (or IP address) of the Mongo Server</param>
        /// <param name="Port">The port number</param>
        public GFMongoConnector (string Server, string Port)
        {
            this._Server = Server;
            this._Port = Port;
            this._MongoClient = new MongoClient(this.GetMongoURL());
        }

        #endregion Constructors

        #region Methods

        private MongoUrl GetMongoURL()
        {
            return new MongoUrl($"mongodb://{this._Server}:{this._Port}");
        }

        /// <summary>
        /// Protected override of the Dispose method
        /// </summary>
        /// <param name="disposing">A flag to indicate whether the object is already being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                //someone want the deterministic release of all resources
                //Let us release all the managed resources
            }
            else
            {
                // Do nothing, no one asked a dispose, the object went out of
                // scope and finalized is called so lets next round of GC 
                // release these resources
            }

            // Release the unmanaged resource in any case as they will not be 
            // released by GC
        }

        /// <summary>
        /// Disposes the GFMongoConnector object
        /// </summary>
        public void Dispose()
        {
            // If this function is being called the user wants to release the
            // resources. lets call the Dispose which will do this for us.
            Dispose(true);

            // Now since we have done the cleanup already there is nothing left
            // for the Finalizer to do. So lets tell the GC not to call it later.
            GC.SuppressFinalize(this);
        }

        #endregion Methods
    }
}
