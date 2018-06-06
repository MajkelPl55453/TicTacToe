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
    }

    public void TicTacToeButton(int whichNumber){
        tictactoeSpaces[whichNumber].image.sprite = playerIcons[whoseTurn];
        tictactoeSpaces[whichNumber].interactable = false;

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

	// Update is called once per frame
	void Update () {
		
	}
}
