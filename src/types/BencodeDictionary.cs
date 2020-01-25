using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using TalamitamBencode;

namespace TalamitamBencode.Types
{
    public class BencodeDictionary : BencodeType
    {
        private IDictionary<BencodeString, BencodeType> theDictionary;
        
        public Int32 Count
        {
            get
            {
                return theDictionary.Count;
            }
        }
        
        public BencodeDictionary()
        {
            theDictionary = new Dictionary<BencodeString, BencodeType>();
        }
        
        public BencodeDictionary(TextReader aReader, Int32 initial)
        {            
            theDictionary = new Dictionary<BencodeString, BencodeType>();
            
            BencodeString key = null;
            BencodeType value = null;
            
            var current = initial;
            while(current != -1)
            {
                var temp = Parser.Parse(aReader);
                if(temp == null)
                    break;
                
                if(key == null && temp.IsBencodeString == false)
                    throw new TalamitamException("invalid bencoded string");
                
                if(key == null && temp.IsBencodeString)
                {
                    key = (BencodeString)temp;
                    continue;
                }
                    
                if(value == null)
                    value = temp;

                // only bencode strings are allowed as keys
                if(key.IsBencodeString == false)
                    throw new TalamitamException("invalid bencoded string");

                // add the value if there is a key and value
                if(key != null && value != null)
                {
                    theDictionary.Add(key, value);
                    key = null;
                    value = null;
                }
            }
            
            // must have key for value and vice versa
            if(key != null || value != null)
                throw new TalamitamException("invalid bencoded string");
        }
        
        public void Add(BencodeString key, BencodeType value)
        {
            if(key == null)
                throw new TalamitamException("invalid key null");
            
            if(value == null)
                throw new TalamitamException("invalid value null");
            
            theDictionary.Add(key, value);
        }
        
        public override String ToBencode()
        {
            var returnValue = new StringBuilder();
            returnValue.Append("d");

            // put the string values in a list for sorting
            var sortableList = new List<String>();
            var sortableHash = new Dictionary<String, BencodeString>();
            var keys = theDictionary.Keys;
            foreach(var key1 in keys)
            {
                
                sortableList.Add(key1.TheValue);
                sortableHash.Add(key1.TheValue, key1);
            }
            
            // sort the keys lexigraphically
            sortableList.Sort();
            
            foreach(var temp in sortableList)
            {
                var key = sortableHash[temp];
                returnValue.Append(key.ToBencode());
                returnValue.Append(theDictionary[key].ToBencode());
            }
            
            returnValue.Append("e");
            return returnValue.ToString();;
        }
        
        public override String ToJson()
        {
            var returnValue = new StringBuilder();
            returnValue.Append("{");
            
            var index = 1;
            
            var keys = theDictionary.Keys;
            foreach(var key in keys)
            {
                returnValue.Append(key.ToJson());
                
                returnValue.Append(':');
                
                var temp = theDictionary[key];
                returnValue.Append(temp.ToJson());
                
                if(index < theDictionary.Count)
                    returnValue.Append(',');
                
                index++;
            }            
            
            returnValue.Append("}");
            return returnValue.ToString();
        }
    }
}