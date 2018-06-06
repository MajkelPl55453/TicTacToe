using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    /// <summary>
    /// Klasa obsługująca logikę gry w pewnym stopniu
    /// </summary>
    public class LogicController
    {
        /// <summary>
        /// Metoda sprawdza, czy któryś z użytkowników wygrał grę.
        /// </summary>
        /// <param name="markedSpaces">Lista pól zawierająca obecnie postawione znaczniki na planszy</param>
        /// <param name="whoseTurn">Identyfikator gracza, który wykonał ruch</param>
        /// <returns>Zwraca id linii, która ma się wyświetlić na odpowiednich polach.</returns>
        public int WinnerCheck(int[] markedSpaces, int whoseTurn)
        {
            /*
                * Sprawdzanie czy ktoś wygrał. Polega na tym, że po kliknięciu w pole, w tablicę markedSpraces zostaje wpisana liczba 1 lub 2 w zależności od gracza.
                * jeżeli gra się skończyła, to któraś z poniższych zmiennych będzie wynosiła 3 lub 6. Dlaczego? Ponieważ w marked space zapisaliśmy id użytkownika, 
                * a do wygrania potrzebujemy 3 pól, więc 3 * 1 = 3, a 3*2 = 6.
                */
            int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
            int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
            int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
            int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
            int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
            int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
            int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
            int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

            //Dodanie zmiennych do tablicy w celu sprawdzenia pętlą
            var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
            for (int i = 0; i < solutions.Length; i++)
            {
                //Sprawdzenie czy ktoś wygrał
                if (solutions[i] == 3 * (whoseTurn + 1))
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>
        /// Metoda na podstawie podanego parametru określa, który użytkownik rozpocznie kolejna rundę.
        /// </summary>
        /// <param name="lastWinner">Id użytkownika który wygrał poprzednią rundę</param>
        /// <returns></returns>
        public int WhoWillStartNextRound(int lastWinner)
        {
            if (lastWinner == 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
