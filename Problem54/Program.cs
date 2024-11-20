using System.Text;

namespace Problem54
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                string[] inputs = Console.ReadLine().Split();
                StringBuilder firstHandInput = new StringBuilder();
                StringBuilder secondHandInput = new StringBuilder();
                for (Int32 j = 0; j < 5; j++)
                {
                    firstHandInput.Append(inputs[j]);
                    secondHandInput.Append(inputs[5 + j]);
                }
                Hand firstHand = new Hand(firstHandInput.ToString());
                Hand secondHand = new Hand(secondHandInput.ToString());
                Console.WriteLine(Hand.Compare(firstHand, secondHand));
            }
        }
    }
    internal class Hand
    {
        Card[] cards;
        Int32[] valueHistogram;
        public Hand(string input)
        {
            List<Card> cardsTemp = new List<Card>();
            for(Int32 i = 0; i < 5; i++)
            {
                cardsTemp.Add(new Card(input[2 * i], input[2 * i + 1]));
            }
            cardsTemp.Sort((x, y) => x.value.CompareTo(y.value));
            cards = cardsTemp.ToArray();
            valueHistogram = new Int32[15];
            foreach (var c in cards)
            {
                valueHistogram[c.value]++;
            }
        }
        private bool IsSraightFlush()
        {
            if (IsStraight() && IsFlush())
            {
                return true;
            }
            return false;
        }
        private bool IsWheel()
        {
            if (cards[0].value==2 && cards[1].value == 3 && cards[2].value == 4 && cards[3].value == 5 && cards[4].value == 14)
            {
                return true;
            }
            return false;
        }
        private bool IsStraight()
        {
            if (IsWheel())
            {
                return true;
            }
            for(Int32 i = 1; i < 5; i++)
            {
                if (cards[i].value != cards[i - 1].value + 1)
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsFlush()
        {
            char c = cards[0].suit;
            for(Int32 i = 1; i < 5; i++)
            {
                if (cards[i].suit != c)
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsFourOfAKind()
        {
            foreach(var v in valueHistogram)
            {
                if (v == 4)
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsThreeOfAKind()
        {
            foreach (var v in valueHistogram)
            {
                if (v == 3)
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsTwoPair()
        {
            List<Int32> pairs = new List<Int32>();
            for(Int32 i = 0; i <= 14; i++)
            {
                if (valueHistogram[i] == 2)
                {
                    pairs.Add(i);
                }
            }
            if (pairs.Count == 2)
            {
                return true;
            }
            return false;
        }
        private static (Int32, Int32, Int32) GetTwoPairValues(Hand hand)
        {
            List<Int32> pairs = new List<Int32>();
            for (Int32 i = 0; i <= 14; i++)
            {
                if (hand.valueHistogram[i] == 2)
                {
                    pairs.Add(i);
                }
            }
            pairs.Sort();
            pairs.Reverse();
            Int32 kicker = 0;
            for(Int32 i = 0; i < 5; i++)
            {
                Int32 nextValue = hand.cards[i].value;
                if (!pairs.Contains(nextValue))
                {
                    kicker = nextValue;
                    break;
                }
            }
            return (pairs[0], pairs[1], kicker);
        }
        private static (Int32,List<Int32>) GetOnePairValues(Hand hand)
        {
            Int32 pair = 0;
            List<Int32> otherCards=new List<Int32>();
            for (Int32 i = 0; i <= 14; i++)
            {
                if (hand.valueHistogram[i] == 2)
                {
                    pair = i;
                }
            }
            for(Int32 i = 0; i < 5; i++)
            {
                Int32 cardValue = hand.cards[i].value;
                if (cardValue != pair)
                {
                    otherCards.Add(cardValue);
                }
            }
            otherCards.Sort();
            otherCards.Reverse();
            return (pair, otherCards);
        }
        private bool IsPair()
        {
            foreach (var v in valueHistogram)
            {
                if (v == 2)
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsFullHouse()
        {
            if (IsPair() && IsThreeOfAKind())
            {
                return true;
            }
            return false;
        }
        public static string Compare(Hand firstHand,Hand secondHand)
        {
            string player1wins = "Player 1";
            string player2wins = "Player 2";
            bool firstPlayerStraightFlush = firstHand.IsSraightFlush();
            bool secondPlayerStraightFlush = secondHand.IsSraightFlush();
            if (firstPlayerStraightFlush)
            {
                if (!secondPlayerStraightFlush)
                {
                    return player1wins;
                }
                if (firstHand.cards[3].value > secondHand.cards[3].value)
                {
                    return player1wins;
                }
                return player2wins;
            }
            if (secondPlayerStraightFlush)
            {
                return player2wins;
            }
            bool firstPlayerFourOfAKind = firstHand.IsFourOfAKind();
            bool secondPlayerFourOfAKind = secondHand.IsFourOfAKind();
            if (firstPlayerFourOfAKind)
            {
                if (!secondPlayerFourOfAKind)
                {
                    return player1wins;
                }
                Int32 firstHandValue = firstHand.cards[2].value;
                Int32 secondHandValue = secondHand.cards[2].value;
                if (firstHandValue > secondHandValue)
                {
                    return player1wins;
                }
                return player2wins;
            }
            if (secondPlayerFourOfAKind)
            {
                return player2wins;
            }
            bool firstPlayerFullHouse = firstHand.IsFullHouse();
            bool secondPlayerFullHouse = secondHand.IsFullHouse();
            if (firstPlayerFullHouse)
            {
                if (!secondPlayerFullHouse)
                {
                    return player1wins;
                }
                Int32 firstHandValue = firstHand.cards[2].value;
                Int32 secondHandValue = secondHand.cards[2].value;
                if (firstHandValue > secondHandValue)
                {
                    return player1wins;
                }
                return player2wins;
            }
            if (secondPlayerFullHouse)
            {
                return player2wins;
            }
            bool firstPlayerFlush = firstHand.IsFlush();
            bool secondPlayerFlush = secondHand.IsFlush();
            if (firstPlayerFlush)
            {
                if (!secondPlayerFlush)
                {
                    return player1wins;
                }
                for(Int32 i = 4; i >= 0; i--)
                {
                    Int32 firstHandValue = firstHand.cards[i].value;
                    Int32 secondHandValue = secondHand.cards[i].value;
                    if (firstHandValue > secondHandValue)
                    {
                        return player1wins;
                    }
                    if (secondHandValue > firstHandValue)
                    {
                        return player2wins;
                    }
                }
                return player2wins;
            }
            if (secondPlayerFlush)
            {
                return player2wins;
            }
            bool firstPlayerStraight = firstHand.IsStraight();
            bool secondPlayerStraight = secondHand.IsStraight();
            if (firstPlayerStraight)
            {
                if (!secondPlayerStraight)
                {
                    return player1wins;
                }
                Int32 firstHandValue = firstHand.cards[3].value;
                Int32 secondHandValue = secondHand.cards[3].value;
                if (firstHandValue > secondHandValue)
                {
                    return player1wins;
                }
                return player2wins;
            }
            if (secondPlayerStraight)
            {
                return player2wins;
            }
            bool firstPlayerThreeOfAKind = firstHand.IsThreeOfAKind();
            bool secondPlayerThreeOfAKind = secondHand.IsThreeOfAKind();
            if (firstPlayerThreeOfAKind)
            {
                if (!secondPlayerThreeOfAKind)
                {
                    return player1wins;
                }
                Int32 firstHandValue = firstHand.cards[2].value;
                Int32 secondHandValue = secondHand.cards[2].value;
                if (firstHandValue > secondHandValue)
                {
                    return player1wins;
                }
                return player2wins;
            }
            if (secondPlayerThreeOfAKind)
            {
                return player2wins;
            }
            bool firstPlayerTwoPair = firstHand.IsTwoPair();
            bool secondPlayerTwoPair = secondHand.IsTwoPair();
            if (firstPlayerTwoPair)
            {
                if (!secondPlayerTwoPair)
                {
                    return player1wins;
                }
                (Int32, Int32, Int32) firstPlayerValue = Hand.GetTwoPairValues(firstHand);
                (Int32, Int32, Int32) secondPlayerValue = Hand.GetTwoPairValues(secondHand);
                if (firstPlayerValue.Item1 > secondPlayerValue.Item1)
                {
                    return player1wins;
                }
                if (secondPlayerValue.Item1 > firstPlayerValue.Item1)
                {
                    return player2wins;
                }
                if (firstPlayerValue.Item2 > secondPlayerValue.Item2)
                {
                    return player1wins;
                }
                if (secondPlayerValue.Item2 > firstPlayerValue.Item2)
                {
                    return player2wins;
                }
                if (firstPlayerValue.Item3 > secondPlayerValue.Item3)
                {
                    return player1wins;
                }
                return player2wins;
            }
            if (secondPlayerTwoPair)
            {
                return player2wins;
            }
            bool firstPlayerPair = firstHand.IsPair();
            bool secondPlayerPair = secondHand.IsPair();
            if (firstPlayerPair)
            {
                if (!secondPlayerPair)
                {
                    return player1wins;
                }
                (Int32, List<Int32>) firstPlayerValue = Hand.GetOnePairValues(firstHand);
                (Int32, List<Int32>) secondPlayerValue = Hand.GetOnePairValues(secondHand);
                if (firstPlayerValue.Item1 > secondPlayerValue.Item1)
                {
                    return player1wins;
                }
                if (secondPlayerValue.Item1 > firstPlayerValue.Item1)
                {
                    return player2wins;
                }
                for(Int32 i = 0; i < 4; i++)
                {
                    Int32 firstPlayerKicker = firstPlayerValue.Item2[i];
                    Int32 secondPlayerKicker = secondPlayerValue.Item2[i];
                    if (firstPlayerKicker > secondPlayerKicker)
                    {
                        return player1wins;
                    }
                    if (secondPlayerKicker > firstPlayerKicker)
                    {
                        return player2wins;
                    }
                }
                return player2wins;
            }
            if (secondPlayerPair)
            {
                return player2wins;
            }
            for(Int32 i = 0; i < 5; i++)
            {
                Int32 firstPlayerHighCard = firstHand.cards[4 - i].value;
                Int32 secondPlayerHighCard = secondHand.cards[4 - i].value;
                if (firstPlayerHighCard > secondPlayerHighCard)
                {
                    return player1wins;
                }
                if (secondPlayerHighCard > firstPlayerHighCard)
                {
                    return player2wins;
                }
            }
            return "Hello World!";
        }
    }
    internal class Card 
    {
        public readonly Int32 value;
        public readonly char suit;
        public Card(char inputValue,char inputSuit)
        {
            this.suit = inputSuit;
            switch (inputValue)
            {
                case '2':
                    value = 2;
                    break;
                case '3':
                    value = 3;
                    break;
                case '4':
                    value = 4;
                    break;
                case '5':
                    value = 5;
                    break;
                case '6':
                    value = 6;
                    break;
                case '7':
                    value = 7;
                    break;
                case '8':
                    value = 8;
                    break;
                case '9':
                    value = 9;
                    break;
                case 'T':
                    value = 10;
                    break;
                case 'J':
                    value = 11;
                    break;
                case 'Q':
                    value = 12;
                    break;
                case 'K':
                    value = 13;
                    break;
                case 'A':
                    value = 14;
                    break;
            }
        }
    }
}