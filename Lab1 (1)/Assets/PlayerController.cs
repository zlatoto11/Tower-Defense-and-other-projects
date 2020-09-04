using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	
 	public float speed = 5.0f;
	public float xMin = -4.2f;
	public float xMax = 4.6f;
	public float zMin = -3.6f;
	public float zMax = 4.8f;
	public GameObject shot;
	public Transform shotTransform;

	public float fireRate = 0.5f;
	public float nextFire = 0.0f;
 // Start is called before the first frame update
	void Start()
	{
		//xMin = -4.2f;
		//xMax = 4.6f;
		//zMin = -3.6f;
		//zMax = 4.8f;
	}
	// Update is called once per frame
	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			Instantiate(
			shot,
			shotTransform.position,
			shotTransform.rotation);
		}
		float horizontalMovement = Input.GetAxis("Horizontal");
		float verticalMovement = Input.GetAxis("Vertical");
		//Debug.Log("Input: " + horizontalMovement + " " + verticalMovement);
		Rigidbody r = GetComponent<Rigidbody>();
		r.position = new Vector3( Mathf.Clamp(r.position.x, xMin, xMax),  r.position.y, Mathf.Clamp(r.position.z, zMin, zMax));

		r.velocity = new Vector3(
		horizontalMovement * speed, 0.0f, verticalMovement * speed);
		
	}

	void OnDestroy(){
		Application.LoadLevel(Application.loadedLevel);
	}
}