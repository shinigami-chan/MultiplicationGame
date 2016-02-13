using UnityEngine;
using System.Collections;

public class Npc : MonoBehaviour
{
    readonly string npcName = "Gegner";
    //private bool hasClicked = false;
    private int playerID;
    private int points = 0;
    
    public double responseTime()
    {

        return 0;
    }

    public int getPoints()
    {
        return points;
    }

    public void setPoints(int receivedPoints)
    {
        points += receivedPoints;
    }

}
