using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Assets.Scripts;

public class NewTestScript {

    [Test]
    public void NewTestScriptSimplePasses() {
        int prawidloweWyniki = 0;
        int[] markedSpaces = { };
        LogicController logicController = new LogicController();

        /*
         * Pierwsza pozioma linia od góry zostanie oznaczona, więc metoda WinnerCheck powinna zwrócić 0, 
         * ponieważ elementem 0 tablicy obiektów winningLines jest górna pozioma linia.
         */
        markedSpaces = new int[]{ 1, 1, 1, -100, -100, -100, -100, -100, -100 };
        if (logicController.WinnerCheck(markedSpaces, 0) == 0) { prawidloweWyniki++; }
        markedSpaces = new int[] { 2, 2, 2, -100, -100, -100, -100, -100, -100 };
        if (logicController.WinnerCheck(markedSpaces, 1) == 0) { prawidloweWyniki++; }

        /*
         * Środkowa pozioma linia zostanie oznaczona, więc metoda WinnerCheck powinna zwrócić 1, 
         * ponieważ elementem 1 tablicy obiektów winningLines jest środkowa pozioma linia.
         */
        markedSpaces = new int[] { -100, -100, -100, 1, 1, 1, -100, -100, -100 };
        if (logicController.WinnerCheck(markedSpaces, 0) == 1) { prawidloweWyniki++; }
        markedSpaces = new int[] { -100, -100, -100, 2, 2, 2, -100, -100, -100 };
        if (logicController.WinnerCheck(markedSpaces, 1) == 1) { prawidloweWyniki++; }

        /*
         * Dolna pozioma linia zostanie oznaczona, więc metoda WinnerCheck powinna zwrócić 2, 
         * ponieważ elementem 2 tablicy obiektów winningLines jest dolna pozioma linia.
         */
        markedSpaces = new int[] { -100, -100, -100, -100, -100, -100, 1, 1, 1 };
        if (logicController.WinnerCheck(markedSpaces, 0) == 2) { prawidloweWyniki++; }
        markedSpaces = new int[] { -100, -100, -100, -100, -100, -100, 2, 2, 2 };
        if (logicController.WinnerCheck(markedSpaces, 1) == 2) { prawidloweWyniki++; }

        /*
         * Lewa pionowa linia zostanie oznaczona, więc metoda WinnerCheck powinna zwrócić 3, 
         * ponieważ elementem 3 tablicy obiektów winningLines jest lewa pionowa linia.
         */
        markedSpaces = new int[] { 1, -100, -100, 1, -100, -100, 1, -100, -100 };
        if (logicController.WinnerCheck(markedSpaces, 0) == 3) { prawidloweWyniki++; }
        markedSpaces = new int[] { 2, -100, -100, 2, -100, -100, 2, -100, -100 };
        if (logicController.WinnerCheck(markedSpaces, 1) == 3) { prawidloweWyniki++; }

        /*
         * Środkowa pionowa linia zostanie oznaczona, więc metoda WinnerCheck powinna zwrócić 4, 
         * ponieważ elementem 4 tablicy obiektów winningLines jest środkowa pionowa linia.
         */
        markedSpaces = new int[] { -100, 1, -100, -100, 1, -100, -100, 1, -100 };
        if (logicController.WinnerCheck(markedSpaces, 0) == 4) { prawidloweWyniki++; }
        markedSpaces = new int[] { -100, 2, -100, -100, 2, -100, -100, 2, -100 };
        if (logicController.WinnerCheck(markedSpaces, 1) == 4) { prawidloweWyniki++; }

        /*
         * Prawa pionowa linia zostanie oznaczona, więc metoda WinnerCheck powinna zwrócić 5, 
         * ponieważ elementem 5 tablicy obiektów winningLines jest prawa pionowa linia.
         */
        markedSpaces = new int[] { -100, -100, 1, -100, -100, 1, -100, -100, 1 };
        if (logicController.WinnerCheck(markedSpaces, 0) == 5) { prawidloweWyniki++; }
        markedSpaces = new int[] { -100, -100, 2, -100, -100, 2, -100, -100, 2 };
        if (logicController.WinnerCheck(markedSpaces, 1) == 5) { prawidloweWyniki++; }

        /*
         * Linia ukośna od lewego górnego rogu do prawego dolnego zostanie oznaczona, więc metoda WinnerCheck powinna zwrócić 6, 
         * ponieważ elementem 6 tablicy obiektów winningLines jest linia ukośna od lewego górnego rogu do prawego dolnego.
         */
        markedSpaces = new int[] { 1, -100, -100, -100, 1, -100, -100, -100, 1 };
        if (logicController.WinnerCheck(markedSpaces, 0) == 6) { prawidloweWyniki++; }
        markedSpaces = new int[] { 2, -100, -100, -100, 2, -100, -100, -100, 2 };
        if (logicController.WinnerCheck(markedSpaces, 1) == 6) { prawidloweWyniki++; }

        /*
         * Linia ukośna od prawego górnego rogu do lewego dolnego zostanie oznaczona, więc metoda WinnerCheck powinna zwrócić 7, 
         * ponieważ elementem 7 tablicy obiektów winningLines jest linia ukośna od prawego górnego rogu do lewego dolnego.
         */
        markedSpaces = new int[] { -100, -100, 1, -100, 1, -100, 1, -100, -100 };
        if (logicController.WinnerCheck(markedSpaces, 0) == 7) { prawidloweWyniki++; }
        markedSpaces = new int[] { -100, -100, 2, -100, 2, -100, 2, -100, -100 };
        if (logicController.WinnerCheck(markedSpaces, 1) == 7) { prawidloweWyniki++; }

        //Jeżeli jest 16 poprawnych wyników, oznacza to, że wszystko się zgadza.
        Assert.AreEqual(prawidloweWyniki, 16);

        //Podając 1 powinno zwrócić 0
        Assert.AreEqual(0, logicController.WhoWillStartNextRound(1));

        //Podając 0 powinno zwrócić 1
        Assert.AreEqual(1, logicController.WhoWillStartNextRound(0));
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
