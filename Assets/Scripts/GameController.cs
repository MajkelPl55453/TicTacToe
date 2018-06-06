using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Jest to główna klasa, która steruję całą grą oraz jej wszystkimi funkcjonalnościami.
/// <param name="whoseTurn">Określa, który gracz obecnie ma prawo do wykonania ruchu. Zero oznacza, że ruch wykonuje gracz "X". Jeden oznacza, że ruch wykonuje gracz "O"</param>
/// <param name="turnCounter">Oblicza ile tur zostało wykonanych.</param>
/// <param name="turnIcons">Tablica zawierająca dwa obiekty typu GameObject. Każdy z tych obiektów posiada ikonę oznaczającą, która zostanie wyświetlona nad graczem gdy będzie wykonywał swój ruch.</param>
/// <param name="playerIcons">Tablica zawierająca dwa obiekty typu Sprite. Każdy z tych obiektów posiada ikonę gracza.</param>
/// <param name="tictactoeSpaces">Tablica zawiera obiekty typu Button. Są to pola, na których gracz może ustawić swój znacznik.</param>
/// <param name="markedSpaces">Tablica wartości typu int. Elementy tablicy mają klucze typu int od 0 do 8. Każdy element tablicy odpowiada jednemu polu w grze. Po kliknięciu i ustawieniu znacznika w danym elemencie tablicy zapisany zostaje identyfikator użytkownika.</param>
/// <param name="winnerText">Przetrzymuje odwołanie do obiektu typu Text. Dzięki temu, po zakończeniu rozgrywki, można zmienić tekst dla wygranego gracza oraz nie trzeba tworzyć 3 różnych obiektów aby obsłużyć każdy przypadek rostrzygnięcia rozgrywki.</param>
/// <param name="winningLines">Tablica obiektów typu GameObject. Każdy z 8 obiektów odpowiada jednej linii która może pokazać się po wygraniu rozgrywki. Posiada 3 linie poziome, 3 linie pionowe oraz 2 linie tworzące krzyż.</param>
/// <param name="winnerPanel">Jest to obiekt typu GameObject. Przetrzymuje odwołanie do panelu, który wyświetli się po wygraniu rozgrywki. Zabezpiecza to przed możliwością wyboru pola po zakończeniu rozgrywki.</param>
/// <param name="xPlayerScore">Zmienna typu int. Przetrzymuje punkty gracza "X"</param>
/// <param name="oPlayerScore">Zmienna typu int. Przetrzymuje punkty gracza "O"</param>
/// <param name="xPlayersScoreText">Zmienna typu Text. Zawiera dane obiektu wyświetlającego wynik dla gracza "X"</param>
/// <param name="oPlayersScoreText">Zmienna typu Text. Zawiera dane obiektu wyświetlającego wynik dla gracza "O"</param>
/// <param name="rematchButton">Zmienna typu Button. Po wciśnięciu przypisanego przycisku uruchomi się przypisana do niego akcja. W naszym przypadku rozpocznie się kolejna runda rozgrywki.</param>
/// <param name="restartButton">Zmienna typu Button. Po wciśnięciu przypisanego przycisku uruchomi się przypisana do niego akcja. W naszym przypadku rozpocznie się nowa rozgrywka. </param>
/// <param name="xPlayersButton">Zmienna typu Button. Po wciśnięciu przypisanego przycisku uruchomi się przypisana do niego akcja. W naszym przypadku zostanie wybrany gracz "X", który rozpocznie rozgrywkę.</param>
/// <param name="oPlayersButton">Zmienna typu Button. Po wciśnięciu przypisanego przycisku uruchomi się przypisana do niego akcja. W naszym przypadku zostanie wybrany gracz "O", który rozpocznie rozgrywkę.</param>
/// <param name="lastWinner">Zmienna typu int. Przechowuje id gracza, który wygrał ostatnią rozgrywkę. Używana jest do wyboru graczak, który ma rozpocząć kolejną rundę.</param>
/// <param name="catImage">Zmienna typu GameObject. Przechowuje odwołanie do obrazu wyświetlającego informację o remisie.</param>
/// <param name="buttonClickAudio">Zmienna typu AudioSource. Przechowuje ścieżkę dzwiękową, która zawiera dzwięk kliknięcia myszką.</param>
/// <param name="winAudio">Zmienna typu AudioSource. Przechowuje ścieżkę dzwiękową, która zawiera dzwięk odpowiedni dla wygrania rozgrywki.</param>
/// <param name="drawAudio">Zmienna typu AudioSource. Przechowuje ścieżkę dzwiękową, która zawiera dzwięk odpowiedni dla zremisowania rozgrywki.</param>
/// </summary>
public class GameController : MonoBehaviour
{
    public LogicController logicController;
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
    public Button rematchButton; // przycisk od ponownej rozgrywki gry
    public Button restartButton; // przycisk od restartu gry
    public Button xPlayersButton; // przycisk x
    public Button oPlayersButton; // przycisk o
    public int lastWinner; // id ostatniego zwyciężcy
    public GameObject catImage; // obraz dla litery remisu
    public AudioSource buttonClickAudio; // przetrzymuje dzwięk klinięcia
    public AudioSource winAudio; // dzwięk podczas wygrania
    public AudioSource drawAudio; // dzwięk podczas remisu

    /// <summary>
    /// Inicjalizacja gry. Wywołuje metodę ustawiającą podstawowe dane gry.
    /// </summary>
    void Start()
    {
        logicController = new LogicController();
        GameSetup();
    }

