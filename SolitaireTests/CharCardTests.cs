using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire;

namespace SolitaireTests
{
    [TestClass]
    public class CharCardTests
    {
        [TestMethod]
        public void TestGetSuit()
        {
            Assert.AreEqual(Suit.Spades, CharCard.GetSuit('A'));
            Assert.AreEqual(Suit.Clubs, CharCard.GetSuit('N'));
            Assert.AreEqual(Suit.Hearts, CharCard.GetSuit('a'));
            Assert.AreEqual(Suit.Diamonds, CharCard.GetSuit('n'));
        }

        [TestMethod]
        public void TestGetValue()
        {
            Assert.AreEqual(1, CharCard.GetValue('A'));
            Assert.AreEqual(2, CharCard.GetValue('O'));
            Assert.AreEqual(10, CharCard.GetValue('j'));
            Assert.AreEqual(13, CharCard.GetValue('z'));
        }

        [TestMethod]
        public void TestFromString()
        {
            Assert.AreEqual('A', CharCard.FromString("A♠"));
            Assert.AreEqual('O', CharCard.FromString("2♣"));
            Assert.AreEqual('j', CharCard.FromString("T♥"));
            Assert.AreEqual('z', CharCard.FromString("K♦"));
        }

        [TestMethod]
        public void TestFromStringInvalidLength()
        {
            Assert.ThrowsException<ArgumentException>(() => CharCard.FromString("1"));
            Assert.ThrowsException<ArgumentException>(() => CharCard.FromString("123"));
        }

        [TestMethod]
        public void TestFromStringInvalidFormat()
        {
            Assert.ThrowsException<ArgumentException>(() => CharCard.FromString("♠A"));
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("A♠", CharCard.ToString('A'));
            Assert.AreEqual("2♣", CharCard.ToString('O'));
            Assert.AreEqual("T♥", CharCard.ToString('j'));
            Assert.AreEqual("J♦", CharCard.ToString('x'));
            Assert.AreEqual("Q♦", CharCard.ToString('y'));
            Assert.AreEqual("K♦", CharCard.ToString('z'));
        }

        [TestMethod]
        public void TestToStringInvalid()
        {
            Assert.ThrowsException<ArgumentException>(() => CharCard.ToString((char)64));
            Assert.ThrowsException<ArgumentException>(() => CharCard.ToString((char)91));
            Assert.ThrowsException<ArgumentException>(() => CharCard.ToString((char)123));
        }

        [TestMethod]
        public void TestIsBlack()
        {
            Assert.IsTrue(CharCard.IsBlack('A'));
            Assert.IsTrue(CharCard.IsBlack('N'));

            Assert.IsFalse(CharCard.IsBlack('a'));
            Assert.IsFalse(CharCard.IsBlack('n'));
        }
    }
}