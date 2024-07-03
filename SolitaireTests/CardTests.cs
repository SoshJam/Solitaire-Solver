using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire;

namespace SolitaireTests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void TestGetSuit()
        {
            Assert.AreEqual(Suit.Spades, Card.GetSuit('A'));
            Assert.AreEqual(Suit.Clubs, Card.GetSuit('N'));
            Assert.AreEqual(Suit.Hearts, Card.GetSuit('a'));
            Assert.AreEqual(Suit.Diamonds, Card.GetSuit('n'));
        }

        [TestMethod]
        public void TestGetValue()
        {
            Assert.AreEqual(1, Card.GetValue('A'));
            Assert.AreEqual(2, Card.GetValue('O'));
            Assert.AreEqual(10, Card.GetValue('j'));
            Assert.AreEqual(13, Card.GetValue('z'));
        }

        [TestMethod]
        public void TestFromSuitAndValue()
        {
            Assert.AreEqual('A', Card.FromSuitAndValue(Suit.Spades, 1));
            Assert.AreEqual('O', Card.FromSuitAndValue(Suit.Clubs, 2));
            Assert.AreEqual('j', Card.FromSuitAndValue(Suit.Hearts, 10));
            Assert.AreEqual('z', Card.FromSuitAndValue(Suit.Diamonds, 13));
        }

        [TestMethod]
        public void TestFromSuitAndValueInvalidValue()
        {
            Assert.ThrowsException<ArgumentException>(() => Card.FromSuitAndValue(Suit.Spades, 0));
            Assert.ThrowsException<ArgumentException>(() => Card.FromSuitAndValue(Suit.Spades, 14));
        }

        [TestMethod]
        public void TestFromString()
        {
            Assert.AreEqual('A', Card.FromString("A♠"));
            Assert.AreEqual('O', Card.FromString("2♣"));
            Assert.AreEqual('j', Card.FromString("T♥"));
            Assert.AreEqual('z', Card.FromString("K♦"));
        }

        [TestMethod]
        public void TestFromStringLetters()
        {
            Assert.AreEqual('A', Card.FromString("AS"));
            Assert.AreEqual('O', Card.FromString("2C"));
            Assert.AreEqual('j', Card.FromString("TH"));
            Assert.AreEqual('z', Card.FromString("KD"));
        }

        [TestMethod]
        public void TestFromStringInvalidLength()
        {
            Assert.ThrowsException<ArgumentException>(() => Card.FromString("1"));
            Assert.ThrowsException<ArgumentException>(() => Card.FromString("123"));
        }

        [TestMethod]
        public void TestFromStringInvalidFormat()
        {
            Assert.ThrowsException<ArgumentException>(() => Card.FromString("♠A"));
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("A♠", Card.ToString('A'));
            Assert.AreEqual("2♣", Card.ToString('O'));
            Assert.AreEqual("T♥", Card.ToString('j'));
            Assert.AreEqual("J♦", Card.ToString('x'));
            Assert.AreEqual("Q♦", Card.ToString('y'));
            Assert.AreEqual("K♦", Card.ToString('z'));
        }

        [TestMethod]
        public void TestToStringInvalid()
        {
            Assert.ThrowsException<ArgumentException>(() => Card.ToString((char)64));
            Assert.ThrowsException<ArgumentException>(() => Card.ToString((char)91));
            Assert.ThrowsException<ArgumentException>(() => Card.ToString((char)123));
        }

        [TestMethod]
        public void TestIsBlack()
        {
            Assert.IsTrue(Card.IsBlack('A'));
            Assert.IsTrue(Card.IsBlack('N'));

            Assert.IsFalse(Card.IsBlack('a'));
            Assert.IsFalse(Card.IsBlack('n'));
        }
    }
}