using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float speed = 100.0f;
	// Use this for initialization
	void Start () {
		Rigidbody r = GetComponent<Rigidbody> ();
		r.velocity = transform.forward * speed;
	}
}