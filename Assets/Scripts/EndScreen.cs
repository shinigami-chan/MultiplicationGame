using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    Text endScreenText;

	void Start () {

        endScreenText = GameObject.Find("Text").GetComponent<Text>();

        endScreenText.text = "Deine Gesamtpunktzahl beträgt: " + PlayerPrefs.GetInt("PlayerPoints");
    }


}
