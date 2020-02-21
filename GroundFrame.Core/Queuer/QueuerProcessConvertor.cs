using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Core.Queuer
{
    /// <summary>
    /// A surrogate class which mimics the QueuerProcess class and is used as part of the custom JsonConverter for a QueuerProcess object
    /// </summary>
    internal class QueuerProcessSurrogate
    {
        #region Constants
        #endregion Contants

        #region Private Variables
        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets or ses the object Id
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Needed in order to serialize / deserialize correctly from MongoDB / BSON")]
        public string _id { get; set; }

        /// <summary>
        /// Gets or setsthe key of the process
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the Application API Key used to initiate the process
        /// </summary>
        public string AppAPIKey { get; set; }

        /// <summary>
        /// Gets or sets the Application User API Key used to initiate the process
        /// </summary>
        public string AppUserAPIKey { get; set; }

        /// <summary>
        /// Gets or sets the time the process was queued
        /// </summary>
        public DateTimeOffset QueueTime { get; set; }

        /// <summary>
        /// Gets or sets the flag to indicate whether the process should be executed straight away
        /// </summary>
        public bool ExecuteNow { get; set; }

        /// <summary>
        /// Gets or sets the task type full name
        /// </summary>
        public string TaskType { get; set; }

        /// <summary>
        /// Gets or sets the process requests
        /// </summary>
        public SurrogateRequest Request { get; set; }

        /// <summary>
        /// Gets or sets the flag to indicate whether the user was authenticated when the process was queued
        /// </summary>
        public bool Authenticated { get; set; }

        #endregion Properties

        #region Methods
        #endregion Methods

        #region Classes

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:Avoid Uninstantiated Internal Classes", Justification = "Used by Newtonsoft.JSON when deserializing")]
        internal class SurrogateRequest
        {
            public ExtendedList<QueuerResponse> Responses { get; set; }

            public JObject Config { get; set; }
        }

        #endregion Classes
    }

    /// <summary>
    /// JsonConverter class for a QueuerProcess object
    /// </summary>
    internal class QueuerProcessConverter : JsonConverter
    {
        #region Constants
        #endregion Contants

        #region Private Variables

        private readonly string _Environment; //Private variable to store the environement

        #endregion Private Variables

        #region Properties
        #endregion Properties

        #region Constructors

        internal QueuerProcessConverter(string Environment)
        {
            this._Environment = Environment;
        }

        #endregion Conrstructors

        #region Methods

        /// <summary>
        /// Flag to indicate whether the class a QueuerProcess
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(QueuerProcess);
        }

        /// <summary>
        /// Override method to deserialize a JSON string into a QueuerProcess
        /// </summary>
        /// <returns></returns>
        public override object ReadJson(JsonReader Reader, Type ObjectType, object ExistingValue, JsonSerializer Serializer)
        {
            //Validate Arguments
            //if (Serializer == null)
            //{
            //    throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new string[] { "Serializer" }, new System.Globalization.CultureInfo("en-GB")));
            //}

            //if (Reader == null)
            //{
            //    throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new string[] { "Reader" }, new System.Globalization.CultureInfo("en-GB")));
            //}

            //Deserialize reader into surrogate object
            QueuerProcessSurrogate SurrogateQueuerProcess = Serializer.Deserialize<QueuerProcessSurrogate>(Reader);
            return new QueuerProcess(SurrogateQueuerProcess, this._Environment);
        }

        /// <summary>
        /// Override method to serialize a QueuerProcess object to a JSON string
        /// </summary>
        /// <returns></returns>
        public override void WriteJson(JsonWriter Writer, object Value, JsonSerializer Serializer)
        {
            //Validate Arguments
            //if (Serializer == null)
            //{
            //    throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new string[] { "Serializer" }, new System.Globalization.CultureInfo("en-GB")));
            //}

            //if (Writer == null)
            //{
            //    throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new string[] { "Writer" }, new System.Globalization.CultureInfo("en-GB")));
            //}

            //if (Value == null)
            //{
            //    throw new ArgumentNullException(ExceptionHelper.GetStaticException("GeneralNullArgument", new string[] { "Value" }, new System.Globalization.CultureInfo("en-GB")));
            //}

            QueuerProcess queuerProcess = (QueuerProcess)Value;
            // create the surrogate and serialize it instead 
            // of the collection itself
            Serializer.Serialize(Writer, queuerProcess.ToQueuerProcessSurrogate());
        }

        #endregion Methods
    }
}
