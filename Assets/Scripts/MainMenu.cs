using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text playerName;

    void Start()
    {
        playerName = GameObject.Find("Text").GetComponent<Text>();
    }

    public void SetTestText(string inputFieldString)
    {
        playerName.text = inputFieldString;
    }

    public void StartGame()
    {
        if (playerName.text.Length >= 1)
        {
            PlayerPrefs.SetString("PlayerName", playerName.text);
            Application.LoadLevel("scene");
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", "Spieler");
            Application.LoadLevel("scene");
        }
    }
}