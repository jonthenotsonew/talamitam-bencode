using System;

namespace TalamitamBencode.Types
{
    public abstract class BencodeType
    {
        public Boolean IsBencodeNumber
        {
            get
            {
                return (this is BencodeNumber);
            }
        }
        
        public Boolean IsBencodeString
        {
            get
            {
                return (this is BencodeString);
            }
        }
        
        public Boolean IsBencodeList
        {
            get
            {
                return (this is BencodeList);
            }
        }
        
        public Boolean IsBencodeDictionary
        {
            get
            {
                return (this is BencodeDictionary);
            }
        }

        public abstract String ToBencode();
        public abstract String ToJson();
    }
}