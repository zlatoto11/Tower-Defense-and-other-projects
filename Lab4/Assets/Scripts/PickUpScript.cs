using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {

	public LayerMask layerMask;
	GameObject heldObject;
	public Transform holdPosition;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if (heldObject == null) {
				RaycastHit colliderHit;
				if (Physics.Raycast (transform.position,
						transform.forward,
						out colliderHit,
						10.0f,
						layerMask)) {
					heldObject = colliderHit.collider.gameObject;
					heldObject.GetComponent<Rigidbody> ().useGravity = false;
				}
			}
		}

		if (Input.GetButtonUp ("Fire1")) {
			// drop the object again
			if (heldObject != null) {
				heldObject.GetComponent<Rigidbody> ().useGravity = true;
				heldObject = null;
			}
		}
		if (heldObject != null) {
			// move the thing we're holding
			heldObject.GetComponent<Rigidbody> ().MovePosition (holdPosition.position);
			heldObject.GetComponent<Rigidbody> ().MoveRotation (holdPosition.rotation);
		}

		if (Input.GetButtonDown ("Fire2")) {
			if (heldObject != null) {
				heldObject.GetComponent<Rigidbody> ().useGravity = true;
				heldObject.GetComponent<Rigidbody> ().AddForce (transform.forward * 10.0f, ForceMode.Impulse);
				heldObject = null;
			}
		}
	}
}