using System.Linq;

namespace Solitaire
{
    public class GameState
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
        public GameState(RequestCard requestCard)
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

        /// <summary>
        /// Adds a card to the top of its requested foundation pile.
        /// </summary>
        /// <param name="card">The card to add</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the card cannot be added to this pile.
        /// </exception>
        public void AddToFoundation(char card)
        {
            int top = FoundationPile[Card.GetSuit(card)];

            if (top + 1 == Card.GetValue(card))
                FoundationPile[Card.GetSuit(card)] = Card.GetValue(card);
            else
                throw new InvalidOperationException("Cannot add this card to the pile.");

            CardsInPlay.Add(card);
        }

        /// <summary>
        /// Removes a card from the top of a foundation pile
        /// </summary>
        /// <param name="suit">The suit of the foundation pile to remove</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the card cannot be removed from this pile.
        /// </exception>
        public char RemoveFromFoundation(Suit suit)
        {
            if (FoundationPile[suit] == 0)
                throw new InvalidOperationException("This pile is empty.");

            char card = Card.FromSuitAndValue(suit, FoundationPile[suit]--);
            CardsInPlay.Remove(card);
            return card;
        }

        /// <summary>
        /// Moves a stack of cards from one pile to another.
        /// </summary>
        /// <param name="start">The pile the stack is in.</param>
        /// <param name="offset">The distance from the bottom of the stack to the bottom of its pile.</param>
        /// <param name="end">The pile the stack will end up in.</param>
        /// <param name="requestCard">A function to get a new card if the move would reveal one.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the card cannot be moved for whatever reason.
        /// </exception>
        public void MoveCards(int start, int offset, int end, RequestCard requestCard)
        {
            // Ensure the move is valid

            if (Board[start].Count == 0)
                throw new ArgumentException("Start pile has no cards.");
            if (Board[start].Count < offset)
                throw new ArgumentException($"Start pile has less than {offset} cards.");

            char bottom = Board[start][offset];

            if (Board[end].Count == 0 && (Card.GetValue(bottom) != 13 || FaceDownCardsInBoard[end] != 0))
                throw new ArgumentException("Target pile is not empty, or the moving card is not a king.");

            char top = Board[end].Last();

            if (Card.IsBlack(top) == Card.IsBlack(bottom))
                throw new ArgumentException("Cards must alternate color.");

            if (Card.GetValue(top) - 1 != Card.GetValue(bottom))
                throw new ArgumentException("Card value must decrease.");

            // Get a list of the cards that will be moved

            List<char> movingCards = new List<char>();
            for (int i = offset; i < Board[start].Count; i++)
                movingCards.Add(Board[start][i]);

            if (movingCards[0] != bottom)
                throw new InvalidOperationException("Something went wrong counting cards.");

            // Add all cards to the new pile and remove from the old

            foreach (char c in movingCards)
            {
                Board[start].Remove(c);
                Board[end].Add(c);
            }

            // Flip over a face-down card if necessary

            if (offset == 0)
                RevealBoardCard(start, requestCard);
        }

        /// <summary>
        /// Reveal a card atop a pile in the board.
        /// </summary>
        /// <param name="pile">The pile to reveal a card from</param>
        /// <param name="requestCard">Delegate to get a card</param>
        /// <returns>true if the card was not already seen in the game.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if there are any face-up cards in that pile.
        /// </exception>
        private bool RevealBoardCard(int pile, RequestCard requestCard)
        {
            if (Board[pile].Count > 0)
                throw new InvalidOperationException("Pile is not empty.");

            if (FaceDownCardsInBoard[pile] == 0)
                throw new InvalidOperationException("No cards to reveal.");

            char card = requestCard();
            bool seen = CardsInPlay.Contains(card) || CardsInStock.Contains(card);
            CardsInPlay.Add(card);
            Board[pile].Add(card);

            FaceDownCardsInBoard[pile]--;

            return !seen;
        }

        /// <summary>
        /// Adds the card to the board
        /// </summary>
        /// <param name="pile">The pile to add the card into</param>
        /// <param name="card">The card to add</param>
        public void AddToBoard(int pile, char card)
        {
            if (Board[pile].Count == 0 && Card.GetValue(card) != 13)
                throw new InvalidOperationException("This card cannot be added to an empty stack.");

            if (Board[pile].Count > 0 && Card.IsBlack(Board[pile].Last()) == Card.IsBlack(card))
                throw new InvalidOperationException("Cards must alternate color.");

            if (Board[pile].Count > 0 && Card.GetValue(Board[pile].Last()) - 1 != Card.GetValue(card))
                throw new InvalidOperationException("Card value must decrease.");

            Board[pile].Add(card);
            CardsInPlay.Add(card);
        }

        /// <summary>
        /// Removes the top card of a pile on the board
        /// </summary>
        /// <param name="pile">The pile to take the card from</param>
        /// <param name="requestCard">If a card needs to be revealed, get that card.</param>
        /// <returns>The card that was removed.</returns>
        public char RemoveFromBoard(int pile, RequestCard requestCard)
        {
            if (Board[pile].Count == 0)
                throw new InvalidOperationException("Pile is empty.");

            char card = Board[pile].Last();
            CardsInPlay.Remove(card);
            Board[pile].Remove(card);

            if (Board[pile].Count == 0 && FaceDownCardsInBoard[pile] > 0)
                RevealBoardCard(pile, requestCard);

            return card;
        }

        /// <summary>
        /// Gets the top card of every pile of the board.
        /// </summary>
        /// <remarks>Useful when calculating moves.</remarks>
        /// <returns>An array of 7 cards.</returns>
        public char[] GetBoardPileTops()
        {
            char[] tops = new char[7];
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
        public char[] GetBoardPileBottoms()
        {
            char[] bottoms = new char[7];
            for (int i = 0; i < 7; i++)
            {
                bottoms[i] = Board[i].First();
            }
            return bottoms;
        }
    }
}
