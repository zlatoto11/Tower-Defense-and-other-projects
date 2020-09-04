using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHighScores : MonoBehaviour {

	public Text score;
	public ScoreManager scoreManager;
	// Use this for initialization
	void Start () {
		score.text = scoreManager.getTime().ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
