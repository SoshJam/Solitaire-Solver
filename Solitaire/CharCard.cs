namespace Solitaire
{
    /// <summary>
    /// There are 52 cards in a deck, which is equivalent to the number of letters in two alphabets.
    /// Because of this, we can represent a card as a single character.
    /// A-M are spades, N-Z are clubs, a-m are hearts, and n-z are diamonds.
    /// (Normally it goes Spades - Hearts - Clubs - Diamonds, but this order makes certain operations easier.)
    /// This static class provides methods for interpreting characters as cards.
    /// </summary>
    public static class CharCard
    {
        public static Suit GetSuit(char input)
        {
            if (input >= 'n')
                return Suit.Diamonds;
            if (input >= 'a')
                return Suit.Hearts;
            if (input >= 'N')
                return Suit.Clubs;
            return Suit.Spades;
        }

        public static int GetValue(char input)
        {
            if (input >= 'n')
                return input - 'n' + 1;
            if (input >= 'a')
                return input - 'a' + 1;
            if (input >= 'N')
                return input - 'N' + 1;
            return input - 'A' + 1;
        }

        /// <summary>
        /// Converts the card to a string.
        /// </summary>
        /// <param name="input">The card to check.</param>
        /// <returns>The name of the card.</returns>
        public static string ToString(char input) {
            Suit suit = GetSuit(input);
            int value = GetValue(input);

            string card = "";
            switch (value) {
                case 1:
                    card = "A";
                    break;
                case 10:
                    card = "T"; // must be T because 10 is two characters
                    break;
                case 11:
                    card = "J";
                    break;
                case 12:
                    card = "Q";
                    break;
                case 13:
                    card = "K";
                    break;
                default:
                    card = value.ToString();
                    break;
            }

            card += "♠♥♣♦"[(int)suit];

            return card;
        }

        /// <summary>
        /// Returns true if the card is black.
        /// </summary>
        /// <param name="input">The card to check.</param>
        /// <returns>True if the card is black.</returns>
        public static bool IsBlack(char input) {
            return input <= 'Z';
        }
    }
}