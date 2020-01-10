using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            int Ordinal = DataReader.GetOrdinal(FieldName);
            return DataReader.IsDBNull(Ordinal) ? (Decimal?)null : DataReader.GetDecimal(Ordinal);
        }
    }
}
