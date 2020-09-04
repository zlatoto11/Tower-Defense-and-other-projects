using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUser : MonoBehaviour {
	public AnimationCurve curve;
	public float y;
	// Use this for initialization
	void Start () {
		y = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x,y +
			curve.Evaluate ((Time.time % curve.length)),
			transform.position.z);
	}
	private void OnCollisionEnter2D (Collision2D collision) {
		Application.LoadLevel (Application.loadedLevel);
	}
}