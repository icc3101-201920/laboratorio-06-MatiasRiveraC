﻿using Laboratorio_5_OOP_201902.Cards;
using Laboratorio_5_OOP_201902.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Laboratorio_5_OOP_201902
{
    public class Game
    {
        //Atributos
        private Player[] players;
        private Player activePlayer;
        private List<Deck> decks;
        private List<SpecialCard> captains;
        private Board boardGame;
        private int turn;

        //Constructor
        public Game()
        {
            Random rnd = new Random();
            players = new Player[]{ new Player(), new Player()};
            activePlayer = players[rnd.Next(0,players.Length)];
            boardGame = new Board();
            decks = new List<Deck>();
            captains = new List<SpecialCard>();
			AddDecks();
			AddCaptains();
			foreach (Player player in players)
            {
                player.Board = boardGame;
            }
            
            turn = 0;
        }
        //Propiedades
        public Player[] Players
        {
            get
            {
                return this.players;
            }
        }
        public Player ActivePlayer
        {
            get
            {
                return this.activePlayer;
            }
            set
            {
                activePlayer = value;
            }
        }
        public List<Deck> Decks
        {
            get
            {
                return this.decks;
            }
        }
        public List<SpecialCard> Captains
        {
            get
            {
                return this.captains;
            }
        }
        public Board BoardGame
        {
            get
            {
                return this.boardGame;
            }
        }

        //Metodos
        public bool CheckIfEndGame()
        {
            if (players[0].LifePoints == 0 || players[1].LifePoints == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetWinner()
        {
            if (players[0].LifePoints == 0 && players[1].LifePoints > 0)
            {
                return 1;
            }
            else if (players[1].LifePoints == 0 && players[0].LifePoints > 0)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        public void Play()
        {
            foreach (Player player in players)
            {
                Visualization.ShowProgramMessage("Player "+Convert.ToString(player.Id) + " select Deck and Captains");

                Visualization.ShowDecks(decks);
				int choiceDeck = Visualization.GetUserInput(decks.Count - 1);
				Deck deckChoice = decks[choiceDeck];
				player.Deck = deckChoice;
				player.FirstHand();
				Visualization.ShowCaptains(captains);
				int choiceCaptain = Visualization.GetUserInput(captains.Count - 1);
				player.ChooseCaptainCard(captains[choiceCaptain]);


				Visualization.ShowListOptions(new List<string>() {"Change Card", "Pass" }, "Change 3 cards or ready to play:");
				int choice = Visualization.GetUserInput(1);
				int cont = 0;
				int choiceCard = -1;
				if (choice == 0)
				{
					while (true)
					{
						if (cont != 3)
						{
							Visualization.ShowHand(player.Hand);
							Visualization.ShowProgramMessage("Input the numbers  of the cards to change (max 3). To stop enter -1");
							choiceCard = Visualization.GetUserInput(player.Hand.Cards.Count - 1, true);
						}
						if (choiceCard == -1 || cont ==3)
						{
							break;
						}
						else
						{
							player.ChangeCard(choiceCard);
							cont++;
						}
					}
				}
				Visualization.ClearConsole();
				


            }
        }
        public void AddDecks()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + @"\Files\Decks.txt";
            StreamReader reader = new StreamReader(path);
            int deckCounter = 0;
            List<Card> cards = new List<Card>();


            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string [] cardDetails = line.Split(",");

                if (cardDetails[0] == "END")
                {
                    decks[deckCounter].Cards = new List<Card>(cards);
                    deckCounter += 1;
                }
                else
                {
                    if (cardDetails[0] != "START")
                    {
                        if (cardDetails[0] == nameof(CombatCard))
                        {
                            cards.Add(new CombatCard(cardDetails[1], (EnumType) Enum.Parse(typeof(EnumType),cardDetails[2]), cardDetails[3], Int32.Parse(cardDetails[4]), bool.Parse(cardDetails[5])));
                        }
                        else
                        {
                            cards.Add(new SpecialCard(cardDetails[1], (EnumType)Enum.Parse(typeof(EnumType), cardDetails[2]), cardDetails[3]));
                        }
                    }
                    else
                    {
                        decks.Add(new Deck());
                        cards = new List<Card>();
                    }
                }

            }
            
        }
        public void AddCaptains()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + @"\Files\Captains.txt";
            StreamReader reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] cardDetails = line.Split(",");
                captains.Add(new SpecialCard(cardDetails[1], (EnumType)Enum.Parse(typeof(EnumType), cardDetails[2]), cardDetails[3]));
            }
        }
    }
}
