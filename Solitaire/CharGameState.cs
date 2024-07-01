namespace Solitaire
{
    class CharGameState
    {
        /// <summary>
        /// The cards that are known to be face-up and in play, either on the
        /// board or in the foundation piles.
        /// </summary>
        public readonly HashSet<char> CardsInPlay;

        /// <summary>
        /// The cards that are known to be in either the stock pile or the waste
        /// pile
        /// </summary>
        public readonly HashSet<char> CardsInStock;

        /// <summary>
        /// The facedown cards in the Stock Pile. If a card is \0 it means
        /// we have not been through the pile yet.
        /// </summary>
        public readonly Stack<char> StockPile;

        /// <summary>
        /// The faceup cards in the Waste Pile. When the StockPile is empty
        /// and reset, everything gets popped from this and pushed to the StockPile.
        /// </summary>
        public readonly Stack<char> WastePile;

        private bool stockSeen; // If we have seen everything in the stockpile

        /// <summary>
        /// The faceup cards in the main game board. Has seven piles of cards.
        /// 
        /// These have to be lists because we can move any part of a stack.
        /// </summary>
        public readonly List<char>[] Board;

        /// <summary>
        /// The face-down cards on the board. There are 0-6 in each pile at the
        /// start of the game.
        /// </summary>
        public readonly int[] FaceDownCardsInBoard;

        /// <summary>
        /// The value of the top card of each foundation pile. If an entry is
        /// 0, there is nothing in the pile.
        /// </summary>
        public readonly Dictionary<Suit, int> FoundationPile;

        /// <summary>
        /// Requests a card that was facedown. Used when drawing from stock pile
        /// and revealing cards from the game board.
        /// </summary>
        /// <returns>The card that was drawn.</returns>
        public delegate char RequestCard();


        /// <summary>
        /// Tracks a new solitaire game.
        /// </summary>
        /// <param name="requestCard">A function to get the seven cards that will be face-up at the start of the game.</param>
        public CharGameState(RequestCard requestCard)
        {
            // Set up the various piles
            CardsInPlay = new HashSet<char>();
            CardsInStock = new HashSet<char>();
            StockPile = new Stack<char>();
            WastePile = new Stack<char>();
            Board = new List<char>[7];
            FaceDownCardsInBoard = new int[7];

            // Set up the Foundation piles
            FoundationPile = new Dictionary<Suit, int>();
            FoundationPile[Suit.Spades] = 0;
            FoundationPile[Suit.Hearts] = 0;
            FoundationPile[Suit.Clubs] = 0;
            FoundationPile[Suit.Diamonds] = 0;

            // Set up the game board with 0-6 facedown cards in each pile and one face-up card on top
            for (int i = 0; i < 7; i++)
            {
                FaceDownCardsInBoard[i] = i;
                char card = requestCard();
                Board[i] = new List<char> { card };
                CardsInPlay.Add(card);
            }
        }

        /// <summary>
        /// When the stockpile is empty, turn the waste pile over.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this is run without an empty stock pile
        /// </exception>
        public void ResetStockPile()
        {
            if (StockPile.Count > 0) { throw new InvalidOperationException("Stockpile is not empty."); }

            stockSeen = true;

            while (WastePile.Count > 0)
                StockPile.Push(WastePile.Pop());
        }

        /// <summary>
        /// Pulls a card from the stock pile and places it in the waste pile.
        /// </summary>
        /// <param name="requestCard">Delegate to draw a card from the stock pile.</param>
        /// <returns>
        /// If we haven't seen the whole stock pile, return true. If we have,
        /// return true if drawnCard matches what we got off the stack.
        /// </returns>
        /// <exception cref="InvalidOperationException">If the stock pile is empty.</exception>
        public bool DrawStockPile(RequestCard requestCard)
        {
            if (StockPile.Count == 0)
                throw new InvalidOperationException("Stockpile is empty.");

            char drawnCard = requestCard();
            char newCard = StockPile.Pop();
            WastePile.Push(drawnCard);

            return !stockSeen && drawnCard.Equals(newCard);
        }

        /// <summary>
        /// Removes a card from the waste pile.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">If the waste pile is empty.</exception>
        public char RemoveFromWastePile()
        {
            if (WastePile.Count == 0)
                throw new InvalidOperationException("Waste pile is empty");

            char card = WastePile.Pop();
            CardsInStock.Remove(card);

            return card;
        }

        /// <summary>
        /// Gets the minimum value of the cards atop the foundation piles.
        /// </summary>
        /// <returns>The minimum value.</returns>
        public int MinimumFoundationValue()
        {
            int blackMinimum = Math.Min(FoundationPile[Suit.Spades], FoundationPile[Suit.Clubs]);
            int redMinimum = Math.Min(FoundationPile[Suit.Diamonds], FoundationPile[Suit.Hearts]);

            return Math.Min(blackMinimum, redMinimum);
        }
    }
}
