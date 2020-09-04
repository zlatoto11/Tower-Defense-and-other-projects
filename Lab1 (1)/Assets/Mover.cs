using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
	public float speed = 5.0f;
	// Use this for initialization
	void Start () {
		 Rigidbody r =GetComponent<Rigidbody>();
		 r.velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
