using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitaire;

namespace SolitaireTests
{
    [TestClass]
    public class GameStateTests
    {
        private GameState SetUpGameState()
        {
            // Creates a GameState with Spade cards for 1 to 7
            int cardIndex = 1;
            GameState gs = new GameState(() => Card.FromString($"s{cardIndex++}"));

            return gs;
        }

        private bool CheckCollections<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            if (expected.Count() != actual.Count())
                return false;

            foreach (T item in expected)
                if (!actual.Contains(item))
                    return false;

            return true;
        }

        private IEnumerable<Card> CreateCardsFromStrings(IEnumerable<string> strings)
        {
            foreach (string s in strings)
                yield return Card.FromString(s);
        }

        [TestMethod]
        public void TestPileTopsAndBottoms()
        {
            // Test from constructor
            IEnumerable<Card> expected = CreateCardsFromStrings(new List<string>
                {
                    "sa", "s2", "s3","s4", "s5", "s6", "s7"
                });

            GameState gs = SetUpGameState();

            Assert.IsTrue(CheckCollections(expected, gs.GetBoardPileBottoms()));
            Assert.IsTrue(CheckCollections(expected, gs.GetBoardPileTops()));

            // Add cards to some piles
            // TODO: Get thingy
        }
    }
}