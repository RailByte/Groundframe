﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Xml.Linq;

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
