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
        public void TestEquals()
        {
            Card card = new Card(Suit.Spades, 1);                   // Is this your card?

            Assert.IsFalse(card.Equals(null));                      // Null
            Assert.IsFalse(card.Equals(0));                         // Primitive type
            Assert.IsFalse(card.Equals(new LinkedList<string>()));  // Reference type
            Assert.IsFalse(card.Equals(new Card(Suit.Clubs, 13)));  // Wrong card
            Assert.IsFalse(card.Equals(new Card(Suit.Spades, 13))); // Right suit, wrong value
            Assert.IsFalse(card.Equals(new Card(Suit.Clubs, 1)));   // Right value, wrong suit
            Assert.IsFalse(card.Equals(Card.FromString("ck")));     // Wrong card, but from string
            Assert.IsFalse(card.Equals(Card.FromString("sk")));     // Right suit, wrong value
            Assert.IsFalse(card.Equals(Card.FromString("c1")));     // Wrong suit, right value

            Assert.IsTrue(card.Equals(new Card(Suit.Spades, 1)));   // Right card
            Assert.IsTrue(card.Equals(Card.FromString("s1")));      // Right card, from string
        }

        [TestMethod]
        public void TestIsBlack()
        {
            Assert.IsTrue(new Card(Suit.Spades, 1).IsBlack());
            Assert.IsTrue(new Card(Suit.Clubs, 1).IsBlack());
            Assert.IsFalse(new Card(Suit.Diamonds, 1).IsBlack());
            Assert.IsFalse(new Card(Suit.Hearts, 1).IsBlack());
        }

        [TestMethod]
        public void TestCardFromString()
        {
            Assert.AreEqual(new Card(Suit.Diamonds, 2), Card.FromString("d2"));
            Assert.AreEqual(new Card(Suit.Clubs, 3), Card.FromString("C3"));
            Assert.AreEqual(new Card(Suit.Hearts, 9), Card.FromString("h9"));

            Assert.AreEqual(new Card(Suit.Spades, 1), Card.FromString("s1"));
            Assert.AreEqual(new Card(Suit.Diamonds, 10), Card.FromString("d0"));
            Assert.AreEqual(new Card(Suit.Clubs, 11), Card.FromString("Cj"));
            Assert.AreEqual(new Card(Suit.Hearts, 12), Card.FromString("hQ"));
            Assert.AreEqual(new Card(Suit.Spades, 13), Card.FromString("SK"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCardFromStringTooLong()
        {
            Card.FromString("s1 <- This would be an Ace of Spades if the string wasn't so long.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCardFromStringWrongFormat()
        {
            Card.FromString("3d");
        }
    }
}