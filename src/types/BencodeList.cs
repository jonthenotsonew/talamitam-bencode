using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TalamitamBencode.Types
{
    public class BencodeList : BencodeType
    {
        private IList<BencodeType> theList;
        
        public Int32 Count
        {
            get
            {
                return theList.Count;
            }
        }
        
        public BencodeList()
        {
            theList = new List<BencodeType>();
        }
        
        public BencodeList(TextReader aReader, Int32 initial)
        {
            theList = new List<BencodeType>();
            
            var current = initial;
            while(current != -1)
            {
                var temp = Parser.Parse(aReader);
                if(temp == null)
                    break;

                theList.Add(temp);
            }
        }
        
        public void Add(BencodeType item)
        {
            if(item == null)
                throw new TalamitamException("invalid value");
            
            theList.Add(item);
        }
        
        public override String ToBencode()
        {
            var temp = new StringBuilder();
            temp.Append("l");
            
            foreach(var item in theList)
            {
                temp.Append(item.ToBencode());
            }
            
            temp.Append("e");
            return temp.ToString();
        }
        
        public override String ToJson()
        {
            var returnValue = new StringBuilder();
            returnValue.Append('[');
            
            var index = 1;
            
            foreach(var item in theList)
            {
                returnValue.Append(item.ToJson());
                
                if(index < theList.Count)
                    returnValue.Append(',');
                
                index++;
            }
            
            returnValue.Append(']');
            return returnValue.ToString();
        }
    }
}