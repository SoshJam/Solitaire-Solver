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
        /// The top card of each foundation pile.
        /// 
        /// The items in the array are ordered by suit. Spades, Diamonds, Clubs,
        /// Hearts.
        /// 
        /// If an entry is null, there is nothing in the pile.
        /// </summary>
        public readonly Card?[] FoundationPile;

        /// <summary>
        /// Tracks a new solitaire game, taking in the top cards of each pile.
        /// </summary>
        /// <param name=""></param>
        public GameState(IEnumerable<Card> stackTopCards)
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
        public bool DrawStockPile(Card drawnCard)
        {
            // TODO: DrawStockPile
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the minimum value of the foundation pile. We should not add
        /// a card to the foundation unless the target card is one above this
        /// value. (All piles are maxed.)
        /// </summary>
        /// <returns></returns>
        public int MinimumFoundationValue()
        {
            // TODO: MinimumFoundationValue
            throw new NotImplementedException();
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
            Card? topCard = FoundationPile[(int)card.suit];
            int topValue = topCard is null ? 0 : ((Card)topCard).value;

            if (topValue + 1 != card.value)
                FoundationPile[(int)card.suit] = card;
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
            // TODO: MoveCards
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the top card of every pile of the board.
        /// </summary>
        /// <remarks>Useful when calculating moves.</remarks>
        /// <returns>An array of 7 cards.</returns>
        public Card[] GetBoardPileTops()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the lowest face-up card of every pile of the board.
        /// </summary>
        /// <remarks>Useful when calculating moves.</remarks>
        /// <returns>An array of 7 cards.</returns>
        public Card[] GetBoardPileBottoms()
        {
            throw new NotImplementedException();
        }
    }
}