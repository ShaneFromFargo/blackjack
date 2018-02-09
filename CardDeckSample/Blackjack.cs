using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDeckSample
{
    class Blackjack
    {
        public List<Card> DealerCards { get; set; } = new List<Card>();
        public List<Card> PlayerCards { get; set; } = new List<Card>();
        public Deck _Deck { get; private set; }
        public GameResult gameResult { get; set; }

        //NOT IMPLEMENTED YET!
        public int NumberOfPlayers { get; set; }

        public Blackjack()
        {
            var deck = Deck.CreateFullDeck();
            deck.Shuffle();
            _Deck = deck;
            IniitialDeal();
        }
        
        public int GetPlayerScore()
        {
            return GetScore(PlayerCards);
        }

        public int GetDealerOpenCard()
        {
            return DealerCards[0].GetBlackJackValue();
        }
        public int GetDealerScore()
        {
            return GetScore(DealerCards);
        }

        private int GetScore(List<Card> cards)
        {
            int score = 0;
            foreach (var card in cards)
            {
                score = score + card.GetBlackJackValue();
            }
            return score;
        }

        public void PlayerHit()
        {
            PlayerCards.Add(DealCard());
        }

        public void DealerFinish()
        {
            if(GetScore(DealerCards)<17)
            {
                DealerCards.Add(DealCard());
                if(GetScore(DealerCards) < 17)
                {
                    DealerFinish();
                }
            }
        }

        private void IniitialDeal()
        {

            PlayerCards.Add(DealCard());
            DealerCards.Add(DealCard());
            PlayerCards.Add(DealCard());
            DealerCards.Add(DealCard());
        }
        private Card DealCard()
        {
            Card card = _Deck.Cards[0];
            _Deck.Cards.Remove(card);
            return card;
        }
    }

    public enum GameResult
    {
        Push,
        PlayerWin,
        DealerWin
    }



}
