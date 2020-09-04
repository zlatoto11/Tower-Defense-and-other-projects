using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WIN : MonoBehaviour {

	public GameObject wingame;
	private void Start() {
		wingame.SetActive(false);
	}
	private void OnTriggerEnter(Collider other) {
		wingame.SetActive(true);
	}
}
