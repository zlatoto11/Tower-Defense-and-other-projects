using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	bool canJump = true;
 	int groundMask = 1<<8;
	bool isIdle = true;
 	bool isLeft;
	int isIdleKey = Animator.StringToHash("isIdle");

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		Animator a = GetComponent<Animator>();
 		a.SetBool(isIdleKey, isIdle);
 		SpriteRenderer r = GetComponent<SpriteRenderer>();
 		r.flipX = isLeft;
	}

	void FixedUpdate(){
 	// the new velocity to apply to the character
 	Vector2 physicsVelocity = Vector2.zero;
 	Rigidbody2D r = GetComponent<Rigidbody2D>();
	 // move to the left
 	if (Input.GetKey(KeyCode.A)){
 		physicsVelocity.x -= 3;
		 isIdle = false;
		 isLeft = true;
 	}
	// implement moving to the right for the D key
	if (Input.GetKey(KeyCode.D)){
		physicsVelocity.x += 3;
		isIdle = false;
		isLeft = false;
	}

 	// this allows the player to jump, but only if canJump is true
 	if (Input.GetKey(KeyCode.W)){
		if (canJump){
 		r.velocity = new Vector2(physicsVelocity.x, 6);
 		canJump = false;
 		}
	}

	if (!Input.anyKey){
		isIdle = true;
	}
	// Test the ground immediately below the Player
	// and if it tagged as a Ground layer, then we allow the
	// Player to jump again.
	if (Physics2D.Raycast(new Vector2
			(transform.position.x,
				transform.position.y),
					-Vector2.up, 1.0f, groundMask))
	{
		canJump = true;
	}
	// apply the updated velocity to the rigid body
	r.velocity = new Vector2(physicsVelocity.x,
	r.velocity.y);
	}

}
