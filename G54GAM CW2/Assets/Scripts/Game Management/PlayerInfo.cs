using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {
    public static int Credits;
    public int startCredits = 400;
    public static int Lives;
    public int startLives = 20;

    public Text creditsText;
    public Text livesText;

    private void Start () {
        Credits = startCredits;
        Lives = startLives;

        creditsText.text = "Credits: " + Credits;
        livesText.text = "Lives Remaining: " + Lives;
    }

    private void Update () {
        creditsText.text = "Credits: " + Credits;
        livesText.text = "Lives Remaining: " + Lives;
    }

}