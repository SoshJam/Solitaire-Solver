namespace Solitaire
{
    public class GameState
    {
        /// <summary>
        /// The cards that are known to be face-up and in play, either on the
        /// board or in the foundation piles.
        /// </summary>
        public HashSet<Card> CardsInPlay { get; private set; }

        /// <summary>
        /// The facedown cards in the Stock Pile. There are 24 to start.
        /// </summary>
        public Queue<Card> StockPile { get; private set; }

        /// <summary>
        /// The faceup cards in the Waste Pile. When the StockPile is empty,
        /// this becomes the new StockPile and is replaced with a new Queue.
        /// </summary>
        public Queue<Card> WastePile { get; private set; }
    }
}