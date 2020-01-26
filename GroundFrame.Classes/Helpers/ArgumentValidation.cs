using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Xml.Linq;
using GroundFrame.Classes.SimSig;
using Newtonsoft.Json.Linq;

namespace GroundFrame.Classes
{
    internal static class ArgumentValidation
    {
        /// <summary>
        /// Validates the Half Minute Character as a string argument
        /// </summary>
        /// <param name="HalfMinuteCharacter"></param>
        internal static void ValidateHalfMinute(string HalfMinuteCharacter, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (string.IsNullOrEmpty(HalfMinuteCharacter))
            {

                throw new ArgumentOutOfRangeException(ExceptionMessageResources.GetString("InvalidHalfMinuteCharacterArgument", Culture));
            }

            //Check the HalfMinuteCharacter argument is only 1 character
            if (HalfMinuteCharacter.Length != 1)
            {
                throw new ArgumentOutOfRangeException(ExceptionMessageResources.GetString("InvalidHalfMinuteCharacterLengthArgument", Culture));
            }
        }

        /// <summary>
        /// Validates a WTT Start Date argument
        /// </summary>
        /// <param name="WTTStartDate"></param>
        internal static void ValidateWTTStartDate(DateTime WTTStartDate, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (WTTStartDate < new DateTime(1850,1,1))
            {
                throw new ArgumentOutOfRangeException(ExceptionMessageResources.GetString("InvalidWTTStartDateArgument", Culture));
            }
        }

        /// <summary>
        /// Validates a JSON string
        /// </summary>
        /// <param name="JSON">The JSON string to validate</param>
        internal static void ValidateJSON(string JSON, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            try
            {
                JToken.Parse(JSON);
            }
            catch
            {
                throw new FormatException(ExceptionMessageResources.GetString("InvalidJSONError", Culture));
            }
        }

        /// <summary>
        /// Validates a UserSettingCollection argument
        /// </summary>
        /// <param name="UserSettings"></param>
        internal static void ValidateUserSettings(UserSettingCollection UserSettings)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (UserSettings == null)
            {

                throw new ArgumentNullException(ExceptionMessageResources.GetString("InvalidHalfMinuteCharacterArgument", new CultureInfo("en-GB")));
            }
        }

        /// <summary>
        /// Validates a Perentage to ensure it's between 0 and 100
        /// </summary>
        /// <param name="Percentage">The percentage to validate</param>
        /// <param name="Culture">The culture in which any error message should be returned</param>
        internal static void ValidatePercentage(int Percentage, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (Percentage < 0 || Percentage > 100)
            {

                throw new ArgumentOutOfRangeException(ExceptionMessageResources.GetString("InvalidPercentageArgument", Culture));
            }
        }

        /// <summary>
        /// Validates a FileName to ensure the file exists
        /// </summary>
        /// <param name="FileName">The path to the file to validate</param>
        /// <param name="Culture">The culture in which any error message should be returned</param>
        internal static void ValidateFilename(FileInfo FileName, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());
            
            if (FileName == null)
            {

                throw new ArgumentNullException(ExceptionMessageResources.GetString("InvalidFileNameNullArgument", Culture));
            }

            if (File.Exists(FileName.FullName) == false)
            {
                throw new FileNotFoundException(ExceptionMessageResources.GetString("FileNotFoundArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the Half Minute Character as a ASCII character argument
        /// </summary>
        internal static void ValidateHalfMinute(int ASCII, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            //Check the HalfMinuteCharacterASCII argument is within the valid range
            if (ASCII < 32 || ASCII > 255)
            {
                throw new ArgumentOutOfRangeException(ExceptionMessageResources.GetString("InvalidHalfMinuteCharacterASCIIArgument", Culture));
            }

        }

        /// <summary>
        /// Valudates the seconds argument
        /// </summary>
        /// <param name="Seconds"></param>
        internal static void ValidateSeconds(int Seconds, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            //Check the Seconds argument is within the valid range
            if (Seconds < 0)
            {
                throw new ArgumentOutOfRangeException(ExceptionMessageResources.GetString("InvalidSecondsArgument", Culture));
            }
        }

        /// <summary>
        /// Valudates the delimiter argument
        /// </summary>
        /// <param name="Seconds"></param>
        internal static void ValidateDelimiter(string Delimiter, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());


            if (Delimiter == null)
            {
                throw new ArgumentNullException(ExceptionMessageResources.GetString("InvalidTimeDelimiterArgument", Culture));
            }

            if (Delimiter.Length != 1)
            {
                throw new ArgumentOutOfRangeException(ExceptionMessageResources.GetString("InvalidTimeDelimiterLengthArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the SqlDataReader argument
        /// </summary>
        /// <param name="DataReader">The SqlDataReader object to validate</param>
        internal static void ValidateSqlDataReader(SqlDataReader DataReader, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

                        if (DataReader == null)
            {
                throw new ArgumentNullException(ExceptionMessageResources.GetString("InvalidDataReaderArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the SqlDataReader argument
        /// </summary>
        /// <param name="DataReader">The SqlDataReader object to validate</param>
        internal static void ValidateSQLConnector(GFSqlConnector SQLConnector, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (SQLConnector == null)
            {
                throw new ArgumentException(ExceptionMessageResources.GetString("InvalidSQLConnectorArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the SqlDataReader argument
        /// </summary>
        /// <param name="DataReader">The SqlDataReader object to validate</param>
        internal static void ValidateName(string Name, CultureInfo Culture, int MaxLength = 0)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentException(ExceptionMessageResources.GetString("InvalidNameArgument", Culture));
            }

            if (MaxLength > 0 && Name.Length> MaxLength)
            {
                throw new ArgumentException(string.Format(Culture, ExceptionMessageResources.GetString("InvalidNameLengthArgument", Culture), MaxLength));
            }
        }

        /// <summary>
        /// Validates the SimSig Code argument
        /// </summary>
        /// <param name="SimSigCode">The SimSig Code to validate</param>
        internal static void ValidateSimSigCode(string SimSigCode, CultureInfo Culture, int MaxLength = 0)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (string.IsNullOrEmpty(SimSigCode))
            {
                throw new ArgumentException(ExceptionMessageResources.GetString("InvalidSimSigCodeArgument", Culture));
            }

            if (MaxLength > 0 && SimSigCode.Length > MaxLength)
            {
                throw new ArgumentException(string.Format(Culture, ExceptionMessageResources.GetString("InvalidSimSigCodeLengthArgument", Culture), MaxLength));
            }      
        }

        /// <summary>
        /// Validates the Meters argument
        /// </summary>
        /// <param name="Meters">The SimSig Code to validate</param>
        internal static void ValidateMeters(int Meters, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (Meters<=0)
            {
                throw new ArgumentException(ExceptionMessageResources.GetString("InvalidMeterArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the XML argument
        /// </summary>
        /// <param name="XML">The XML to validate</param>
        internal static void ValidateXElement(XElement XML, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (XML == null)
            {
                throw new ArgumentException(ExceptionMessageResources.GetString("InvalidXMLArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the Simulation argument
        /// </summary>
        /// <param name="XML">The Simulation to validate</param>
        internal static void ValidateSimulation(Simulation Simulation, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Classes.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (Simulation == null)
            {
                throw new ArgumentException(ExceptionMessageResources.GetString("InvalidSimulationArgument", Culture));
            }
        }
    }
}
