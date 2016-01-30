using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MultiplicationController : MonoBehaviour{

    private View view;
    private Game game;

    // Use this for initialization
    void Awake () {

        //initialize game and view (need to be added to an object -> using camera since it exists through the whole game)
        game = GameObject.Find("Main Camera").AddComponent<Game>();
        view = GameObject.Find("Main Camera").AddComponent<View>();

        game.getPlayer().setPlayerName(PlayerPrefs.GetString("PlayerName"));
        view.setPlayerName(game.getPlayer().getPlayerName());
        //load first round
        loadNewRound();
    }

    public void loadNewRound()
    {
        //all rounds have been played -> switch to the end screen memorizing player score
        if (game.loadCurrentQuest() == null)
        {
            //disableButtons();
            Debug.Log("Alle Runden wurden gespielt.");
            PlayerPrefs.SetInt("PlayerPoints", game.getPlayer().getPoints());
            Application.LoadLevel("end_screen");
        }
        //new quest has been loaded -> set the task to its panel and the options to the buttons
        else
        {
            Debug.Log("neue Quest wurde geladen.");
            view.setTaskText(game.currentQuest.getProblem().stringProblemTask());
            view.setButtonTexts(game.currentQuest.getOptions());
        }
    }

    //set all buttons disabled
    public void disableButtons()
    {
        for (int i = 0; i < view.buttonList.Count; i++)
        {
            view.buttonList[i].interactable = false;
        }
    }

    //sets all buttons interactable and its disabled color back to grey
    public void resetButtons()
    {
        ColorBlock cb;
        cb = view.buttonList[1].colors;

        for (int i = 0; i < view.buttonList.Count; i++)
        {
            view.buttonList[i].interactable = true;

            cb.disabledColor = Color.grey;
            view.buttonList[i].colors = cb;

        }
    }

    //update points in class Player
    public void updatePoints(int recievedPoints)
    {
        game.getPlayer().setPoints(recievedPoints);
        view.refreshPoints(game.getPlayer().getPoints());
    }

    //triggers events following a clicked button (points, new task etc.)
    public void evaluateAnswer(Button button)
        //TODO: behaviour when all Buttons have already been clicked
    {
        button.interactable = false;
        if (int.Parse(button.GetComponentInChildren<Text>().text) == game.currentQuest.getProblem().getSolution())
        {
            Debug.Log(game.currentQuest.getProblem().stringProblemTask()+" = "+ game.currentQuest.getProblem().getSolution()+" correct answer given");

            ColorBlock cb = button.colors;      //copy
            cb.disabledColor = Color.green;     //add change to the copy
            button.colors = cb;                 //apply copy back

            updatePoints(10);
            //Invoke: adds delay time for the methods
            Invoke("resetButtons", 1.25f);
            Invoke("loadNewRound", 1.25f);
        }
        else
        {
            Debug.Log(game.currentQuest.getProblem().stringProblemTask() + " = " + game.currentQuest.getProblem().getSolution() + " wrong answer given");

            ColorBlock cb = button.colors;
            cb.disabledColor = Color.red;
            button.colors = cb;
        }
    }
}
