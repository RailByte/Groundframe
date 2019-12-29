using System;
using System.Collections.Generic;
using System.Text;

namespace GroundFrame.Classes
{
    /// <summary>
    /// Custom exception for handling no rows returned from GroundFrame.SQL database
    /// </summary>
    public class NoRowsException : Exception
    {
        public NoRowsException()
        {
        }

        public NoRowsException(string Message)
            : base(Message)
        {
        }

        public NoRowsException(string Message, Exception Inner)
            : base(Message, Inner)
        {
        }
    }
}
