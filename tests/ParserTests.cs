using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

using NUnit.Framework;

namespace TalamitamBencode
{
    [TestFixture]
    public class ParserTests : BaseTests
    {
		private Stream GetBinaryStream(String bencode)
		{
			var theStream = new MemoryStream();
			var theWriter = new BinaryWriter(theStream);
			foreach(var aChar in bencode)
			{
				theWriter.Write(aChar);
			}
			theStream.Seek(0, SeekOrigin.Begin);
			return theStream;
		}
		

		
        // should error on empty stream
        [Test]
        public void TestParse100()
        {
            using(var reader = new BinaryReader(GetBinaryStream("")))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        // should parse number
        [Test]
        public void TestParseNumber100()
        {
            using(var reader = new BinaryReader(GetBinaryStream("i0e")))
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
            using(var reader = new BinaryReader(GetBinaryStream("i-1e")))
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
            using(var reader = new BinaryReader(GetBinaryStream("i11e")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeNumber);
                Assert.AreEqual("11", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseNumber115()
        {
            using(var reader = new BinaryReader(GetBinaryStream("ie")))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseNumber120()
        {
            using(var reader = new BinaryReader(GetBinaryStream("i1")))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseNumber125()
        {
            using(var reader = new BinaryReader(GetBinaryStream("i--1e")))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        // should parse string
        [Test]
        public void TestParseString100()
        {
            using(var reader = new BinaryReader(GetBinaryStream("3:abc")))
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
            using(var reader = new BinaryReader(GetBinaryStream("3:a23")))
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
            using(var reader = new BinaryReader(GetBinaryStream("3::23")))
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
            using(var reader = new BinaryReader(GetBinaryStream("10:abcde12345")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeString);
                Assert.AreEqual("\"abcde12345\"", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseString120()
        {
            using(var reader = new BinaryReader(GetBinaryStream("3:1:a")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeString);
                Assert.AreEqual("\"1:a\"", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseString125()
        {
            using(var reader = new BinaryReader(GetBinaryStream("1:\"")))
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
            using(var reader = new BinaryReader(GetBinaryStream("1:")))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseString135()
        {
            using(var reader = new BinaryReader(GetBinaryStream("3:ab")))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseList100()
        {
            using(var reader = new BinaryReader(GetBinaryStream("le")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList105()
        {
            using(var reader = new BinaryReader(GetBinaryStream("li1ee")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[1]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList110()
        {
            using(var reader = new BinaryReader(GetBinaryStream("l1:ae")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[\"a\"]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList115()
        {
            using(var reader = new BinaryReader(GetBinaryStream("li1ei2ee")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[1,2]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList120()
        {
            using(var reader = new BinaryReader(GetBinaryStream("l1:a1:be")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[\"a\",\"b\"]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList125()
        {
            using(var reader = new BinaryReader(GetBinaryStream("l1:ai1ee")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[\"a\",1]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList130()
        {
            using(var reader = new BinaryReader(GetBinaryStream("li11e10:abcdefghiji23ee")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[11,\"abcdefghij\",23]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList135()
        {
            using(var reader = new BinaryReader(GetBinaryStream("lli1eel1:aee")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeList);
                Assert.AreEqual("[[1],[\"a\"]]", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseList140()
        {
            using(var reader = new BinaryReader(GetBinaryStream("l")))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseDictionary100()
        {
            using(var reader = new BinaryReader(GetBinaryStream("de")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary105()
        {
            using(var reader = new BinaryReader(GetBinaryStream("d1:ai1ee")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{\"a\":1}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary110()
        {
            using(var reader = new BinaryReader(GetBinaryStream("d1:ai1e1:bi2ee")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{\"a\":1,\"b\":2}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary115()
        {
            using(var reader = new BinaryReader(GetBinaryStream("d1:a1:b1:c1:de")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{\"a\":\"b\",\"c\":\"d\"}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary120()
        {
            using(var reader = new BinaryReader(GetBinaryStream("d1:ali1eee")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{\"a\":[1]}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary125()
        {
            using(var reader = new BinaryReader(GetBinaryStream("d1:ad1:bi1eee")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{\"a\":{\"b\":1}}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary130()
        {
            using(var reader = new BinaryReader(GetBinaryStream("d1:ad1:bl1:ceee")))
            {
                var bencodeType = Parser.Parse(reader);
                Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
                Assert.AreEqual("{\"a\":{\"b\":[\"c\"]}}", bencodeType.ToJson());
            }
        }
        
        [Test]
        public void TestParseDictionary135()
        {
            using(var reader = new BinaryReader(GetBinaryStream("d")))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseDictionary140()
        {
            using(var reader = new BinaryReader(GetBinaryStream("di1e1:ae")))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
        
        [Test]
        public void TestParseDictionary145()
        {
            using(var reader = new BinaryReader(GetBinaryStream("d1:ae")))
            {
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
            }
        }
		
		private Byte[] GetSha1(String something)
		{
			using (SHA1Managed sha1 = new SHA1Managed())
			{
				return sha1.ComputeHash(Encoding.UTF8.GetBytes(something));
			}
		}
		
		private Stream GetPiecesStream(Byte[][] pieces)
		{
			var theStream = new MemoryStream();
			var theWriter = new BinaryWriter(theStream);
			theWriter.Write('d');
			theWriter.Write('6');
			theWriter.Write(':');
			theWriter.Write('p');
			theWriter.Write('i');
			theWriter.Write('e');
			theWriter.Write('c');
			theWriter.Write('e');
			theWriter.Write('s');

			var temp = pieces.Length * 20;
			var lengthString = temp.ToString();
			foreach(var lengthChar in lengthString)
			{
				theWriter.Write(lengthChar);
			}

			theWriter.Write(':');

			foreach(var piece in pieces)
			{
				theWriter.Write(piece);
			}
			
			theWriter.Write('e');
			
			theStream.Seek(0, SeekOrigin.Begin);
			return theStream;
		}
        
        [Test]
        public void TestParsePieces100()
        {
			var sha11 = GetSha1("test1");
            var pieces = new Byte[1][] { sha11 };
            var hex1 = BitConverter.ToString(sha11).Replace("-","");
            
			var theStream = GetPiecesStream(pieces);
			using(var reader = new BinaryReader(theStream))
			{
				var bencodeType = Parser.Parse(reader);
				Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
				var expected = "{\"pieces\":[\"" + hex1 + "\"]}";
				Assert.AreEqual(expected, bencodeType.ToJson());
			}
        }
		
        [Test]
        public void TestParsePieces105()
        {
			var sha11 = GetSha1("test1");
			var sha12 = GetSha1("test1");
            var pieces = new Byte[2][] { sha11, sha12 };
            var hex1 = BitConverter.ToString(sha11).Replace("-","");
			var hex2 = BitConverter.ToString(sha12).Replace("-","");
            
			var theStream = GetPiecesStream(pieces);
			using(var reader = new BinaryReader(theStream))
			{
				var bencodeType = Parser.Parse(reader);
				Assert.AreEqual(true, bencodeType.IsBencodeDictionary);
				var expected = "{\"pieces\":[\"" + hex1 + "\",\"" + hex2 + "\"]}";
				Assert.AreEqual(expected, bencodeType.ToJson());
			}
        }
		
        [Test]
        public void TestParsePieces115()
        {
			var invalid = new Byte[1] { 1 };
            var pieces = new Byte[1][] { invalid };
            
			var theStream = GetPiecesStream(pieces);
			using(var reader = new BinaryReader(theStream))
			{
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
			}
        }
        [Test]
        public void TestParsePieces120()
        {
			var sha11 = GetSha1("test1");
			var invalid = new Byte[1] { 1 };
            var pieces = new Byte[2][] { sha11, invalid };
            
			var theStream = GetPiecesStream(pieces);
			using(var reader = new BinaryReader(theStream))
			{
                var ex = Assert.Throws<TalamitamException>(() => { Parser.Parse(reader); });
                Assert.AreEqual("invalid bencoded string", ex.Message);
			}
        }
    }
}