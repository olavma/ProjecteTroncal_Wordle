/*
 * AUTHOR: Olav Martos Aceña
 * DATE: 29/11/2022
 * DESCRIPTION: Es tracta de fer una solució que ens permeti jugar al joc Wordle.
 *      Com encara estem als inicis del curs i gairebé no heu treballat interfícies d'usuari, el joc funcionarà a través de la terminal. 
 */

using System;

namespace Wordle
{
    class Wordle
    {
        static void Main(string[] args)
        {
            // Array de paraules
            string[] words = { "angel", "anima", "artic", "atoms", "audio", "camio", "cants", "civil", "decim", "delta", "digit", "dogma", "dolor", "gelat", "liceu", "licor", "local", "octal", "ocult", "orbes"};
            
            // Generem un numero aleatorio i guardem la paraula a la que correspon aquest numero per poder comparar.
            Random rnd = new Random();
            int rnum = rnd.Next(0, 1);
            string word = words[rnum];


            // Creem la matriu del wordle
            string[,] wordle = new string[5, 5];

            // La omplim d'x per a que tingui contingut
            for(int i = 0; i < wordle.GetLength(0); i++)
            {
                for(int j = 0; j < wordle.GetLength(1); j++)
                {
                    wordle[i, j] = "x";
                }
            }


            Console.ForegroundColor = ConsoleColor.White;
            // Comença la interaccio amb l'usuari
            for (int i = 0; i < wordle.GetLength(0); i++)
            {
                // Escriu la paraula
                Console.WriteLine("Escriu una paraula: ");
                string wordUser = Console.ReadLine();
                
                //Es comprova si compleix els requisits
                while(wordUser.Length > 5 || wordUser.Length < 0)
                {
                    Console.WriteLine("T'has equivocat amb el tamany de la paraula");
                    Console.WriteLine("Escriu una paraula: ");
                    wordUser = Console.ReadLine();
                }

                // Guarda el contingut en una linea de la matriu de Wordle
                for (int j = 0; j < wordle.GetLength(1); j++)
                {
                    wordle[i, j] = Convert.ToString(wordUser[j]);
                }

                // Contador per les lletres en verd
                int lettersGreen = 0;


                // Comencen les comprovacions
                for(int j = 0; j < wordle.GetLength(1); j++)
                {
                    // Si la lletra de la paraula de l'usuari coincideix amb la lletra de la mateixa posicio de la paraula secreta, es pintara de verd
                    if (Convert.ToChar(wordle[i, j]) == word[j])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" " + wordle[i, j]);
                        lettersGreen++;
                    }
                    // Si no coincideix es comprova si la lletra existeix en la paraula secreta. Si no existeix la pintara de Gris Fosc
                    else
                    {
                        for (int k = 0; k < wordle.GetLength(1); k++)
                        {
                            if (Convert.ToChar(wordle[i, j]) != word[k])
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                            }
                        }
                        Console.Write(" " + wordle[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    // Si el contador de lletres en verd es igual a cinc. El programa es tanca indicant que hem acertat la paraula
                    if (lettersGreen == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Environment.Exit(0);
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

            }






            //// Mostrem la matriu entera per si hem fallat
            //for (int rowShow = 0; rowShow < wordle.GetLength(0); rowShow++)
            //{
            //    for (int colShow = 0; colShow < wordle.GetLength(1); colShow++)
            //    {
            //        Console.Write(" " + wordle[rowShow, colShow]);

            //    }
            //    Console.WriteLine();
            //}

            Console.WriteLine($"Resposta correcta {word}");

        }
    }
}
