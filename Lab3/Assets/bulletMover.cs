using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMover : MonoBehaviour {
    public float speed = 20.0f;
    // Use this for initialization
    void Start () {
        Rigidbody r = GetComponent<Rigidbody>();
        r.velocity = transform.forward * speed;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        DestroyObject(gameObject);
    }
}
