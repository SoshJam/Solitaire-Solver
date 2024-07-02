using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire;

namespace SolitaireTests
{
    /*
    [TestClass]
    public class GameStateTests
    {
        /// <summary>
        /// Sets up a game state with spade cards from 1 to 7 on the board.
        /// </summary>
        /// <returns>The game state.</returns>
        private GameState SetUpGameState()
        {
            // Creates a GameState with Spade cards for 1 to 7
            int cardIndex = 1;
            GameState gs = new GameState(() => Card.FromString($"s{cardIndex++}"));

            return gs;
        }

        /// <summary>
        /// Helper method. Checks if two IEnumerables are equal.
        /// </summary>
        /// <typeparam name="T">The elements in the collections.</typeparam>
        /// <param name="expected">The expected collection contents.</param>
        /// <param name="actual">The actual contents of the collection.</param>
        /// <returns>True if the collections match.</returns>
        private bool CheckCollections<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            if (expected.Count() != actual.Count())
                return false;

            foreach (T item in expected)
                if (!actual.Contains(item))
                    return false;

            return true;
        }

        /// <summary>
        /// Converts a collection of strings into cards.
        /// </summary>
        /// <param name="strings">The strings representing cards.</param>
        /// <returns>A collection of cards.</returns>
        private IEnumerable<Card> CreateCardsFromStrings(IEnumerable<string> strings)
        {
            foreach (string s in strings)
                yield return Card.FromString(s);
        }

        // Tests

        /// <summary>
        /// Ensures the tops and bottoms of each pile is correct after adding,
        /// removing, and revealing cards.
        /// </summary>
        [TestMethod]
        public void TestPileTopsAndBottoms()
        {
            // Test from constructor

            IEnumerable<Card> expectedBottoms = CreateCardsFromStrings(new List<string> {
                    "sa", "s2", "s3","s4", "s5", "s6", "s7"
                });
            IEnumerable<Card> expectedTops = CreateCardsFromStrings(new List<string> {
                    "sa", "s2", "s3","s4", "s5", "s6", "s7"
                });

            GameState gs = SetUpGameState();

            Assert.IsTrue(CheckCollections(expectedBottoms, gs.GetBoardPileBottoms()));
            Assert.IsTrue(CheckCollections(expectedTops, gs.GetBoardPileTops()));

            // Add cards to some piles and check

            gs.AddToBoard(2, Card.FromString("d2"));
            gs.AddToBoard(4, Card.FromString("h4"));
            gs.AddToBoard(5, Card.FromString("h5"));

            expectedBottoms = CreateCardsFromStrings(new List<string> {
                    "sa", "s2", "s3","s4", "s5", "s6", "s7"
                });
            expectedTops = CreateCardsFromStrings(new List<string> {
                    "sa", "s2", "d2", "s4", "h4", "h5", "s7"
                });

            Assert.IsTrue(CheckCollections(expectedBottoms, gs.GetBoardPileBottoms()));
            Assert.IsTrue(CheckCollections(expectedTops, gs.GetBoardPileTops()));

            // Remove cards from pile

            Assert.AreEqual(
                Card.FromString("s2"),
                gs.RemoveFromBoard(1, () => Card.FromString("s8")));
            Assert.AreEqual(
                Card.FromString("h4"),
                gs.RemoveFromBoard(4, () => Card.FromString("sa")));
            Assert.AreEqual(
                Card.FromString("h5"),
                gs.RemoveFromBoard(5, () => Card.FromString("sa")));

            expectedBottoms = CreateCardsFromStrings(new List<string> {
                    "sa", "s8", "s3", "s4", "s5", "s6", "s7"
                });
            expectedTops = CreateCardsFromStrings(new List<string> {
                    "sa", "s8", "d2", "s4", "s5", "s6", "s7"
                });

            Assert.IsTrue(CheckCollections(expectedBottoms, gs.GetBoardPileBottoms()));
            Assert.IsTrue(CheckCollections(expectedTops, gs.GetBoardPileTops()));
        }

        /// <summary>
        /// Ensures one cannot remove a card from an empty pile
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestCannotRemoveFromEmptyPile()
        {
            GameState gs = SetUpGameState();

            Assert.AreEqual(
                Card.FromString("s2"),
                gs.RemoveFromBoard(1, () => Card.FromString("sk")));

            // Face-down card was revealed.

            Assert.AreEqual(
                Card.FromString("sk"),
                gs.RemoveFromBoard(1, () => Card.FromString("s8")));

            // Second pile should now be empty

            gs.RemoveFromBoard(1, () => Card.FromString("s0"));
        }

        /// <summary>
        /// Ensures we can add a king to an empty pile
        /// </summary>
        [TestMethod]
        public void TestLegalAddToBoardEmptyPile()
        {
            GameState gs = SetUpGameState();

            gs.RemoveFromBoard(0, () => Card.FromString("da"));

            bool threwException = false;
            try
            {
                gs.AddToBoard(0, Card.FromString("ck"));
            }
            catch
            {
                threwException = true;
            }
            Assert.IsFalse(threwException);
        }

        /// <summary>
        /// Ensures we cannot add a non-king card to an empty pile
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestIllegalAddToBoardEmptyPile()
        {
            GameState gs = SetUpGameState();

            gs.RemoveFromBoard(0, () => Card.FromString("da"));

            gs.AddToBoard(0, Card.FromString("ca"));
        }

        /// <summary>
        /// Ensures we cannot add a card if the colors do not alternate
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestIllegalAddToBoardMatchingColors()
        {
            SetUpGameState().AddToBoard(1, Card.FromString("ca"));
        }

        /// <summary>
        /// Ensures we cannot add a card if the number is greater
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestIllegalAddToBoardMatchingValues()
        {
            SetUpGameState().AddToBoard(6, Card.FromString("h8"));
        }

        /// <summary>
        /// Ensures we cannot add a card if the number is lower, but not immediately below
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestIllegalAddToBoardDistantValues()
        {
            SetUpGameState().AddToBoard(6, Card.FromString("h5"));
        }
    }
    */
}