using System;

using TalamitamBencode.Types;

using NUnit.Framework;

namespace TalamitamBencode
{
    [TestFixture]
    public class BencodeTypeTests
    {
        [Test]
        public void TestIsBencodeNumber100()
        {
            BencodeType fixture = new BencodeNumber(0);
            Assert.AreEqual(true, fixture.IsBencodeNumber);
        }
        
        [Test]
        public void TestIsBencodeNumber105()
        {
            BencodeType fixture = new BencodeString("a");
            Assert.AreEqual(false, fixture.IsBencodeNumber);
        }

        [Test]
        public void TestIsBencodeString100()
        {
            BencodeType fixture = new BencodeString("a");
            Assert.AreEqual(true, fixture.IsBencodeString);
        }
        
        [Test]
        public void TestIsBencodeString105()
        {
            BencodeType fixture = new BencodeNumber(0);
            Assert.AreEqual(false, fixture.IsBencodeString);
        }

        [Test]
        public void TestIsBencodeList100()
        {
            BencodeType fixture = new BencodeList();
            Assert.AreEqual(true, fixture.IsBencodeList);
        }
        
        [Test]
        public void TestIsBencodeList105()
        {
            BencodeType fixture = new BencodeNumber(0);
            Assert.AreEqual(false, fixture.IsBencodeList);
        }
        
        [Test]
        public void TestIsBencodeDictionary100()
        {
            BencodeType fixture = new BencodeDictionary();
            Assert.AreEqual(true, fixture.IsBencodeDictionary);
        }
        
        [Test]
        public void TestIsBencodeDictionary105()
        {
            BencodeType fixture = new BencodeNumber(0);
            Assert.AreEqual(false, fixture.IsBencodeDictionary);
        }
    }
}