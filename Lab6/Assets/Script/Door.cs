using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public bool opened = false;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Open () {
		Debug.Log ("Opening");
		StartCoroutine (DoorAnimation (0, 20));
		opened = true;
	}

	public void Close () {
		Debug.Log ("Closing");
		StartCoroutine (DoorAnimation (90, 20));
	}

	private IEnumerator DoorAnimation (int targetAngle, int animationSpeed) {
		for (int r = 0; r < animationSpeed; r += 1) {
			Debug.Log("In Here");
			transform.localEulerAngles = new Vector3 (0,
				Mathf.LerpAngle (transform.localEulerAngles.y, targetAngle,
					5f / animationSpeed),
				0);
			yield return null;
		}
	}
}