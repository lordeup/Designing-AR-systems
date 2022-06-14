using System.Collections.Generic;
using System.Linq;
using Card.Type;

namespace Tracking.NumberCard
{
    public class NumberCardRecognizer : Recognizer<NumberCardType>
    {
        private readonly List<NumberCardType> _cards = new();

        public List<NumberCardType> GetCards()
        {
            return _cards;
        }
        
        public int GetCountCards()
        {
            return _cards.Count;
        }

        public void AddCard(NumberCardType type)
        {
            _cards.Add(type);
        }

        public void ClearCards()
        {
            _cards.Clear();
        }

        public int GetAmountCards(int count)
        {
            return _cards.Take(count).Sum(card => (int)card);
        }
    }
}