    /// <summary>
    /// Inicjalizacja danych gry, ustawienie podstawowych danych gry.
    /// </summary>
    void GameSetup()
    {
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

    /// <summary>
    /// Metoda wywoływana po ustawieniu kółka lub krzyżyka na planszy.
    /// </summary>
    /// <param name="whichNumber">Zmienna typu int. Przekazuje id wciśniętego przycisku.</param>
    public void TicTacToeButton(int whichNumber)
    {
        bool isWinner = false;
        xPlayersButton.interactable = false;
        oPlayersButton.interactable = false;
        tictactoeSpaces[whichNumber].image.sprite = playerIcons[whoseTurn];
        tictactoeSpaces[whichNumber].interactable = false;

        //Ustawiamy o jeden więcej aby poprawić logikę sprawdzania wygranego
        markedSpaces[whichNumber] = whoseTurn + 1;
        turnCounter++;

        if (turnCounter > 4)
        {
            int winnerCheck = logicController.WinnerCheck(markedSpaces, whoseTurn);
            if (winnerCheck >= 0)
            {
                isWinner = true;
                WinnderDisplay(winnerCheck);
            }
            else
            {
                isWinner = false;
            }
        }

        if (whoseTurn == 0)
        {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }

        if (turnCounter == 9 && isWinner == false)
        {
            Cat();
        }
        else if (isWinner == true)
        {
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(false);
        }
    }
    

    /// <summary>
    /// Metoda odpowiedzialna za wyświetlenie informacji o wygranym graczu.
    /// </summary>
    /// <param name="indexIn"></param>
    void WinnderDisplay(int indexIn)
    {
        lastWinner = whoseTurn;
        winnerPanel.gameObject.SetActive(true);
        winnerText.gameObject.SetActive(true);
        if (whoseTurn == 0)
        {
            xPlayerScore++;
            xPlayersScoreText.text = xPlayerScore.ToString();
            winnerText.text = "Gracz X wygrał!";
        }
        else if (whoseTurn == 1)
        {
            oPlayerScore++;
            oPlayersScoreText.text = oPlayerScore.ToString();
            winnerText.text = "Gracz O wygrał";
        }
        winningLines[indexIn].SetActive(true);
        rematchButton.interactable = true;
        winAudio.Play();

        turnIcons[0].SetActive(false);
        turnIcons[1].SetActive(false);
    }

    /// <summary>
    /// Metoda wywoływana po wciśnięciu przycisku odpowiedzialnego za uruchomienie kolejnej rundy.
    /// </summary>
    public void Rematch()
    {
        GameSetup();
        for (int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }
        winnerPanel.SetActive(false);
        rematchButton.interactable = false;
        xPlayersButton.interactable = true;
        oPlayersButton.interactable = true;
        catImage.SetActive(false);

        switchPlayer(logicController.WhoWillStartNextRound(lastWinner));
    }

    /// <summary>
    /// Metoda wywoływana po wciśnięciu przycisku odpowiedzialnego za uruchomienie nowej rozgrywki.
    /// </summary>
    public void Restart()
    {
        Rematch();
        xPlayerScore = 0;
        oPlayerScore = 0;
        xPlayersScoreText.text = "0";
        oPlayersScoreText.text = "0";
    }

    /// <summary>
    /// Metoda odpowiedzialna za przełączenie gracza.
    /// </summary>
    /// <param name="whichPlayer">Przechowuje id gracza, który ma mieć możliwość ruchu.</param>
    public void switchPlayer(int whichPlayer)
    {
        if (whichPlayer == 0)
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else if (whichPlayer == 1)
        {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
    }

    /// <summary>
    /// Metoda wywoływana w momencie, gdy gracze zremisują
    /// </summary>
    void Cat()
    {
        turnIcons[0].SetActive(false);
        turnIcons[1].SetActive(false);
        lastWinner = whoseTurn;
        winnerPanel.SetActive(true);
        catImage.SetActive(true);
        winnerText.text = "Remis!";
        rematchButton.interactable = true;
        drawAudio.Play();
    }

    /// <summary>
    /// Metoda odpowiedzialna za odtworzenie dzwięku kliknięcia przycisku
    /// </summary>
    public void PlayButtonClick()
    {
        buttonClickAudio.Play();
    }

    /// <summary>
    /// Metoda odpowiedzialna za wyświetlanie tymczasowego znacznika na przycisku na który gracz najechał kursorem
    /// </summary>
    /// <param name="whichNumber">Zmienna typu int. Przechowuje id pola w którym ma zostać wyświetlony placeholder</param>
    public void OnPointerEnter(int whichNumber)
    {
        if (tictactoeSpaces[whichNumber].interactable)
        {
            tictactoeSpaces[whichNumber].image.sprite = playerIcons[whoseTurn];
        }
    }

    /// <summary>
    /// Metoda odpowiedzialna za usunięcie tymczasowego znacznika na przycisku, z którego gracz zjechał kursorem
    /// </summary>
    /// <param name="whichNumber">Zmienna typu int. Przechowuje id pola w którym ma zostać usunięty placeholder</param>
    public void OnPointerExit(int whichNumber)
    {
        if (tictactoeSpaces[whichNumber].interactable)
        {
            tictactoeSpaces[whichNumber].image.sprite = null;
        }
    }

    /// <summary>
    /// Metoda uruchamiana jest w każdej klatce działania programu
    /// </summary>
    void Update()
    {

    }
}
