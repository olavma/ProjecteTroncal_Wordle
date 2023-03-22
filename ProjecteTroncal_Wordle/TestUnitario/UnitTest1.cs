using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using Wordle_Olav;

namespace TestUnitario
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Nos aseguramos que la retorne el numero de letras en verde más 1
        /// </summary>
        [Test]
        public void NormalPaintGreen()
        {
            string[,] matrix = new string[6, 5];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++) matrix[i, j] = " ";
            }
            matrix[0, 0] = "a";
            matrix[0, 1] = "r";
            matrix[0, 2] = "b";
            matrix[0, 3] = "o";
            matrix[0, 4] = "l";
            Assert.AreEqual(2, Wordle_Olav.Wordle_Olav.PaintGreen(matrix, 0, 2, 1));
        }

        /// <summary>
        /// Comprovamos que si por alguna casualidad, el contador de letras en verde llega a cinco y se vuelve a ejecutar, 
        /// <para>devuelva 5, en vez de aumentar en uno.</para>
        /// </summary>
        [Test]
        public void Int5PaintGreen()
        {
            string[,] matrix = new string[6, 5];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++) matrix[i, j] = " ";
            }
            matrix[0, 0] = "a";
            matrix[0, 1] = "r";
            matrix[0, 2] = "b";
            matrix[0, 3] = "o";
            matrix[0, 4] = "l";
            Assert.AreEqual(5, Wordle_Olav.Wordle_Olav.PaintGreen(matrix, 0, 2, 5));
        }

        /// <summary>
        /// Comprovamos que si tenemos cinco letras y el codigo se ejecuta por alguna casualidad, no devuelva 6
        /// </summary>
        [Test]
        public void Superior5PaintGreen()
        {
            string[,] matrix = new string[6, 5];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++) matrix[i, j] = " ";
            }
            matrix[0, 0] = "a";
            matrix[0, 1] = "r";
            matrix[0, 2] = "b";
            matrix[0, 3] = "o";
            matrix[0, 4] = "l";
            Assert.AreNotEqual(6, Wordle_Olav.Wordle_Olav.PaintGreen(matrix, 0, 2, 5));
        }

        /// <summary>
        /// Comprovamos que selecciona bien la palabra random
        /// </summary>
        [Test]
        public void ChoosenWord()
        {
            string[] array = { "almas", "bueno", "adios" };
            Assert.AreEqual("almas", Wordle_Olav.Wordle_Olav.ChoosenWord(array, 0));
        }

        /// <summary>
        /// Comprobamos que la esperada y la elegida por el codigo no sean iguales
        /// </summary>
        [Test]
        public void NotChoosenWord()
        {
            string[] array = { "almas", "bueno", "adios" };
            Assert.AreNotEqual("almas", Wordle_Olav.Wordle_Olav.ChoosenWord(array, 1));
        }

        /// <summary>
        /// Comprovamos que el contenido del fichero de prueba wordfile1.txt, sea el mismo que el de una array creada manualmente
        /// </summary>
        [Test]
        public void WordFile()
        {
            string[] array = { "almas", "bueno", "adios" };
            Assert.AreEqual(array, Wordle_Olav.Wordle_Olav.WordFile(@"..\..\..\TestFiles\wordfile1.txt"));
        }

        /// <summary>
        /// Comprovamos que el contenido del fichero de prueba wordfile2.txt, no sea el mismo que el de una array creada manualmente
        /// </summary>
        [Test]
        public void IncorrectWordFile()
        {
            string[] array = { "almas", "bueno", "adios" };
            Assert.AreNotEqual(array, Wordle_Olav.Wordle_Olav.WordFile(@"..\..\..\TestFiles\wordfile2.txt"));
        }

        /// <summary>
        /// Comprobamos que la palabra introducida por el usuario quede guardada en una matriz, de la forma esperada
        /// </summary>
        [Test]
        public void UserInteractionTest()
        {
            string[,] matrix = { 
                { "a", "r", "b", "o", "l" },
                { " ", " ", " ", " ", " "}
            };

            string[,] expected = {
                { "a", "r", "b", "o", "l" },
                { "a", "l", "m", "a", "s"}
            };

            StreamReader sr = File.OpenText(@"..\..\..\..\Wordle\Archives\Idiomas\es.txt");
            string st = sr.ReadToEnd();
            sr.Close();

            List<string> palabrasUsuario = new List<string>();

            string[] test = st.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(expected, Wordle_Olav.Wordle_Olav.UserInteraction(matrix, 1, test, "almas", palabrasUsuario));
        }

        /// <summary>
        /// Comprovamos que la palabra del usuario dentro de la matriz, no es igual a la esperada
        /// </summary>
        [Test]
        public void ErrorUserInteractionTest()
        {
            string[,] matrix = {
                { "a", "r", "b", "o", "l" },
                { " ", " ", " ", " ", " "}
            };

            string[,] expected = {
                { "a", "r", "b", "o", "l" },
                { "a", "l", "m", "a", "s"}
            };

            StreamReader sr = File.OpenText(@"..\..\..\..\Wordle\Archives\Idiomas\es.txt");
            string st = sr.ReadToEnd();
            sr.Close();
            List<string> palabrasUsuario = new List<string>();
            string[] test = st.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreNotEqual(expected, Wordle_Olav.Wordle_Olav.UserInteraction(matrix, 1, test, "almes", palabrasUsuario));
        }

        /// <summary>
        /// Si ocurre algun problema durante la ejecucion, que el texto dentro de la matriz se pueda reemplazar.
        /// </summary>
        [Test]
        public void I_UserInteractionTest()
        {
            string[,] matrix = {
                { "a", "r", "b", "o", "l" },
                { " ", " ", " ", " ", " "}
            };

            string[,] expected = {
                { "a", "l", "m", "a", "s" },
                { " ", " ", " ", " ", " "}
            };

            StreamReader sr = File.OpenText(@"..\..\..\..\Wordle\Archives\Idiomas\es.txt");
            string st = sr.ReadToEnd();
            sr.Close();
            List<string> palabrasUsuario = new List<string>();
            string[] test = st.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(expected, Wordle_Olav.Wordle_Olav.UserInteraction(matrix, 0, test, "almas", palabrasUsuario));
        }
    }
}