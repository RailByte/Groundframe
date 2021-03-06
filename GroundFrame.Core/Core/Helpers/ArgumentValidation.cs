﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Xml.Linq;
using GroundFrame.Core.SimSig;
using Newtonsoft.Json.Linq;

namespace GroundFrame.Core
{
    internal static class ArgumentValidation
    {
        /// <summary>
        /// Validates the a version number
        /// </summary>
        /// <param name="VersionNumber">The version number to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateVersionNumber(decimal VersionNumber, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (VersionNumber<0)
            {

                throw new ArgumentOutOfRangeException(ExceptionMessageResources.GetString("InvalidVersionNumberArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the Location Node argument
        /// </summary>
        /// <param name="LocationNode">The Location Node to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateLocationNode(LocationNode LocationNode, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (LocationNode == null)
            {

                throw new ArgumentNullException(ExceptionMessageResources.GetString("InvalidLocationNodeArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the Simulation Era argument
        /// </summary>
        /// <param name="SimEra">The Simualation Era object to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateSimEra(SimulationEra SimEra, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (SimEra == null)
            {
                throw new ArgumentNullException(ExceptionMessageResources.GetString("InvalidSimEraArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the Location argument
        /// </summary>
        /// <param name="Location">The Location object to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateLocation(Location Location, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (Location == null)
            {
                throw new ArgumentNullException(ExceptionMessageResources.GetString("InvalidLocationArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the Path Edge argument
        /// </summary>
        /// <param name="PathEdge">The Path Edge object to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidatePathEdge(PathEdge PathEdge, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (PathEdge == null)
            {

                throw new ArgumentNullException(ExceptionMessageResources.GetString("InvalidPathEdgeArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the Half Minute Character as a string argument
        /// </summary>
        /// <param name="HalfMinuteCharacter">The half minute character to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateHalfMinute(string HalfMinuteCharacter, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

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
        /// <param name="WTTStartDate">The WTT Start Date to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateWTTStartDate(DateTime WTTStartDate, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (WTTStartDate < new DateTime(1850,1,1))
            {
                throw new ArgumentOutOfRangeException(ExceptionMessageResources.GetString("InvalidWTTStartDateArgument", Culture));
            }
        }

        /// <summary>
        /// Validates a JSON string
        /// </summary>
        /// <param name="JSON">The JSON string to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateJSON(string JSON, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (string.IsNullOrEmpty(JSON))
            {
                throw new ArgumentNullException(ExceptionMessageResources.GetString("NullOrEmptyJSONError", Culture));
            }

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
        /// <param name="UserSettings">The UserSettingCollection to validate</param>
        internal static void ValidateUserSettings(UserSettingCollection UserSettings)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (UserSettings == null)
            {

                throw new ArgumentNullException(ExceptionMessageResources.GetString("InvalidUserSettingsArgument", new CultureInfo("en-GB")));
            }
        }

        /// <summary>
        /// Validates a Perentage to ensure it's between 0 and 100
        /// </summary>
        /// <param name="Percentage">The percentage to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidatePercentage(int Percentage, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (Percentage < 0 || Percentage > 100)
            {

                throw new ArgumentOutOfRangeException(ExceptionMessageResources.GetString("InvalidPercentageArgument", Culture));
            }
        }

        /// <summary>
        /// Validates a FileName to ensure the file exists
        /// </summary>
        /// <param name="FileName">The path to the file to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateFilename(FileInfo FileName, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());
            
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
        /// <param name="ASCII">The ASCII character to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateHalfMinute(int ASCII, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            //Check the HalfMinuteCharacterASCII argument is within the valid range
            if (ASCII < 32 || ASCII > 255)
            {
                throw new ArgumentOutOfRangeException(ExceptionMessageResources.GetString("InvalidHalfMinuteCharacterASCIIArgument", Culture));
            }

        }

        /// <summary>
        /// Valudates the seconds argument
        /// </summary>
        /// <param name="Seconds">The number of seconds to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateSeconds(int Seconds, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            //Check the Seconds argument is within the valid range
            if (Seconds < 0)
            {
                throw new ArgumentOutOfRangeException(ExceptionMessageResources.GetString("InvalidSecondsArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the delimiter argument
        /// </summary>
        /// <param name="Delimiter">The time delimiter to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateDelimiter(string Delimiter, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());


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
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateSqlDataReader(SqlDataReader DataReader, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

                        if (DataReader == null)
            {
                throw new ArgumentNullException(ExceptionMessageResources.GetString("InvalidDataReaderArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the SqlDataReader argument
        /// </summary>
        /// <param name="SQLConnector">The GFSqlConnector object to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateSQLConnector(GFSqlConnector SQLConnector, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (SQLConnector == null)
            {
                throw new ArgumentException(ExceptionMessageResources.GetString("InvalidSQLConnectorArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the Name argument
        /// </summary>
        /// <param name="Name">The name string to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        /// <param name="MaxLength">The allowable max length of the name. Default is zero. If zero then there is no limiation on length</param>      
        internal static void ValidateName(string Name, CultureInfo Culture, int MaxLength = 0)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

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
        /// <param name="SimSigCode">The SimSig code to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        /// <param name="MaxLength">The allowable max length of the SimSig code. Default is zero. If zero then there is no limiation on length</param>  
        internal static void ValidateSimSigCode(string SimSigCode, CultureInfo Culture, int MaxLength = 0)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

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
        /// <param name="Meters">The number of meters to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateMeters(int Meters, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (Meters<=0)
            {
                throw new ArgumentException(ExceptionMessageResources.GetString("InvalidMeterArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the XML (XElement) argument
        /// </summary>
        /// <param name="XML">The XML (XElemnt) to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateXElement(XElement XML, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (XML == null)
            {
                throw new ArgumentNullException(ExceptionMessageResources.GetString("InvalidXMLArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the Simulation argument
        /// </summary>
        /// <param name="Simulation">The Simulation to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateSimulation(Simulation Simulation, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (Simulation == null)
            {
                throw new ArgumentException(ExceptionMessageResources.GetString("InvalidSimulationArgument", Culture));
            }
        }

        /// <summary>
        /// Validates the Version argument
        /// </summary>
        /// <param name="Version">The Version to validate</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        internal static void ValidateVersion(SimSig.Version Version, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (Version == null)
            {
                throw new ArgumentException(ExceptionMessageResources.GetString("InvalidSimulationArgument", Culture));
            }
        }


        /// <summary>
        /// Validates the Environment argument
        /// </summary>
        /// <param name="Environment">The environment to validation</param>
        /// <param name="Culture">The culture in which any exception messages should be thrown</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "Environment wall always by in en-GB")]
        internal static void ValidateEnvironment(string Environment, CultureInfo Culture)
        {
            ResourceManager ExceptionMessageResources = new ResourceManager("GroundFrame.Core.Resources.ExceptionResources", Assembly.GetExecutingAssembly());

            if (string.IsNullOrEmpty(Environment))
            {
                
            }

            //Check it's a valid environment
            List<string> ValidEnvironments = new List<string>() { "production", "localhost" };

            if (ValidEnvironments.Any(x => x.ToLower() == Environment.ToLower()) == false)
            {
                throw new ArgumentException(ExceptionMessageResources.GetString("InvalidEnvironmentArgument", Culture)); 
            }
        }
    }
}
