using System;
using System.Collections.Generic;
using System.IO;

using TalamitamBencode.Types;

namespace TalamitamBencode
{
    public class Parser
    {
        public static BencodeType Parse(BinaryReader aReader)
        {
            var firstInt = aReader.Read();

            // throw an error if the reader is empty
            if(firstInt == -1)
                throw new TalamitamException("invalid bencoded string");
            
            var temp = Convert.ToChar(firstInt).ToString();

            if(temp.Equals("e"))
                return null;
            
            if(temp.Equals("d"))
                return new BencodeDictionary(aReader, firstInt);

            if(temp.Equals("i"))
                return new BencodeNumber(aReader, firstInt);

            if(temp.Equals("l"))
                return new BencodeList(aReader, firstInt);

            Int32 number;
            if(Int32.TryParse(temp, out number))
                return new BencodeString(aReader, firstInt);

            throw new Exception("not implemented yet");
        }
    }
}