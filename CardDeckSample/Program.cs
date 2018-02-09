using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDeckSample
{
    class Program
    {
        static void Main(string[] args)
        {

            // A deck could be created using Deck.CreateFullDeck() and shuffled,
            // sorted, etc...  The list of cards is exposed as a property.  A 'deal'
            // method could be added.
            bool gameActive = true;
            int gamesWon = 0;
            int gamesPlayed = 0;
            while (gameActive)
            {
                Blackjack blackjack = new Blackjack();
                Console.WriteLine("Here are your cards");
                //Display player cards
                foreach (var card in blackjack.PlayerCards)
                {
                    //Console.WriteLine(card.GetFriendlyCardName());
                }
                Console.WriteLine("Your score is: " + blackjack.GetPlayerScore());
                //Console.WriteLine("Here are the dealers cards");
                //Display Dealer cards
                foreach (var card in blackjack.DealerCards)
                {
                    //Console.WriteLine(card.GetFriendlyCardName());
                }
                Console.WriteLine("The dealer is showing: " + blackjack.GetDealerOpenCard());

                bool hitMe = true;
                while (hitMe == true && blackjack.GetPlayerScore() <= 21)
                {
                    Console.WriteLine("Would you like to hit[h] or stay[s]");
                    string playerDecision = Console.ReadLine();
                    if (playerDecision.ToLower() == "h" || playerDecision.ToLower() == "hit")
                    {
                        blackjack.PlayerHit();
                        hitMe = true;
                        foreach (var card in blackjack.PlayerCards)
                        {
                            Console.WriteLine(card.GetFriendlyCardName());
                        }
                        Console.WriteLine("Your score is: " + blackjack.GetPlayerScore());
                        if (blackjack.GetPlayerScore() > 21)
                        {
                            Console.WriteLine("YOU BUSTED!!! Game Over!");
                            break;
                        }
                    }
                    else
                    {
                        hitMe = false;
                        blackjack.DealerFinish();


                        if (blackjack.GetPlayerScore() > blackjack.GetDealerScore())
                        {
                            gamesWon++;
                            blackjack.gameResult = GameResult.PlayerWin;
                        }
                        else
                        {
                            
                            if (blackjack.GetPlayerScore() == blackjack.GetDealerScore())
                            {
                                blackjack.gameResult = GameResult.Push;
                            }
                            else if(blackjack.GetDealerScore()>21)
                            {
                                gamesWon++;
                                blackjack.gameResult = GameResult.PlayerWin;
                            }
                            else
                            {
                                blackjack.gameResult = GameResult.DealerWin;
                            }
                        }
                    }

                }
                DisplayGameEnd(blackjack);
                gamesPlayed++;
                Console.WriteLine("-----------------");
                Console.WriteLine("You have won " + gamesWon.ToString() + "/"+ gamesPlayed.ToString() + " games");
                Console.WriteLine("Play again?[y]");
               string playAgain =  Console.ReadLine();
                if(playAgain.ToLower()=="y" || playAgain.ToLower()=="yes")
                {
                    gameActive = true;
                    Console.Clear();
                }
                else
                {
                    gameActive = false;
                }
            }
           

        }
        public static void DisplayGameEnd(Blackjack blackjack)
        {
            Console.Clear();
            Console.WriteLine("GAME OVER");
            Console.WriteLine(blackjack.gameResult.ToString());
            Console.WriteLine("-----------------");
            Console.WriteLine("Dealers Score: " + blackjack.GetDealerScore());
            Console.WriteLine("Your Score: " + blackjack.GetPlayerScore());
            Console.WriteLine("-----------------");
            Console.WriteLine("Dealers Cards Below");
            Console.WriteLine("-----------------");

            foreach (var card in blackjack.DealerCards)
            {
                Console.WriteLine(card.Number + " of " + card.Suit);
            }
            Console.WriteLine("-----------------");
            Console.WriteLine("Players Cards Below");
            Console.WriteLine("-----------------");

            foreach (var card in blackjack.PlayerCards)
            {
                Console.WriteLine(card.Number + " of " + card.Suit);
            }
        }

    }


}
