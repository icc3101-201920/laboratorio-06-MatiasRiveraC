using Laboratorio_5_OOP_201902.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio_5_OOP_201902
{
    static class Visualization
    {
        /*Este debe mostrar la mano del jugador y el id de cada carta. 
        Además las cartas de combate deben aparecer en rojo y las especiales en azul.*/
        public static void ShowHand(Hand hand)
        {
            int cont = 0;
            List<Card> cards = hand.Cards;
            foreach(Card card in cards)
            {
                if (0 < (int)card.Type && (int)card.Type < 4)
                {
                    CombatCard Ccard = (CombatCard)card;
                    Console.ForegroundColor = ConsoleColor.Red ;
                    Console.WriteLine("|({0}) {1} ({2}): {3} |",cont, card.Name,card.Type,Ccard.AttackPoints);
                    Console.ResetColor();
                }
                else if ((int)card.Type != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("|({0}) {1} ({2}) |", cont, card.Name, card.Type);
                    Console.ResetColor();
                }
                cont++;
            }
        }
        /*Debe mostrar de manera sencilla las opciones de mazos a elegir. */

        public static void ShowDecks(List<Deck> decks)
        {
            int cont = 0;
            foreach(Deck deck in decks)
            {
                Console.WriteLine("({0}) Deck {1}",cont,cont+1);
                cont++;
            }
        }

        /*Debe mostrar de manera sencialla las opciones de Capitanes a elegir. */

        public static void ShowCaptains(List<SpecialCard> captains)
        {
            int cont = 0;
            foreach(SpecialCard captain in captains)
            {
                Console.WriteLine("({0}) {1}: {2}", cont, captain.Name,captain.Effect);
                cont++;
            }
        }
        /*Debe obtener el input del usuario desde la consola. Como parámetro recibe el número máximo permitido
         * a ingresar y un flag llamado stopper. El método debe verificar que el usuario ingrese un número 
         * entre 0 y maxInput, y en caso de que stopper sea true, entonces entre -1 y maxInput. En caso de 
         * error debe llamar al metodo ConsoleError (a continuación) y mostrar un mensaje que permita al usuario 
         * saber cuál es el error. */

        public static int GetUserInput(int maxInput, bool stopper= false)
        {
            while(true)
            switch (stopper)
            {
                case true:
                    try
                    {
                        string str = Console.ReadLine();
						int value = Convert.ToInt32(str);
                        if (value<-1 || value > maxInput)
                        {
                            string error = ("The option ("+ Convert.ToString(value) + ") is not valid, try again");
                            ConsoleError(error);
                        }
                        else
                        {
                                return value;
                        }

                    }

                    catch
                    {
                        string error = ("Input must be a number, try again");
                        ConsoleError(error);
                    }
                    break;

                case false:
                    try
                    {
							string str = Console.ReadLine();
							int value = Convert.ToInt32(str);
							if (value < 0 || value > maxInput)
                        {
                            string error = ("The option (" + Convert.ToString(value) + ") is not valid, try again");
                            ConsoleError(error);
                        }
                            else
                            {
                                return value;
                            }

                        }
                    catch
                    {
                        string error = ("Input must be a number, try again");
                        ConsoleError(error);
                    }
                    break;
            }
           
        }

        /* Debe mostrar el mensaje proveniente en el parámetro. Este debe tener un fondo de color rojo. */

        public static void ConsoleError(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /*Debe mostrar el mensaje con color de texto verde.*/

        public static void ShowProgramMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /* Debe mostrar las opciones para el usuario que vienen en la lista options. 
         * Si message no es null, debe mostrar este antes de la lista de opciones.*/

        public static void ShowListOptions(List<string> options, string message = null)
        {
            int cont = 0;
            if (message != null)
            {
                Console.WriteLine(message);
            }
            foreach(string option in options)
            {
                Console.WriteLine("({0}) {1}",cont,option);
                cont++;
            }
        }

        /* Debe limpiar la consola. */

        public static void ClearConsole()
        {
            Console.Clear();
        }
    }
}
