using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class RaceController : MonoBehaviour {

	public Text resultText;
	public Text timeText;
	float startTime;
	GameObject car;
	string playerTag = "PlayerCar";
	public ScoreManager scoreManager;

	int lapcounter = 0;

	RaceState raceState;
	enum RaceState {
		START,
		RACING,
		FINISHED
	}
	void Start () {
		StartCoroutine (startCountdown ());
		raceState = RaceState.START;
	}
	IEnumerator startCountdown () {
		int count = 3;
		while (count > 0) {
			resultText.text = "" + count;
			count--;
			yield return new WaitForSeconds (1);
		}

		GameObject[] AICars = GameObject.FindGameObjectsWithTag ("AICar");
		foreach (GameObject car in AICars) {
			car.GetComponent<CarAIControl> ().enabled = true;
		}

		raceState = RaceState.RACING;
		startTime = Time.time;
		resultText.text = "GO";
		yield return new WaitForSeconds (1);
		resultText.enabled = false;

	}
	void Update () {
		if (raceState == RaceState.RACING) {
			timeText.text = "" + (Time.time - startTime);
		}
	}

	private void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			lapcounter++;
		}

		if (lapcounter >= 2) {
			Finish ();
		}
	}

	void Finish () {
		StopCoroutine (startCountdown ());
		raceState = RaceState.FINISHED;
		scoreManager.setTime ((Time.time - startTime),
			SceneManager.GetActiveScene ().buildIndex);
		SceneManager.LoadScene (0);
	}
}