using System;

namespace TalamitamBencode
{
    // just an exception for error messages
    public class TalamitamException : Exception
    {
        public TalamitamException(String message) : base(message)
        {
        }
    }
}