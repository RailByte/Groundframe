using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GroundFrame.Classes
{
    /// <summary>
    /// Static class which provides extension methods for a SqlDataReader
    /// </summary>
    public static class SqlDataReaderExtensions
    {
        /// <summary>
        /// Gets a Nullable Decimal value from a SqlDataReader
        /// </summary>
        /// <param name="DataReader"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public static Decimal? GetNullableDecimal(this SqlDataReader DataReader, string FieldName)
        {
            //Validate Arguments
            ArgumentValidation.ValidateSqlDataReader(DataReader, new System.Globalization.CultureInfo("en-GB"));

            int Ordinal = DataReader.GetOrdinal(FieldName);
            return DataReader.IsDBNull(Ordinal) ? (Decimal?)null : DataReader.GetDecimal(Ordinal);
        }

        /// <summary>
        /// Gets a Nullable Decimal value from a SqlDataReader
        /// </summary>
        /// <param name="DataReader"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public static string GetNullableString(this SqlDataReader DataReader, string FieldName)
        {
            //Validate Arguments
            ArgumentValidation.ValidateSqlDataReader(DataReader, new System.Globalization.CultureInfo("en-GB"));

            int Ordinal = DataReader.GetOrdinal(FieldName);
            return DataReader.IsDBNull(Ordinal) ? null : DataReader.GetString(Ordinal);
        }

        /// <summary>
        /// Gets a Nullable Int32 value from a SqlDataReader
        /// </summary>
        /// <param name="DataReader"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public static Int32 GetNullableInt32(this SqlDataReader DataReader, string FieldName)
        {
            //Validate Arguments
            ArgumentValidation.ValidateSqlDataReader(DataReader, new System.Globalization.CultureInfo("en-GB"));

            int Ordinal = DataReader.GetOrdinal(FieldName);
            return DataReader.IsDBNull(Ordinal) ? 0 : DataReader.GetInt32(Ordinal);
        }

        /// <summary>
        /// Gets a Nullable Int16 value from a SqlDataReader
        /// </summary>
        /// <param name="DataReader"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public static Int16 GetNullableInt16(this SqlDataReader DataReader, string FieldName)
        {
            //Validate Arguments
            ArgumentValidation.ValidateSqlDataReader(DataReader, new System.Globalization.CultureInfo("en-GB"));

            int Ordinal = DataReader.GetOrdinal(FieldName);
            return DataReader.IsDBNull(Ordinal) ? Convert.ToInt16(0) : DataReader.GetInt16(Ordinal);
        }

        /// <summary>
        /// Gets a Nullable Byte value from a SqlDataReader
        /// </summary>
        /// <param name="DataReader"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public static byte GetNullableByte(this SqlDataReader DataReader, string FieldName)
        {
            //Validate Arguments
            ArgumentValidation.ValidateSqlDataReader(DataReader, new System.Globalization.CultureInfo("en-GB"));

            int Ordinal = DataReader.GetOrdinal(FieldName);
            return DataReader.IsDBNull(Ordinal) ? Convert.ToByte(0) : DataReader.GetByte(Ordinal);
        }
    }
}
