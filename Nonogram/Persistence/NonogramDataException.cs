using System;
using System.Collections.Generic;
using System.Text;

namespace Nonogram.Persistence
{
    public class NonogramDataException : Exception
    {
        public NonogramDataException() { }
        public NonogramDataException(string message) : base(message) { }
        public NonogramDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
