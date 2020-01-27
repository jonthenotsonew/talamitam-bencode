using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TalamitamBencode.Types
{
    public class BencodePieces : BencodeType
    {
        private IList<Byte> theBytes;
        
        public BencodePieces(Byte[] bytes)
        {
            throw new Exception("not implemented yet");
        }
        
        public BencodePieces(BinaryReader aReader, Int32 initial)
        {
            theBytes = new List<Byte>(); 
            
            // append initial number to get length
            var length = new StringBuilder();
            length.Append(Convert.ToChar(initial));
            
            var count = 0;
            var parsingLength = true;
            
            var theStream = aReader.BaseStream;
            
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
                    break;
                }
            }
            
            var currentByte = aReader.ReadByte();
            while(true)
            {
                if(aReader.BaseStream.Length == aReader.BaseStream.Position)
                    break;
                
                theBytes.Add(Convert.ToByte(currentByte));
                count--;

                if(count == 0)
                    break;
                
                currentByte = aReader.ReadByte();
            }

            if(count != 0)
                throw new TalamitamException("invalid bencoded string");
        }
        
        public override String ToBencode()
        {
            throw new Exception("not implemented yet");
        }
        
        public override String ToJson()
        {
            var returnValue = new StringBuilder();
            returnValue.Append('[');
            
            var count = 0;
            var temp = new Byte[20];
            foreach(var aByte in theBytes)
            {
                if(count == 19)
                {
                    temp[count] = aByte;
                    var hex = BitConverter.ToString(temp).Replace("-","");
                    returnValue.Append('"');
                    returnValue.Append(hex);
                    returnValue.Append('"');
                    returnValue.Append(',');
                    count = 0;
                    continue;
                }
                
                temp[count] = aByte;
                count++;                
            }
            
            returnValue.Append(']');
            returnValue.Replace(",]", "]");
            return returnValue.ToString();
        }
    }
}