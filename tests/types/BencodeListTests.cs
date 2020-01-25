using System;

using TalamitamBencode.Types;

using NUnit.Framework;

namespace TalamitamBencode
{
    [TestFixture]
    public class BencodeListTests : BaseTests
    {
        
        // should error upon getting null value
        [Test]
        public void TestCount100()
        {
            var fixture = new BencodeList();
            Assert.AreEqual(0, fixture.Count);
        }
        
        [Test]
        public void TestAdd100()
        {
            var fixture = new BencodeList();
            var test1 = new BencodeNumber(1);
            fixture.Add(test1);
            Assert.AreEqual(1, fixture.Count);
        }
        
        [Test]
        public void TestAdd105()
        {
            var fixture = new BencodeList();
            var test1 = new BencodeString("abc");
            fixture.Add(test1);
            Assert.AreEqual(1, fixture.Count);
        }
        
        [Test]
        public void TestAdd110()
        {
            var fixture = new BencodeList();
            var test1 = new BencodeString("abc");
            fixture.Add(test1);
            var test2 = new BencodeNumber(1);
            fixture.Add(test2);
            Assert.AreEqual(2, fixture.Count);
        }
        
        [Test]
        public void TestAdd115()
        {
            var fixture = new BencodeList();
            var ex = Assert.Throws<TalamitamException>(() => { fixture.Add(null); });
            Assert.AreEqual("invalid value", ex.Message);
        }
        
        [Test]
        public void TestToBencode100()
        {
            var fixture = new BencodeList();
            var returned = fixture.ToBencode();
            Assert.AreEqual("le", returned);
        }
        
        [Test]
        public void TestToBencode105()
        {
            var fixture = new BencodeList();
            var test1 = new BencodeNumber(1);
            fixture.Add(test1);
            var returned = fixture.ToBencode();
            Assert.AreEqual("li1ee", returned);
        }
        
        [Test]
        public void TestToBencode110()
        {
            var fixture = new BencodeList();
            var test1 = new BencodeString("abc");
            fixture.Add(test1);
            var returned = fixture.ToBencode();
            Assert.AreEqual("l3:abce", returned);
        }
        
        [Test]
        public void TestToBencode115()
        {
            var fixture = new BencodeList();
            var test1 = new BencodeString("abc");
            fixture.Add(test1);
            var test2 = new BencodeNumber(1);
            fixture.Add(test2);
            var returned = fixture.ToBencode();
            Assert.AreEqual("l3:abci1ee", returned);
        }
        
        [Test]
        public void TestToBencode120()
        {
            var fixture = new BencodeList();
            var test1 = new BencodeNumber(11);
            fixture.Add(test1);
            var test2 = new BencodeString("abcdefghij");
            fixture.Add(test2);
            var returned = fixture.ToBencode();
            Assert.AreEqual("li11e10:abcdefghije", returned);
        }
        
        [Test]
        public void TestToBencode125()
        {
            var fixture = new BencodeList();
            var test1 = new BencodeNumber(1);
            fixture.Add(test1);
            var test2 = new BencodeNumber(11);
            fixture.Add(test2);
            var returned = fixture.ToBencode();
            Assert.AreEqual("li1ei11ee", returned);
        }
        
        [Test]
        public void TestToBencode130()
        {
            var fixture = new BencodeList();
            var test1 = new BencodeString("abc");
            fixture.Add(test1);
            var test2 = new BencodeString("def");
            fixture.Add(test2);
            var returned = fixture.ToBencode();
            Assert.AreEqual("l3:abc3:defe", returned);
        }
        
        [Test]
        public void TestToBencode135()
        {
            var fixture = new BencodeList();
            var test1 = new BencodeList();
            test1.Add(new BencodeNumber(1));
            fixture.Add(test1);
            var test2 = new BencodeList();
            test2.Add(new BencodeNumber(2));
            fixture.Add(test2);
            var returned = fixture.ToBencode();
            Assert.AreEqual("lli1eeli2eee", returned);
        }
        
        [Test]
        public void TestToBencode140()
        {
            var fixture = new BencodeList();
            var test1 = new BencodeList();
            test1.Add(new BencodeString("a"));
            fixture.Add(test1);
            var test2 = new BencodeList();
            test2.Add(new BencodeString("b"));
            fixture.Add(test2);
            var returned = fixture.ToBencode();
            Assert.AreEqual("ll1:ael1:bee", returned);
        }
        
        [Test]
        public void TestToBencode145()
        {
            var fixture = new BencodeList();
            var test1 = new BencodeDictionary();
            test1.Add(new BencodeString("a"), new BencodeNumber(1));
            fixture.Add(test1);
            var test2 = new BencodeDictionary();
            test2.Add(new BencodeString("b"), new BencodeNumber(2));
            fixture.Add(test2);
            var returned = fixture.ToBencode();
            Assert.AreEqual("ld1:ai1eed1:bi2eee", returned);
        }
    }
}