namespace Solitaire
{
    public class GameState
    {
        /// <summary>
        /// The cards that are known to be face-up and in play, either on the
        /// board or in the foundation piles.
        /// </summary>
        public readonly HashSet<Card> CardsInPlay;

        /// <summary>
        /// The cards that are known to be in either the stock pile or the waste
        /// pile
        /// </summary>
        public readonly HashSet<Card> CardsInStock;

        /// <summary>
        /// The facedown cards in the Stock Pile. If a card is null it means
        /// we have not been through the pile yet.
        /// </summary>
        public readonly Stack<Card> StockPile;

        /// <summary>
        /// The faceup cards in the Waste Pile. When the StockPile is empty
        /// and reset, everything gets popped from this and pushed to the StockPile.
        /// </summary>
        public readonly Stack<Card> WastePile;

        private bool stockSeen; // If we have seen everything in the stockpile

        /// <summary>
        /// The faceup cards in the main game board. Has seven piles of cards.
        /// 
        /// These have to be lists because we can move any part of a stack.
        /// </summary>
        public readonly List<Card>[] Board;

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
        private Func<Card> RequestCard;

        /// <summary>
        /// Tracks a new solitaire game.
        /// </summary>
        /// <param name="RequestCard">A function to get a new facedown card.</param>
        /// <param name="topCards">The cards atop each pile on the board.</param>
        public GameState(Func<Card> RequestCard, IEnumerable<Card> topCards)
        {
            // TODO: Constructor
            throw new NotImplementedException();
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
        /// <param name="drawnCard">The card that was drawn.</param>
        /// <returns>
        /// If we haven't seen the whole stock pile, return true. If we have,
        /// return true if drawnCard matches what we got off the linked list.
        /// </returns>
        /// <exception cref="InvalidOperationException">If the stock pile is empty.</exception>
        public bool DrawStockPile(Card drawnCard)
        {
            if (StockPile.Count == 0)
                throw new InvalidOperationException("Stockpile is empty.");

            Card newCard = StockPile.Pop();
            WastePile.Push(drawnCard);

            return !stockSeen || drawnCard.Equals(newCard);
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

        /// <summary>
        /// Adds a card to the top of its requested foundation pile.
        /// </summary>
        /// <param name="card"></param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the card cannot be added to this pile.
        /// </exception>
        public void AddToFoundation(Card card)
        {
            int top = FoundationPile[card.suit];

            if (top + 1 == card.value)
                FoundationPile[card.suit] = card.value;
            else
                throw new InvalidOperationException("Cannot add this card to the pile.");
        }

        /// <summary>
        /// Moves a stack of cards from one pile to another.
        /// </summary>
        /// <param name="startPos">The pile the stack is in.</param>
        /// <param name="bottomPos">The distance from the bottom of the stack to the bottom of its pile.</param>
        /// <param name="endPos">The pile the stack will end up in.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the card cannot be moved for whatever reason.
        /// </exception>
        public void MoveCards(int startPos, int bottomPos, int endPos)
        {
            // Ensure the move is valid

            // Get a list of the cards that will be moved

            // Add all cards to the new pile

            // Remove all cards from the old pile

            // Flip over a face-down card if necessary
        }

        /// <summary>
        /// Reveal a card atop a pile in the board.
        /// </summary>
        /// <param name="pile">The pile </param>
        /// <param name="card"></param>
        /// <returns>true if the card was not already seen in the game.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if there are any face-up cards in that pile.
        /// </exception>
        public bool RevealBoardCard(int pile, Card card)
        {
            if (Board[pile].Count > 0)
                throw new InvalidOperationException("Pile is not empty.");

            bool seen = CardsInPlay.Contains(card) || CardsInStock.Contains(card);
            CardsInPlay.Add(card);
            Board[pile].Add(card);

            return !seen;
        }

        /// <summary>
        /// Gets the top card of every pile of the board.
        /// </summary>
        /// <remarks>Useful when calculating moves.</remarks>
        /// <returns>An array of 7 cards.</returns>
        public Card[] GetBoardPileTops()
        {
            Card[] tops = new Card[7];
            for (int i = 0; i < 7; i++)
            {
                tops[i] = Board[i].Last();
            }
            return tops;
        }

        /// <summary>
        /// Gets the lowest face-up card of every pile of the board.
        /// </summary>
        /// <remarks>Useful when calculating moves.</remarks>
        /// <returns>An array of 7 cards.</returns>
        public Card[] GetBoardPileBottoms()
        {
            Card[] bottoms = new Card[7];
            for (int i = 0; i < 7; i++)
            {
                bottoms[i] = Board[i].First();
            }
            return bottoms;
        }
    }
}