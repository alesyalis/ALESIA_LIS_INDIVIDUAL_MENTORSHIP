using System;

namespace Weather.BL.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) 
        : base(message)
        {
        }

        public ValidationException() 
        : base()
        {
        }

    }
}
