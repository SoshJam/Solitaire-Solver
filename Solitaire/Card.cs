namespace Solitaire
{
    public class Card
    {
        public readonly Suit suit;
        public readonly int value;

        public Card(Suit suit, int value)
        {
            this.suit = suit;
            this.value = value;
        }

        /// <summary>
        /// Converts the card to a string.
        /// </summary>
        /// <returns>The name of the card.</returns>
        public override string ToString()
        {
            switch (value)
            {
                case 1:
                    return $"Ace of {suit}";
                case 11:
                    return $"Jack of {suit}";
                case 12:
                    return $"Queen of {suit}";
                case 13:
                    return $"King of {suit}";
                default:
                    return $"{value} of {suit}";
            }
        }

        /// <summary>
        /// If the card is a Spade or a Club.
        /// </summary>
        /// <returns>True if the card is black.</returns>
        public bool IsBlack()
        {
            return suit == Suit.Spades || suit == Suit.Clubs;
        }
    }

    public enum Suit
    {
        Spades,
        Diamonds,
        Clubs,
        Hearts
    }
}