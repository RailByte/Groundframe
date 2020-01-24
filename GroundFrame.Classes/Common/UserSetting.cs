using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes
{
    /// <summary>
    /// Class which represents a user setting
    /// </summary>
    public class UserSetting
    {
        #region Constants
        #endregion Constatns

        #region Private Variables

        private string _Key; //Stores the key of the setting
        private string _Description; //Stores the description of the setting
        private Type _DataType; //Stores the data type of the setting value
        private readonly object _DefaultValue; //Stores the default value

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Gets the key of the user setting
        /// </summary>
        public string Key { get { return this._Key; } }
        /// <summary>
        /// Gets the description of the user setting
        /// </summary>
        public string Description { get { return this._Description; } }
        /// <summary>
        /// Gets the value of the user setting
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// Gets the setting default value
        /// </summary>
        public object DefaultValue { get { return this._DefaultValue; } }
        /// <summary>
        /// Gets the data type of the user setting
        /// </summary>
        public Type DataType { get { return this._DataType; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Instantiates a user setting from the supplied arguments
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Description"></param>
        /// <param name="Value"></param>
        /// <param name="DefaultValue"></param>
        /// <param name="DataTypeName"></param>
        public UserSetting (string Key, string Description, string Value, string DataTypeName, string DefaultValue)
        {
            this._Key = Key;
            this._Description = Description;
            this._DataType = Type.GetType(DataTypeName);

            if (DataTypeName == "system.string")
            {
                this.Value = Value;
            }
            else
            {
                this.Value = string.IsNullOrEmpty(Value) ? null : Value;
            }

            if (DataTypeName == "system.string")
            {
                this._DefaultValue = DefaultValue;
            }
            else
            {
                this._DefaultValue = string.IsNullOrEmpty(DefaultValue) ? null : DefaultValue;
            }
        }

        #endregion Constructors
    }
}
