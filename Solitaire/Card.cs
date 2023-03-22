namespace Solitaire
{
	public struct Card
	{
		public Suit suit { get; private set; }
		public int value { get; private set; }

		public bool IsBlack
		{
			get { return suit == Suit.Spades || suit == Suit.Clubs; }
			private set { IsBlack = value; } // Updating this will not do anything.
		}

		public Card(Suit suit, int value)
		{
			this.suit = suit;
			this.value = value;
		}

		public override string ToString()
		{
			string valueString = "";
			switch (value) {
				case 1:
					valueString = "Ace";
					break;
                case 11:
                    valueString = "Jack";
                    break;
                case 12:
                    valueString = "Queen";
                    break;
                case 13:
                    valueString = "King";
                    break;
				default:
					valueString = value.ToString();
					break;
            }

			return $"{valueString} of {suit.ToString()}";
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