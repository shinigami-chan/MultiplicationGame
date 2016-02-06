using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class View : MonoBehaviour
{
    public Button button0;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;
    public Button button8;
    public Button button9;
    public Button button10;
    public Button button11;

    public List<Button> buttonList = new List<Button>();


    public Text taskText;
    public Text playerScoreText;
    public Text playerNameText;
    public Text npcScoreText;

    public View() { }

    void Awake()
    {
        //adding all buttons to the list
        buttonList.Add(button0);
        buttonList.Add(button1);
        buttonList.Add(button2);
        buttonList.Add(button3);
        buttonList.Add(button4);
        buttonList.Add(button5);
        buttonList.Add(button6);
        buttonList.Add(button7);
        buttonList.Add(button8);
        buttonList.Add(button9);
        buttonList.Add(button10);
        buttonList.Add(button11);

        //GameObject.Find can only be used in the main thread -> referencing all buttons
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i] = GameObject.Find("Button" + i).GetComponent<Button>();
        }

        //referencing other UI Objects
        taskText = GameObject.Find("Task").GetComponent<Text>();
        playerNameText = GameObject.Find("Player").GetComponent<Text>();
        playerScoreText = GameObject.Find("PlayerScore").GetComponent<Text>();
        npcScoreText = GameObject.Find("NpcScore").GetComponent<Text>();
    }

    public void setPlayerName(string playerName)
    {
        playerNameText.text = playerName;
    }

    public void setDisabledButtonColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;  //copy
        cb.disabledColor = color;       //add change to the copy
        button.colors = cb;             //apply copy back
    }

    public void refreshPoints(int displayedPoints)
    {
        playerScoreText.text = "" + displayedPoints;
    }

    public void refreshNpcPoints(int displayedPoints)
    {
        npcScoreText.text = "" + displayedPoints;
    }

    public void setButtonTexts(ArrayList options)
    {
        MathOption currentOption;
        for (int i = 0; i < options.Count - 1; i++)
        {
            currentOption = ((MathOption)options[i]);
            buttonList[i].GetComponentInChildren<Text>().text = "" + currentOption.getAltSolution();
        }
    }

    public void setTaskText(string task)
    {
        taskText.text = task;
    }


}
