using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TalamitamBencode.Types
{
    public class BencodeNumber : BencodeType
    {
        private Int64 theValue;
        
        public BencodeNumber(Int64 anInt64)
        {
            theValue = anInt64;
        }
        
        public BencodeNumber(BinaryReader aReader, Int32 initial)
        {
            var foundStart = Convert.ToChar(initial).Equals('i');
            var foundEnd = false;
            
            var value = new StringBuilder();
            
            var current = aReader.Read();
            while(current != -1)
            {
                var temp = Convert.ToChar(current);
                if(temp.Equals('e'))
                {
                    foundEnd = true;
                    break;
                }
                else
                {
                    value.Append(temp);
                    current = aReader.Read();
                }
            }
            
            // error on strings like this 'ie'
            if(value.Length == 0)
                throw new TalamitamException("invalid bencoded string");
            
            // error on strings like this 'i1'
            if(foundStart == false || foundEnd == false)
                throw new TalamitamException("invalid bencoded string");
            
            // error on strings like this 'i--1e'
            Int64 number;
            if(Int64.TryParse(value.ToString(), out number) == false)
                throw new TalamitamException("invalid bencoded string");
            
            theValue = Convert.ToInt64(value.ToString());
        }
        
        public override String ToBencode()
        {
            return "i" + theValue.ToString() + "e";
        }
        
        public override String ToJson()
        {
            return theValue.ToString();
        }
    }
}