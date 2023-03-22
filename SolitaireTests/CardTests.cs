using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire;

namespace SolitaireTests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            Card card1 = new Card(Suit.Spades, 1);

            Assert.AreEqual(Suit.Spades, card1.suit);
            Assert.AreEqual(1, card1.value);

            Card card2 = new Card(Suit.Hearts, 12);

            Assert.AreEqual(Suit.Hearts, card2.suit);
            Assert.AreEqual(12, card2.value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorInvalidValueMin()
        {
            new Card(Suit.Spades, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorInvalidValueMax()
        {
            new Card(Suit.Spades, 14);
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("3 of Clubs", new Card(Suit.Clubs, 3).ToString());
            Assert.AreEqual("10 of Diamonds", new Card(Suit.Diamonds, 10).ToString());

            Assert.AreEqual("Ace of Spades", new Card(Suit.Spades, 1).ToString());
            Assert.AreEqual("Jack of Diamonds", new Card(Suit.Diamonds, 11).ToString());
            Assert.AreEqual("Queen of Clubs", new Card(Suit.Clubs, 12).ToString());
            Assert.AreEqual("King of Hearts", new Card(Suit.Hearts, 13).ToString());
        }

        [TestMethod]
        public void TestIsBlack()
        {
            Assert.IsTrue(new Card(Suit.Spades, 1).IsBlack());
            Assert.IsTrue(new Card(Suit.Clubs, 1).IsBlack());
            Assert.IsFalse(new Card(Suit.Diamonds, 1).IsBlack());
            Assert.IsFalse(new Card(Suit.Hearts, 1).IsBlack());
        }
    }
}