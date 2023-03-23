using System.Text.RegularExpressions;

namespace Solitaire
{
    public class Card
    {
        public readonly Suit suit;
        public readonly int value;

        public Card(Suit suit, int value)
        {
            this.suit = suit;

            if (value < 1 || value > 13)
                throw new ArgumentException("Card value must be between 1 and 13.");
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
        /// Two cards are considered equal if they have the same suit.
        /// and value.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the cards are equal.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is Card)) return false;

            if (((Card)obj).suit != this.suit) return false;
            if (((Card)obj).value != this.value) return false;

            return true;
        }

        /// <summary>
        /// Converts the string to a hash code.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return suit.GetHashCode() * value.GetHashCode();
        }

        /// <summary>
        /// If the card is a Spade or a Club.
        /// </summary>
        /// <returns>True if the card is black.</returns>
        public bool IsBlack()
        {
            return suit == Suit.Spades || suit == Suit.Clubs;
        }

        /// <summary>
        /// Creates a card from a string. The string must be two characters long,
        /// with the first representing the suit and the second represents the 
        /// value. Value will be 1-9, or 0 representing a 10, a representing an
        /// ace, j for jack, q for queen, or k for king.
        /// </summary>
        /// <param name="s">The input string</param>
        /// <returns>A new Card created from the input string.</returns>
        public static Card FromString(string s)
        {
            s = s.ToLower();

            if (!Regex.IsMatch(s, @"^[sdch][\dajqk]$"))
                throw new ArgumentException("Invalid input.");

            Suit suit;
            int value;

            switch (s[0])
            {
                case 's':
                default:
                    suit = Suit.Spades;
                    break;
                case 'd':
                    suit = Suit.Diamonds;
                    break;
                case 'c':
                    suit = Suit.Clubs;
                    break;
                case 'h':
                    suit = Suit.Hearts;
                    break;
            }

            switch (s[1])
            {
                case 'a':
                    value = 1;
                    break;
                case '0':
                    value = 10;
                    break;
                case 'j':
                    value = 11;
                    break;
                case 'q':
                    value = 12;
                    break;
                case 'k':
                    value = 13;
                    break;
                default:
                    value = int.Parse($"{s[1]}");
                    break;
            }

            return new Card(suit, value);
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