using System;

using TalamitamBencode.Types;

using NUnit.Framework;

namespace TalamitamBencode
{
    [TestFixture]
    public class BencodeDictionaryTests : BaseTests
    {
        [Test]
        public void TestCount100()
        {
            var fixture = new BencodeDictionary();
            Assert.AreEqual(0, fixture.Count);
        }
        
        [Test]
        public void TestAdd100()
        {
            var fixture = new BencodeDictionary();
            var key1 = new BencodeString("a");
            var value1 = new BencodeNumber(2);
            fixture.Add(key1, value1);
            Assert.AreEqual(1, fixture.Count);
        }
        
        [Test]
        public void TestAdd105()
        {
            var fixture = new BencodeDictionary();
            var key1 = new BencodeString("a");
            var value1 = new BencodeString("b");;
            fixture.Add(key1, value1);
            var key2 = new BencodeString("c");
            var value2 = new BencodeNumber(2);
            fixture.Add(key2, value2);
            Assert.AreEqual(2, fixture.Count);
        }

        [Test]
        public void TestAdd110()
        {
            var fixture = new BencodeDictionary();
            var key1 = new BencodeString("abc");
            var value1 = new BencodeString("def");;
            var ex = Assert.Throws<TalamitamException>(() => { fixture.Add(null, value1); });
            Assert.AreEqual("invalid key null", ex.Message);
        }
        
        [Test]
        public void TestAdd115()
        {
            var fixture = new BencodeDictionary();
            var key1 = new BencodeString("abc");
            var value1 = new BencodeString("def");;
            var ex = Assert.Throws<TalamitamException>(() => { fixture.Add(key1, null); });
            Assert.AreEqual("invalid value null", ex.Message);
        }
        
        [Test]
        public void TestToBencode100()
        {
            var fixture = new BencodeDictionary();
            var key1 = new BencodeString("a");
            var value1 = new BencodeString("b");;
            fixture.Add(key1, value1);
            var returned = fixture.ToBencode();
            Assert.AreEqual("d1:a1:be", returned);
        }
        
        [Test]
        public void TestToBencode105()
        {
            var fixture = new BencodeDictionary();
            var key1 = new BencodeString("a");
            var value1 = new BencodeNumber(1);;
            fixture.Add(key1, value1);
            var returned = fixture.ToBencode();
            Assert.AreEqual("d1:ai1ee", returned);
        }
        
        [Test]
        public void TestToBencode110()
        {
            var fixture = new BencodeDictionary();
            var key1 = new BencodeString("c");
            var value1 = new BencodeNumber(3);;
            fixture.Add(key1, value1);
            var key2 = new BencodeString("b");
            var value2 = new BencodeNumber(2);;
            fixture.Add(key2, value2);
            var key3 = new BencodeString("a");
            var value3 = new BencodeNumber(1);;
            fixture.Add(key3, value3);
            var returned = fixture.ToBencode();
            Assert.AreEqual("d1:ai1e1:bi2e1:ci3ee", returned);
        }
    }
}