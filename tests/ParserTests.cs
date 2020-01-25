using System;
using System.Text;
using System.IO;

using NUnit.Framework;

namespace TalamitamBencode
{
    [TestFixture]
    public class ParserTests : BaseTests
    {
        // should error on empty stream
        [Test]
        public void TestParse100()
        {
            using(var reader = new StringReader(""))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        // should parse number
        [Test]
        public void TestParseNumber100()
        {
            using(var reader = new StringReader("i0e"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeNumber);
                Assert.AreEqual("0", bencodeType.ToJson());
            }
        }
        
        // should parse number
        [Test]
        public void TestParseNumber105()
        {
            using(var reader = new StringReader("i-1e"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeNumber);
                Assert.AreEqual("-1", bencodeType.ToJson());
            }
        }
        
        // should parse number
        [Test]
        public void TestParseNumber110()
        {
            using(var reader = new StringReader("i11e"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeNumber);
                Assert.AreEqual("11", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseNumber115()
        {
            using(var reader = new StringReader("ie"))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseNumber120()
        {
            using(var reader = new StringReader("i1"))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseNumber125()
        {
            using(var reader = new StringReader("i--1e"))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        // should parse string
        [Test]
        public void TestParseString100()
        {
            using(var reader = new StringReader("3:abc"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeString);
                Assert.AreEqual("\"abc\"", bencodeType.ToJson());
            }
        }
        
        // should parse string
        [Test]
        public void TestParseString105()
        {
            using(var reader = new StringReader("3:a23"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeString);
                Assert.AreEqual("\"a23\"", bencodeType.ToJson());
            }
        }
        
        // should parse string
        [Test]
        public void TestParseString110()
        {
            using(var reader = new StringReader("3::23"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeString);
                Assert.AreEqual("\":23\"", bencodeType.ToJson());
            }
        }
        
        // should parse string
        [Test]
        public void TestParseString115()
        {
            using(var reader = new StringReader("10:abcde12345"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeString);
                Assert.AreEqual("\"abcde12345\"", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseString120()
        {
            using(var reader = new StringReader("3:1:a"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeString);
                Assert.AreEqual("\"1:a\"", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseString125()
        {
            using(var reader = new StringReader("1:\""))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeString);
                var returnValue = new StringBuilder();
                returnValue.Append('"');
                returnValue.Append('\\');
                returnValue.Append('\\');
                returnValue.Append('"');
                returnValue.Append('"');
                Assert.AreEqual(returnValue.ToString(), bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseString130()
        {
            using(var reader = new StringReader("1:"))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseString135()
        {
            using(var reader = new StringReader("3:ab"))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseList100()
        {
            using(var reader = new StringReader("le"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList105()
        {
            using(var reader = new StringReader("li1ee"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[1]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList110()
        {
            using(var reader = new StringReader("l1:ae"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[\"a\"]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList115()
        {
            using(var reader = new StringReader("li1ei2ee"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[1,2]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList120()
        {
            using(var reader = new StringReader("l1:a1:be"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[\"a\",\"b\"]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList125()
        {
            using(var reader = new StringReader("l1:ai1ee"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[\"a\",1]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList130()
        {
            using(var reader = new StringReader("li11e10:abcdefghiji23ee"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[11,\"abcdefghij\",23]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList135()
        {
            using(var reader = new StringReader("lli1eel1:aee"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[[1],[\"a\"]]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList140()
        {
            using(var reader = new StringReader("l"))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseDictionary100()
        {
            using(var reader = new StringReader("de"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary105()
        {
            using(var reader = new StringReader("d1:ai1ee"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{\"a\":1}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary110()
        {
            using(var reader = new StringReader("d1:ai1e1:bi2ee"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{\"a\":1,\"b\":2}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary115()
        {
            using(var reader = new StringReader("d1:a1:b1:c1:de"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{\"a\":\"b\",\"c\":\"d\"}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary120()
        {
            using(var reader = new StringReader("d1:ali1eee"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{\"a\":[1]}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary125()
        {
            using(var reader = new StringReader("d1:ad1:bi1eee"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{\"a\":{\"b\":1}}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary130()
        {
            using(var reader = new StringReader("d1:ad1:bl1:ceee"))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{\"a\":{\"b\":[\"c\"]}}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary135()
        {
            using(var reader = new StringReader("d"))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseDictionary140()
        {
            using(var reader = new StringReader("di1e1:ae"))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseDictionary145()
        {
            using(var reader = new StringReader("d1:ae"))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
    }
}