using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    private Player player;
    readonly int maxRounds = 1;
    public int currentRound = 0;
    public MultiplicationQuest currentQuest;

    public Game()
    {
        player = new Player();
    }

    //get a new Quest -> if all rounds have been played, null is returned
    public MultiplicationQuest loadCurrentQuest()
    {
        currentRound++;
        if (currentRound > maxRounds)
        {
            return null;
        }
        else
        {
            currentQuest = new MultiplicationQuest();
            return currentQuest;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public int getMaxRounds()
    {
        return maxRounds;
    }

    public Player getPlayer()
    {
        return player;
    }
}
