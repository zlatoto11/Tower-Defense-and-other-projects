using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public void Open () {
		StartCoroutine(DoorAnimation(90,120));
		Debug.Log("Opening");
	}
	public void Close () {
		StartCoroutine(DoorAnimation(0,120));
		Debug.Log ("closing");
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