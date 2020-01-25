using System;
using System.Text;

namespace TalamitamBencode
{
    public class BaseTests
    {        
        protected Byte GetByte(Char aChar)
        {
            var bytes = Encoding.UTF8.GetBytes(new Char[] { aChar });
            return bytes[0];
        }
    }
}