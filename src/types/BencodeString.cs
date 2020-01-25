using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using TalamitamBencode;

namespace TalamitamBencode.Types
{
    public class BencodeString : BencodeType
    {
        private String theValue;
        
        public String TheValue
        {
            get 
            { 
                return theValue; 
            }
        }
        
        public BencodeString(String aString)
        {
            if(aString == null)
                throw new TalamitamException("invalid value");
            
            if(aString.Length == 0)
                throw new TalamitamException("invalid value");
            
            theValue = aString;
        }
        
        // parses byte string into an object
        public BencodeString(TextReader aReader, Int32 initial)
        {
            // append initial number to get length
            var length = new StringBuilder();
            length.Append(Convert.ToChar(initial));
            
            var count = 0;
            var parsingLength = true;
            
            var value = new StringBuilder();
            
            var current = aReader.Read();
            while(current != -1)
            {
                // get initial length
                var temp = Convert.ToChar(current).ToString();
                Int32 number;
                if(Int32.TryParse(temp, out number) && parsingLength)
                {
                    length.Append(temp);
                    current = aReader.Read();
                    continue;
                }

                // : means everything afterwards within length is value
                if(temp.Equals(":") && parsingLength)
                {
                    count += Convert.ToInt32(length.ToString());
                    parsingLength = false;
                    current = aReader.Read();
                    continue;
                }
                
                // save value
                value.Append(temp);
                count--;
                
                if(count <= 0)
                    break;
                
                current = aReader.Read();
            }
            
            if(count != 0)
                throw new TalamitamException("invalid bencoded string");
            
            theValue = value.ToString();
        }
        
        public override String ToBencode()
        {
            return theValue.Length.ToString() + ":" + theValue;
        }
        
        public override String ToJson()
        {
            var replacement = new StringBuilder();
            replacement.Append('\\');
            replacement.Append('\\');
            replacement.Append('"');
            var doubleQuote = '"';
            var temp = theValue.Replace(doubleQuote.ToString(), replacement.ToString());
            
            var returnValue = new StringBuilder();
            returnValue.Append('"');
            returnValue.Append(temp);
            returnValue.Append('"');
            return returnValue.ToString();
        }
    }
}