using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int whoseTurn; // 0 = X; 1 = O;
    public int turnCounter; // zlicza liczbę tur
    public GameObject[] turnIcons; // wyświetla czyja tura
    public Sprite[] playerIcons; //0 = X; 1 = O;
    public Button[] tictactoeSpaces; //klikalne pola w grze
    public int[] markedSpaces; // id pól klikniętych przez konkretnego gracza
    public Text winnerText; // Trzyma komponent tekstu dla wygranego gracza
    public GameObject[] winningLines; // przetrzymuje linie dla wygrania
    public GameObject winnerPanel; // panel zabezpieczający przed klikaniem przysików po wygraniu
    public int xPlayerScore; // punkty dla gracza x
    public int oPlayerScore; // punkty dla gracza o
    public Text xPlayersScoreText; // pole tekstowe z punktami dla gracza x
    public Text oPlayersScoreText; // pole tekstowe z punktami dla gracza o
    public GameObject rematchButton; // przycisk od ponownej rozgrywki gry
    public GameObject restartButton; // przycisk od restartu gry

    // Use this for initialization
    void Start () {
        GameSetup();
	}

    void GameSetup() {
        whoseTurn = 0;
        turnCounter = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    public void TicTacToeButton(int whichNumber){
        tictactoeSpaces[whichNumber].image.sprite = playerIcons[whoseTurn];
        tictactoeSpaces[whichNumber].interactable = false;

        //Ustawiamy o jeden więcej aby poprawić logikę sprawdzania wygranego
        markedSpaces[whichNumber] = whoseTurn+1;
        turnCounter++;

        if (turnCounter > 4)
        {
            WinnerCheck();
        }

        if (whoseTurn == 0) {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    void WinnerCheck() {
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
                WinnderDisplay(i);
                return;
            }
        }
    }

    void WinnderDisplay(int indexIn) {
        winnerPanel.gameObject.SetActive(true);
        winnerText.gameObject.SetActive(true);
        if (whoseTurn == 0)
        {
            xPlayerScore++;
            xPlayersScoreText.text = xPlayerScore.ToString();
            winnerText.text = "Gracz X wygrał!";
        }
        else if(whoseTurn == 1)
        {
            oPlayerScore++;
            oPlayersScoreText.text = oPlayerScore.ToString();
            winnerText.text = "Gracz O wygrał";
        }
        winningLines[indexIn].SetActive(true);
        rematchButton.SetActive(true);
    }

    public void Rematch()
    {
        GameSetup();
        for (int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }
        winnerPanel.SetActive(false);
        rematchButton.SetActive(false);
    }

    public void Restart()
    {
        Rematch();
        xPlayerScore = 0;
        oPlayerScore = 0;
        xPlayersScoreText.text = "0";
        oPlayersScoreText.text = "0";
    }

    // Update is called once per frame
    void Update () {
		
	}
}
