/*
 * AUTHOR: Olav Martos Aceña
 * DATE: 29/11/2022 - UF1
 * DATE: 21/02/2023 - UF2
 * DATE: 23/02/2023 - UF3
 * DESCRIPTION: Es tracta de fer una solució que ens permeti jugar al joc Wordle.
 *      Com encara estem als inicis del curs i gairebé no heu treballat interfícies d'usuari, el joc funcionarà a través de la terminal. 
 */

using System;
using System.IO;

namespace Wordle
{
    public class WordleGame
    {
        /// <summary>
        /// Mostramos el menu principal que esta dentro del config.txt
        /// </summary>
        static void Main()
        {
            var ex = new WordleGame();
            StreamReader sr = File.OpenText(@"..\..\..\Archives\config.txt");
            string st = sr.ReadToEnd();
            sr.Close();

            string[] configLines = st.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            ex.Menu(configLines);
        }

        void Menu(string[] configLines)
        {
            for(int i = 0; i < 3; i++) Console.WriteLine(configLines[i]);
            Console.Write("> ");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Language(configLines);
                    break;
                case "2":
                    History(configLines);
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Pedimos al usuario el idioma en el que quiere jugar
        /// </summary>
        /// <param name="configLines">Lineas del contenido del fichero config.txt</param>
        void Language(string[] configLines)
        {
            string path = @"..\..\..\Archives\Idiomas\";
            Console.Write(configLines[3]);
            string lang = Console.ReadLine();

            while (true)
            {
                // Si existe la ruta con el idioma se iniciara el juego.
                if (File.Exists(path + lang + ".txt"))
                {
                    string langPath = path + lang + @".txt";
                    StreamReader sr = File.OpenText(langPath);
                    string st = sr.ReadToEnd();
                    sr.Close();

                    string[] file = st.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    WannaPlay(file, lang);
                }
                // Si no, nos saldra un error por pantalla
                else Console.WriteLine(configLines[4] + "\n");
                Console.Write(configLines[3]);
                lang = Console.ReadLine();
            }
        }
        /// <summary>
        /// Muestra el historico de las partidas sin tener en cuenta el usuario que quiere acceder
        /// </summary>
        /// <param name="configLines">Lineas del fichero de configuracion</param>
        void History(string[] configLines)
        {
            Console.Clear();
            string folderPath = @"..\..\..\Archives\Partidas\";

            // Obtenemos los archivos del directorio Partidas
            string[] fileNames = Directory.GetFiles(folderPath);
            int partidasNum = 1;

            // Creamos un bucle para ir recorriendo la lista de los archivos
            foreach (string file in fileNames)
            {
                Console.WriteLine($"-------------------- {partidasNum} Partida --------------------");
                Console.WriteLine(configLines[5] + Path.GetFileName(file));
                Console.WriteLine();

                string content = File.ReadAllText(file);

                string[] fileContent = content.Split(new char[] { ';', '\n', '\r'});

                Console.WriteLine(configLines[6] + fileContent[0]);
                Console.WriteLine(configLines[7] + fileContent[1]);
                Console.WriteLine(configLines[8] + fileContent[2]);
                Console.Write(configLines[9]);

                // Comprobamos el statusInt, un numero que determina si ha sido victoria o no para cambiar de color la palabra
                if (fileContent[4] == "1") Console.ForegroundColor = ConsoleColor.Green;
                else Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(fileContent[3]+"\n");
                Console.ResetColor();
                partidasNum++;
            }
            Console.WriteLine("\n\n" + configLines[10]);
            Console.ReadLine();
            Console.Clear();
            Main();
        }

        /// <summary>
        /// Preguntamos al usuario si quiere jugar
        /// <para>Si escribe 'S' o 's' comenzara el juego</para>
        /// </summary>
        /// <param name="file">Contenido del archivo de idioma seleccionado</param>
        /// <param name="lang">Idioma escogido</param>
        void WannaPlay(string[] file, string lang)
        {
            Console.WriteLine(file[0]);
            string replay = Console.ReadLine();

            // Comprovamos que solo introduzca 'S/s' o 'N/n' o 'Y/y'
            while ((replay.ToUpper() != "S" && replay.ToUpper() != "N") && (replay.ToUpper() != "Y" && replay.ToUpper() != "N"))
            {
                Console.WriteLine(file[1]);
                replay = Console.ReadLine();
            }

            // Si escribe S/s llamara a la funcion Start()
            while (replay.ToUpper() != "N")
            {
                Console.Clear();
                Start(file, lang);
            }
            Console.WriteLine(file[2]);
            Console.ReadLine();
            Console.Clear();
            Main();
        }

        /// <summary>
        /// Creamos la array de string, el numero random, creamos y llenamos la matriz y llamamos a Play()
        /// </summary>
        /// <param name="file">Contenido del archivo de idioma seleccionado</param>
        /// <param name="lang">Idioma escogido</param>
        void Start(string[] file, string lang)
        {
            Console.WriteLine(file[7]);
            string username = Console.ReadLine();


            string[] words = WordFile(file[3]);
            Random rnd = new Random();
            int rnum = rnd.Next(0, words.Length);
            string word = ChoosenWord(words, rnum);;


            string[,] wordle = new string[6, 5];

            for (int i = 0; i < wordle.GetLength(0); i++)
            {
                for (int j = 0; j < wordle.GetLength(1); j++) wordle[i, j] = " ";
            }

            Console.WriteLine(word);
            Console.ForegroundColor = ConsoleColor.White;
            Play(wordle, word, file, username, lang);
            Console.Read();
        }

        /// <summary>
        /// Crea una array de strings que contiene todas las palabras del idioma seleccionado por el usuario
        /// </summary>
        /// <param name="file">Linea del fitxer d'idioma que indica on estan les paraules guardades</param>
        /// <returns>L'array de string</returns>
        public static string[] WordFile(string file)
        {
            StreamReader sr = File.OpenText(file);
            string st = sr.ReadToEnd();
            sr.Close();

            string[] words = st.Split(new char[] { '\n', ' ', '\r', ',' }, StringSplitOptions.RemoveEmptyEntries);

            return words;
        }

        /// <summary>
        /// Funció que retorna la una paraula aleatoria
        /// </summary>
        /// <param name="words">Llista de paraules</param>
        /// <param name="rnum">Numero aleatoria</param>
        /// <returns>Una paraula aleatoria de totes les del fitxer de paraules</returns>
        public static string ChoosenWord(string[] words, int rnum) 
        { 
            return words[rnum]; 
        }

        /// <summary>
        /// Funcion que reproduce una partida de este Wordle
        /// </summary>
        /// <param name="wordle">Matriz del Wordle</param>
        /// <param name="word">Palabra aleatoria</param>
        /// <param name="file">Contenido del archivo de idioma seleccionado</param>
        /// <param name="username">Nombre de usuario</param>
        /// <param name="lang">Idioma escogido</param>
        void Play(string[,] wordle, string word, string[] file, string username, string lang)
        {
            for (int i = 0; i < wordle.GetLength(0); i++)
            {
                Console.Write(file[4]);
                string wordUser = Console.ReadLine();

                UserInteraction(wordle, i, file, wordUser);
                int lettersGreen = 0;
                Comprovaciones(wordle, i, lettersGreen, word, file, username, lang);
                Console.Clear();
                Matrix(word, wordle);
            }
            EndGame(word, file, username, lang);
        }

        /// <summary>
        /// Vamos preguntando al usuario la palabra que quiere escribir
        /// </summary>
        /// <param name="wordle">Matriz del Wordle</param>
        /// <param name="i">Iterador que indica en que linia del Wordle tiene que escribir</param>
        /// <param name="file">Contenido del archivo de idioma seleccionado</param>
        /// <param name="wordUser">Palabra escrita por el usuario</param>
        /// <returns>Devuelve la matriz del Wordle</returns>
        public static string[,] UserInteraction(string[,] wordle, int i, string[] file, string wordUser)
        {
            while (wordUser.Length != 5)
            {
                Console.WriteLine(file[5]);
                Console.Write(file[4]);
                wordUser = Console.ReadLine();
            }
            
            for (int j = 0; j < wordle.GetLength(1); j++) { wordle[i, j] = Convert.ToString(wordUser[j]); }
            return wordle;
        }

        /// <summary>
        /// Si no se ha encontrado la palabra se ejecutara esta funcion
        /// </summary>
        /// <param name="word">Palabra aleatoria que hay que buscar</param>
        /// <param name="file">Contenido del archivo de idioma seleccionado</param>
        /// <param name="username">Nombre del usuario</param>
        /// <param name="lang">Idioma escogido</param>
        void EndGame(string word, string[] file, string username, string lang)
        {
            Console.Write(file[6]);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(word);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Save(username, word, false, file, lang);
            WannaPlay(file, lang);
        }

        /// <summary>
        /// Guardamos la partida recien jugada
        /// </summary>
        /// <param name="username">Nombre de usuario</param>
        /// <param name="word">Palabra que hay que buscar</param>
        /// <param name="win">Si se ha ganado o no</param>
        /// <param name="file">Contenido del archivo de idioma seleccionado</param>
        /// <param name="lang">Idioma escogido</param>
        void Save(string username, string word, bool win, string[] file, string lang)
        {
            string status = file[9];
            int statusInt = 0;
            if (win)
            {
                status = file[10];
                statusInt = 1;
            }
            string path = @"..\..\..\Archives\Partidas\";
            DateTime now = DateTime.Now;
            string dateStr = now.ToString("yyyyMMdd_HHmmss");
            using (StreamWriter sw = File.AppendText(path + dateStr + "_" + username + ".txt"))
            {
                sw.WriteLine($"{username};{word};{lang};{status};{statusInt}");
            }
            Console.WriteLine(file[8]);
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
                    if (Convert.ToChar(wordle[rowShow, colShow]) == word[colShow]) Green(wordle, rowShow, colShow); 
                    else
                    {
                        for (int k = 0; k < wordle.GetLength(1); k++)
                        {
                            if (Convert.ToChar(wordle[rowShow, colShow]) != word[k]) IsDarkGray(wordle, rowShow, colShow, word);
                        }
                        for (int k = 0; k < wordle.GetLength(1); k++)
                        {
                            if (Convert.ToChar(wordle[rowShow, colShow]) == word[k]) IsYellow(wordle, rowShow, colShow, word);
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
        public static void Green(string[,] wordle, int i, int j)
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
        /// <returns>Devuelve el numero de letras en verde más 1</returns>
        public static int PaintGreen(string[,] wordle, int i, int j, int lettersGreen)
        {
            Green(wordle, i, j);
            if(lettersGreen+1 > 5) return lettersGreen;
            else return (lettersGreen + 1);
        }

        /// <summary>
        /// Pinta de un determinado color, la linea y columna en la que estamos
        /// </summary>
        /// <param name="color">Color del que se pintara la fila y columna</param>
        void PaintNotGreen(ConsoleColor color) { Console.ForegroundColor = color; }

        /// <summary>
        /// Si no esta en la palabra, se pintara de gris oscuro
        /// </summary>
        /// <param name="wordle">Wordle</param>
        /// <param name="rowShow">Fila en la que estamos</param>
        /// <param name="colShow">Columna en la que estamos</param>
        /// <param name="word">Palabra secreta</param>
        void IsDarkGray(string[,] wordle, int rowShow, int colShow, string word)
        {
            for (int k = 0; k < wordle.GetLength(1); k++)
            {
                if (Convert.ToChar(wordle[rowShow, colShow]) != word[k]) PaintNotGreen(ConsoleColor.DarkGray);
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
            for (int k = 0; k < wordle.GetLength(1); k++)
            {
                if (Convert.ToChar(wordle[rowShow, colShow]) == word[k]) PaintNotGreen(ConsoleColor.Yellow);
            }
        }

        /// <summary>
        /// Si se ha acertado la palabra se mostrara toda la matriz con todos sus errores.
        /// </summary>
        /// <param name="wordle">Wordle</param>
        /// <param name="word">Palabra secreta que hay que adivinar</param>
        /// <param name="file">Contenido del archivo de idioma seleccionado</param>
        /// <param name="username">Nombre del usuario</param>
        /// <param name="lang">Idioma escogido</param>
        void Victory(string[,] wordle, string word, string[] file, string username, string lang)
        {
            Console.Clear();

            for (int rowShow = 0; rowShow < wordle.GetLength(0); rowShow++)
            {
                for (int colShow = 0; colShow < wordle.GetLength(1); colShow++)
                {
                    if (Convert.ToChar(wordle[rowShow, colShow]) == word[colShow]) Green(wordle, rowShow, colShow);
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
            Save(username, word, true, file, lang);
            WannaPlay(file, lang);
        }

        /// <summary>
        /// Funcion que realizara todas las comprovaciones necesarias
        /// </summary>
        /// <param name="wordle">Wordle</param>
        /// <param name="i">Fila en la que estamos</param>
        /// <param name="lettersGreen">Numero de letras pintadas de verde</param>
        /// <param name="word">Palabra que hay que adivinar</param>
        /// <param name="file">Contenido del archivo de idioma seleccionado</param>
        /// <param name="lang">Idioma escogido</param>
        /// <param name="username">Nombre del usuario</param>
        void Comprovaciones(string[,] wordle, int i, int lettersGreen, string word, string[] file, string username, string lang)
        {
            for (int j = 0; j < wordle.GetLength(1); j++)
            {
                if (Convert.ToChar(wordle[i, j]) == word[j]) lettersGreen = PaintGreen(wordle, i, j, lettersGreen);
                else
                {
                    IsDarkGray(wordle, i, j, word);
                    IsYellow(wordle, i, j, word);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (lettersGreen == 5) Victory(wordle, word, file, username, lang);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
    }
}
