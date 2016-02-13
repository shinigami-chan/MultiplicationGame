using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = System.Random;
using System;

public class MultiplicationController : MonoBehaviour
{
    Random rnd = new Random(Guid.NewGuid().GetHashCode());

    private View view;
    private Game game;

    // Use this for initialization
    void Awake()
    {

        //initialize game (+ npc) and view (need to be added to an object -> using camera since it exists through the whole game)
        game = GameObject.Find("Main Camera").AddComponent<Game>();
        view = GameObject.Find("Main Camera").AddComponent<View>();
        game.setNpc( GameObject.Find("Main Camera").AddComponent<Npc>());

        game.getPlayer().setPlayerName(PlayerPrefs.GetString("PlayerName"));
        view.setPlayerName(game.getPlayer().getPlayerName());
        //load first round
        loadNewRound();
    }

    public void loadNewRound()
    {
        view.resetAllButtons();
        //all rounds have been played -> switch to the end screen memorizing player score
        if (game.loadCurrentQuest() == null)
        {
            Debug.Log("Alle Runden wurden gespielt.");
            PlayerPrefs.SetInt("PlayerPoints", game.getPlayer().getPoints()); //necessary for transfering this to the next scene
            Application.LoadLevel("end_screen");
        }
        //new quest has been loaded -> set the task to its panel and the options to the buttons, start npc behaviour
        else
        {
            Debug.Log("neue Quest wurde geladen.");
            view.setTaskText(game.currentQuest.getProblem().stringProblemTask());
            view.setButtonTexts(game.currentQuest.getOptions());
            StartCoroutine(startNpcBehaviour());
        }
    }


    //update points in class Player or Npc and in view
    public void updatePoints(int recievedPoints, bool player)
    {
        if (player)
        {
            game.getPlayer().setPoints(recievedPoints);
            view.refreshPoints(game.getPlayer().getPoints());
        }
        else
        {
            game.getNpc().setPoints(recievedPoints);
            view.refreshNpcPoints(game.getNpc().getPoints());
        }
    }


    public IEnumerator startNpcBehaviour()
    {
       

        yield return new WaitForSeconds(1);

        Debug.Log("click"+" "+game.getNpc().responseTime());

        int i = rnd.Next(0, 11);
        while (view.buttonList[i].interactable == false)
        {
            i = rnd.Next(0, 11);
        }
        view.buttonList[i].interactable = false;

        if (((MathOption)game.currentQuest.getOptions()[i]).getIsCorrect())
            {
                view.setDisabledButtonColor(view.buttonList[i], Color.green);
                updatePoints(10,false);
                view.disableAllButtons();
                Invoke("loadNewRound", 1.25f);
            }
            else
            {
                view.setDisabledButtonColor(view.buttonList[i], Color.red);
                StartCoroutine(startNpcBehaviour());
            }
    }


    //triggers events following a clicked button (points, new task etc.)
    public void evaluateAnswer(Button button)
    {
        button.interactable = false;
        int buttonIndex = view.buttonList.IndexOf(button);
        MathOption clickedOption = (MathOption)game.currentQuest.getOptions()[buttonIndex];
        clickedOption.setIsClicked();
       
        if (clickedOption.getIsCorrect())
        {
            StopAllCoroutines(); //prevents any interaction with the npc
            Debug.Log(game.currentQuest.getProblem().stringProblemTask() + " = " + game.currentQuest.getProblem().getSolution() + " correct answer given");
            view.setDisabledButtonColor(button, Color.green);

            updatePoints(10,true);
            view.disableAllButtons();

            //Invoke: adds delay time for the methods
            Invoke("loadNewRound", 1.25f);
        }
        else
        {
            Debug.Log(game.currentQuest.getProblem().stringProblemTask() + " = " + game.currentQuest.getProblem().getSolution() + " wrong answer given");

            view.setDisabledButtonColor(button, Color.red);
        }
    }
}
