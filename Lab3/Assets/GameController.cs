using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public float timeLeft = 30.0f;
    public Text timeText;

    public Text Gamestatus;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString ("0");
        if (timeLeft < 0) {
            // format to a string with no decimal places
            Gamestatus.text = "YOU HAVE LOST!";
        }
        if (GameObject.FindObjectsOfType<Destroyable> ().Length == 0) {
            Gamestatus.text = "YOU HAVE WON!";
            Debug.Log("You HAVE WON");
        }
    }

    public void TargetDestroyed () {
        Debug.Log(GameObject.FindObjectsOfType<Destroyable>().Length);
        if (GameObject.FindObjectsOfType<Destroyable> ().Length >= 6) {
            timeLeft += 2;
        }
        else if (GameObject.FindObjectsOfType<Destroyable> ().Length <= 5) {
            timeLeft += 3;
        }
    }
}