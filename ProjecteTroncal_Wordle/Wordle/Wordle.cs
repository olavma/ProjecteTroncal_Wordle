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
            var ex = new Wordle();
            ex.WannaPlay();
        }

        /// <summary>
        /// Preguntamos al usuario si quiere jugar
        /// <para>Si escribe 'S' o 's' comenzara el juego</para>
        /// </summary>
        void WannaPlay()
        {
            Console.WriteLine("Desea jugar? (S/N)");
            string replay = Console.ReadLine();

            // Comprovamos que solo introduzca 'S/s' o 'N/n'
            while (replay.ToUpper() != "S" && replay.ToUpper() != "N")
            {
                Console.WriteLine("Solo S o N");
                replay = Console.ReadLine();
            }

            // Si escribe S/s llamara a la funcion Start()
            while (replay.ToUpper() != "N")
            {
                Console.Clear();
                Start();
            }
            Console.WriteLine("Adios");
            Environment.Exit(0);
        }

        /// <summary>
        /// Creamos la array de string, el numero random, creamos y llenamos la matriz y llamamos a Play()
        /// </summary>
        void Start()
        {
            // Array de paraules
            string[] words = { "angel", "anima", "artic", "atoms", "audio", "camio", "cants", "civil", "decim", "delta", "digit", "dogma", "dolor", "gelat", "inici", "liceu", "licor", "local", "octal", "ocult", "orbes" };

            // Generem un numero aleatori i guardem la paraula a la que correspon aquest numero per poder comparar durant el joc.
            Random rnd = new Random();
            int rnum = rnd.Next(0, 21);
            string word = words[rnum];

            // Creem la matriu del wordle
            string[,] wordle = new string[6, 5];

            // La omplim d'x per a que tingui contingut
            for (int i = 0; i < wordle.GetLength(0); i++)
            {
                for (int j = 0; j < wordle.GetLength(1); j++)
                {
                    wordle[i, j] = " ";
                }
            }

            Console.WriteLine(word);
            Console.ForegroundColor = ConsoleColor.White;
            // Comença la interaccio amb l'usuari
            Play(wordle, word);
        }

        /// <summary>
        /// Funcion que reproduce una partida de este Wordle
        /// </summary>
        /// <param name="wordle">Matriz del Wordle</param>
        /// <param name="word">Palabra aleatoria</param>
        void Play(string[,] wordle, string word)
        {
            for (int i = 0; i < wordle.GetLength(0); i++)
            {
                // Llamamos a una funcion para que escriba una palabra
                UserInteraction(wordle, i);

                // Contador per les lletres en verd
                int lettersGreen = 0;

                // Comencen les comprovacions
                Comprovaciones(wordle, i, lettersGreen, word);

                Console.Clear();

                // Mostrem la matriu entera per si hem fallat
                Matrix(word, wordle);
            }
            //Si no se ha encontrado la palabra, el juego finalizara con esta funcion
            EndGame(word);
        }

        /// <summary>
        /// Vamos preguntando al usuario la palabra que quiere escribir
        /// </summary>
        /// <param name="wordle">Matriz del Wordle</param>
        /// <param name="i">Iterador que indica en que linia del Wordle tiene que escribir</param>
        /// <returns>Devuelve la matriz del Wordle</returns>
        string[,] UserInteraction(string[,] wordle, int i)
        {
            // Escriu la paraula
            Console.Write("Escriu una paraula: ");
            string wordUser = Console.ReadLine();

            //Es comprova si compleix els requisits
            while (wordUser.Length != 5)
            {
                Console.WriteLine("T'has equivocat amb el tamany de la paraula");
                Console.Write("Escriu una paraula: ");
                wordUser = Console.ReadLine();
            }

            // Guarda el contingut en una linea de la matriu de Wordle
            for (int j = 0; j < wordle.GetLength(1); j++)
            {
                wordle[i, j] = Convert.ToString(wordUser[j]);
            }
            return wordle;
        }

        /// <summary>
        /// Si no se ha encontrado la palabra se ejecutara esta funcion
        /// </summary>
        /// <param name="word">Palabra aleatoria que hay que buscar</param>
        void EndGame(string word)
        {
            Console.Write("Resposta correcta: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(word);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            WannaPlay();
        }

        /// <summary>
        /// Mostramos la matriz para ver en que nos hemos equivocado
        /// </summary>
        /// <param name="word">Palabra aleatoria</param>
        /// <param name="wordle">Matriz del Wordle</param>
        void Matrix(string word, string[,] wordle)
        {
            for (int rowShow = 0; rowShow < wordle.GetLength(0); rowShow++)
            {
                for (int colShow = 0; colShow < wordle.GetLength(1); colShow++)
                {
                    if (Convert.ToChar(wordle[rowShow, colShow]) == word[colShow])
                    {
                        Green(wordle, rowShow, colShow);
                    }
                    // Si no es comproven si les lletres es troben o no dins de la paraula
                    else
                    {
                        /* Primer es comprova si alguna de les lletras de la paraula de l'usuari no existeix en la paraula aleatoria. 
                         * Aquesta lletra que no existeix es pinta de color Gris Fosc.
                        */
                        for (int k = 0; k < wordle.GetLength(1); k++)
                        {
                            if (Convert.ToChar(wordle[rowShow, colShow]) != word[k])
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                            }
                        }

                        /* Despres es comprova si alguna lletra de la paraula de l'usuari existeix en la paraula aleatoria pero es troba en un altre lloc.
                         * Aquesta lletra en diferent posicio es pinta de color Groc.
                         */
                        for (int k = 0; k < wordle.GetLength(1); k++)
                        {
                            if (Convert.ToChar(wordle[rowShow, colShow]) == word[k])
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                        }

                        Console.Write(" " + wordle[rowShow, colShow]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Pinta en verde las letras que estan bien
        /// </summary>
        /// <param name="wordle">Wordle</param>
        /// <param name="i">Fila en la que se encuentra</param>
        /// <param name="j">Columna en la que se encuentra</param>
        void Green(string[,] wordle, int i, int j)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" " + wordle[i, j]);
        }

        /// <summary>
        /// Funcion que llama a Green() para pintar las letras y devuelve un contador más 1 para saber cuantas llebamos
        /// </summary>
        /// <param name="wordle">Wordle</param>
        /// <param name="i">Fila en la que estamos</param>
        /// <param name="j">Columna en la que estamos</param>
        /// <param name="lettersGreen">Contador de las letras pintadas de verde</param>
        /// <returns></returns>
        int PaintGreen(string[,] wordle, int i, int j, int lettersGreen)
        {
            Green(wordle, i, j);
            return (lettersGreen + 1);
        }

        /// <summary>
        /// Pinta de un determinado color, la linea y columna en la que estamos
        /// </summary>
        /// <param name="color">Color del que se pintara la fila y columna</param>
        void PaintNotGreen(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        /// <summary>
        /// Si no esta en la palabra, se pintara de gris oscuro
        /// </summary>
        /// <param name="wordle">Wordle</param>
        /// <param name="rowShow">Fila en la que estamos</param>
        /// <param name="colShow">Columna en la que estamos</param>
        /// <param name="word">Palabra secreta</param>
        void IsDarkGray(string[,] wordle, int rowShow, int colShow, string word)
        {
            /* Primer es comprova si alguna de les lletras de la paraula de l'usuari no existeix en la paraula aleatoria. 
            * Aquesta lletra que no existeix es pinta de color Gris Fosc.
            */
            for (int k = 0; k < wordle.GetLength(1); k++)
            {
                if (Convert.ToChar(wordle[rowShow, colShow]) != word[k])
                {
                    PaintNotGreen(ConsoleColor.DarkGray);
                }
            }
        }

        /// <summary>
        /// Si no esta en la palabra, se pintara de amarillo
        /// </summary>
        /// <param name="wordle">Wordle</param>
        /// <param name="rowShow">Fila en la que estamos</param>
        /// <param name="colShow">Columna en la que estamos</param>
        /// <param name="word">Palabra secreta</param>
        void IsYellow(string[,] wordle, int rowShow, int colShow, string word)
        {
            /* Despres es comprova si alguna lletra de la paraula de l'usuari existeix en la paraula aleatoria pero es troba en un altre lloc.
            * Aquesta lletra en diferent posicio es pinta de color Groc.
            */
            for (int k = 0; k < wordle.GetLength(1); k++)
            {
                if (Convert.ToChar(wordle[rowShow, colShow]) == word[k])
                {
                    PaintNotGreen(ConsoleColor.Yellow);
                }
            }
        }

        /// <summary>
        /// Si se ha acertado la palabra se mostrara toda la matriz con todos sus errores.
        /// </summary>
        /// <param name="wordle">Wordle</param>
        /// <param name="word">Palabra secreta que hay que adivinar</param>
        void Victory(string[,] wordle, string word)
        {
            Console.Clear();

            for (int rowShow = 0; rowShow < wordle.GetLength(0); rowShow++)
            {
                for (int colShow = 0; colShow < wordle.GetLength(1); colShow++)
                {
                    if (Convert.ToChar(wordle[rowShow, colShow]) == word[colShow])
                    {
                        Green(wordle, rowShow, colShow);
                    }
                    // Si no es comproven si les lletres es troben o no dins de la paraula
                    else
                    {
                        IsDarkGray(wordle, rowShow, colShow, word);

                        IsYellow(wordle, rowShow, colShow, word);

                        Console.Write(" " + wordle[rowShow, colShow]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;

            // Preguntamos si queremos volver a jugar o no
            WannaPlay();
        }

        /// <summary>
        /// Funcion que realizara todas las comprovaciones necesarias
        /// </summary>
        /// <param name="wordle">Wordle</param>
        /// <param name="i">Fila en la que estamos</param>
        /// <param name="lettersGreen">Numero de letras pintadas de verde</param>
        /// <param name="word">Palabra que hay que adivinar</param>
        void Comprovaciones(string[,] wordle, int i, int lettersGreen, string word)
        {
            for (int j = 0; j < wordle.GetLength(1); j++)
            {
                // Si la lletra de la paraula de l'usuari coincideix amb la lletra de la mateixa posicio de la paraula secreta, es pintara de verd
                if (Convert.ToChar(wordle[i, j]) == word[j])
                {
                    lettersGreen = PaintGreen(wordle, i, j, lettersGreen);
                }
                // Si no es comproven si les lletres es troben o no dins de la paraula
                else
                {
                    /* Primer es comprova si alguna de les lletras de la paraula de l'usuari no existeix en la paraula aleatoria. 
                     * Aquesta lletra que no existeix es pinta de color Gris Fosc.
                    */
                    IsDarkGray(wordle, i, j, word);

                    /* Despres es comprova si alguna lletra de la paraula de l'usuari existeix en la paraula aleatoria pero es troba en un altre lloc.
                     * Aquesta lletra en diferent posicio es pinta de color Groc.
                     */
                    IsYellow(wordle, i, j, word);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                /* Si el contador de lletres en verd es igual a cinc. El programa es tanca indicant que hem acertat la paraula
                 * I mostra la matriu del joc amb les lletres pintades de forma correcta 
                 */
                if (lettersGreen == 5)
                {
                    Victory(wordle, word);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
    }
}
