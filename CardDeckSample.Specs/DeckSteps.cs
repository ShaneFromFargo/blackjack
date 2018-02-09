using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace CardDeckSample.Specs
{
    /// <summary>
    /// BDD Cucumber-style tests.  These methods correspond to the contents of Deck.feature.
    /// </summary>
    [Binding]
    public class DeckSteps
    {
        private Deck deck;

        [Given(@"An unshuffled deck")]
        public void GivenAnUnshuffledDeck()
        {
            deck = Deck.CreateFullDeck();
        }

        [When(@"the deck is shuffled")]
        public void WhenTheDeckIsShuffled()
        {
            deck.Shuffle();
        }

        [When(@"the deck is sorted")]
        public void WhenTheDeckIsSorted()
        {
            deck.SortAscending();
        }

        [Then(@"the deck should have (.*) cards")]
        public void ThenTheDeckShouldHaveCards(int totalCards)
        {
            Assert.AreEqual(52, deck.Cards.Count);
        }

        [Then(@"the deck should have (.*) suits")]
        public void ThenTheDeckShouldHaveSuits(int suitCount)
        {
            var suits = new HashSet<Suit>();
            foreach (var card in deck.Cards)
            {
                suits.Add(card.Suit);
            }
            Assert.AreEqual(suitCount, suits.Count);
        }

        [Then(@"the deck should have (.*) card numbers")]
        public void ThenTheDeckShouldHaveCardNumbers(int cardNumberCount)
        {
            var cardNumbers = new HashSet<CardNumber>();
            foreach (var card in deck.Cards)
            {
                cardNumbers.Add(card.Number);
            }
            Assert.AreEqual(cardNumberCount, cardNumbers.Count);
        }

        [Then(@"the deck should not have duplicate cards")]
        public void ThenTheDeckShouldNotHaveDuplicateCards()
        {
            var uniqueCards = new HashSet<Card>();
            foreach (var card in deck.Cards)
            {
                Assert.IsFalse(uniqueCards.Contains(card));
                uniqueCards.Add(card);
            }
            Assert.AreEqual(52, uniqueCards.Count);
        }

        [Then(@"the cards are in ascending order")]
        public void ThenTheCardsAreInAscendingOrder()
        {
            Card currentCard = null;
            foreach (var card in deck.Cards)
            {
                if (currentCard != null)
                {
                    Assert.IsTrue(card.CompareTo(currentCard) > 0);
                }
                currentCard = card;
            }
        }

        [Then(@"shuffling several decks produces different results")]
        public void ThenShufflingSeveralDecksProducesDifferentResults()
        {
            var deck1 = Deck.CreateFullDeck();
            var deck2 = Deck.CreateFullDeck();

            deck1.Shuffle();
            deck2.Shuffle();

            Assert.IsTrue(AreDecksDifferent(deck1, deck2));
        }

        private bool AreDecksDifferent(Deck deck1, Deck deck2)
        {
            for (int i = 0; i < deck1.Cards.Count; ++i)
            {
                if ((deck1.Cards[i].Number != deck2.Cards[i].Number) ||
                    (deck1.Cards[i].Suit != deck2.Cards[i].Suit))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
