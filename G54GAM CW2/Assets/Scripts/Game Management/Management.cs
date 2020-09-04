using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Management : MonoBehaviour {
    // Update is called once per frame
    public bool isGameOver = false;
    public GameObject Gameover;
    public GameObject WinScreen;
    void Update () {
        if (PlayerInfo.Lives <= 0 && isGameOver == false) {
            GameOver ();
        }
    }

    void GameOver () {
        isGameOver = true;
        Gameover.SetActive (true);
    }

    public void RestartLevel () {
        SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex);
        Gameover.SetActive (false);
    }

    public void LoadNextLevel () {
        if (SceneManager.GetActiveScene ().buildIndex + 1 == 4) {   //only 3 scenes, therefore activate winscreen after level 3 has been beat
            WinScreen.SetActive (true);
            return;
        }
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }
}