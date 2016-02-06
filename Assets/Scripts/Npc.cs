using UnityEngine;
using System.Collections;

public class Npc : MonoBehaviour
{

    readonly string npcName = "Gegner";
    private bool hasClicked = false;
    private int playerID;
    private int points;

    public int getPoints()
    {
        return points;
    }

    public void setPoints(int receivedPoints)
    {
        points += receivedPoints;
    }

}
