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
            string[] words = { "angel", "anima", "artic", "atoms", "audio", "camio", "cants", "civil", "decim", "delta", "digit", "dogma", "dolor", "gelat", "liceu", "licor", "local", "octal", "ocult", "orbes"};
            //Console.WriteLine(word[19]);
            //string wordle = word[19];
            //Console.WriteLine($"\n\n\n{wordle[2]}");

            Random rnd = new Random();
            int rnum = rnd.Next(0, 1);
            string word = words[rnum];

            string[,] wordle = new string[5, 5];
            for(int i = 0; i < wordle.GetLength(0); i++)
            {
                for(int j = 0; j < wordle.GetLength(1); j++)
                {
                    wordle[i, j] = "x";
                }
            }

            for(int i = 0; i < wordle.GetLength(0); i++)
            {
                Console.WriteLine("Escriu una paraula: ");
                string wordUser = Console.ReadLine();
                while(wordUser.Length > 5 || wordUser.Length < 0)
                {
                    Console.WriteLine("T'has equivocat amb el tamany de la paraula");
                    Console.WriteLine("Escriu una paraula: ");
                    wordUser = Console.ReadLine();
                }

                for (int j = 0; j < wordle.GetLength(1); j++)
                {
                    wordle[i, j] = Convert.ToString(wordUser[j]);
                }

                int cont = 0;

                for(int j = 0; j < wordle.GetLength(1); j++)
                {
                    if (Convert.ToChar(wordle[i, j]) == word[j])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" " + wordle[i, j]);
                        cont++;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" " + wordle[i, j]);
                    }


                    if (cont == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Environment.Exit(0);
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

            }







            for (int rowShow = 0; rowShow < wordle.GetLength(0); rowShow++)
            {
                for (int colShow = 0; colShow < wordle.GetLength(1); colShow++)
                {
                    Console.Write(" " + wordle[rowShow, colShow]);

                }
                Console.WriteLine();
            }

        }
    }
}
