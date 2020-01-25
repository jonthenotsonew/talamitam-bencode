using System;
using System.Collections.Generic;

using TalamitamBencode.Types;

using NUnit.Framework;

namespace TalamitamBencode
{
    [TestFixture]
    public class BencodeStringTests : BaseTests
    {
        // should error upon getting null value
        [Test]
        public void TestBencodeString100()
        {
            var ex = Assert.Throws<TalamitamException>(() => { var x = new BencodeString(null); });
            Assert.AreEqual("invalid value", ex.Message);
        }
        
        // should error upon getting "" value
        [Test]
        public void TestBencodeString105()
        {
            var ex = Assert.Throws<TalamitamException>(() => { var x = new BencodeString(""); });
            Assert.AreEqual("invalid value", ex.Message);
        }
        
        [Test]
        public void TestToBencode100()
        {
            var fixture = new BencodeString("abc");
            var returned = fixture.ToBencode();
            Assert.AreEqual("3:abc", returned);
        }
        
        [Test]
        public void TestToBencode105()
        {
            var fixture = new BencodeString("abcdefghij");
            var returned = fixture.ToBencode();
            Assert.AreEqual("10:abcdefghij", returned);
        }
    }
}