using System;

using TalamitamBencode.Types;

using NUnit.Framework;

namespace TalamitamBencode
{
    [TestFixture]
    public class BencodeNumberTests : BaseTests
    {
        // encode single digit
        [Test]
        public void TestToBencode100()
        {
            var fixture = new BencodeNumber(0);
            var returned = fixture.ToBencode();
            Assert.AreEqual("i0e", returned);
        }
        
        // encode negative single digit
        [Test]
        public void TestToBencode105()
        {
            var fixture = new BencodeNumber(-1);
            var returned = fixture.ToBencode();
            Assert.AreEqual("i-1e", returned);
        }
        
        // encode double digit
        [Test]
        public void TestToBencode110()
        {
            var fixture = new BencodeNumber(11);
            var returned = fixture.ToBencode();
            Assert.AreEqual("i11e", returned);
        }
        
        // encode negative double digit
        [Test]
        public void TestToBencode115()
        {
            var fixture = new BencodeNumber(-11);
            var returned = fixture.ToBencode();
            Assert.AreEqual("i-11e", returned);
        }
        
        // encode triple digit
        [Test]
        public void TestToBencode120()
        {
            var fixture = new BencodeNumber(101);
            var returned = fixture.ToBencode();
            Assert.AreEqual("i101e", returned);
        }
        
        // encode negative triple digit
        [Test]
        public void TestToBencode125()
        {
            var fixture = new BencodeNumber(-101);
            var returned = fixture.ToBencode();
            Assert.AreEqual("i-101e", returned);
        }
    }
}