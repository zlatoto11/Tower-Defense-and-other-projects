﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    float verticalVelocity = 0;
    public float playerRotateLeftRight = 3.0f, movementSpeed = 7.0f, playerLookUpDown = 5.0f;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    float y = 0.0f;
    public Inventory inventory;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        // rotate the player object about the Y axis
        float rotation = playerRotateLeftRight * Input.GetAxis ("Mouse X");
        transform.Rotate (0, rotation, 0);
        // rotate the camera (the player's "head") about its X axis
        float updown = -playerLookUpDown * Input.GetAxis ("Mouse Y");
        if (y + updown > 100|| y + updown < -100) {
            updown = 0;
        }
        y += updown;
        Camera.main.transform.RotateAround (transform.position, transform.right, updown);
        Camera.main.transform.LookAt (transform);
        // moving forwards and backwards
        float forwardSpeed = movementSpeed * Input.GetAxis ("Vertical");
        // moving left to right
        float lateralSpeed = movementSpeed * Input.GetAxis ("Horizontal");
        // apply gravity
        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        CharacterController characterController
            = GetComponent<CharacterController> ();
        if (Input.GetButton ("Jump") && characterController.isGrounded) {
            verticalVelocity = 5;
        }
        Vector3 speed = new Vector3 (lateralSpeed, verticalVelocity, forwardSpeed);
        // transform this absolute speed relative to the player's current rotation
        // i.e. we don't want them to move "north", but forwards depending on where
        // they are facing
        speed = transform.rotation * speed;
        // what is deltaTime?
        // move at a different speed to make up for variable framerates
        characterController.Move (speed * Time.deltaTime);

        /* if (Input.GetButton("Fire1") && Time.time > nextFire)
         {
             nextFire = Time.time + fireRate;
             Instantiate(shot,
             shotTransform.position,
             Camera.main.transform.rotation);
         }*/
    }

    private void OnControllerColliderHit (ControllerColliderHit hit) {
        IInventoryItem item = hit.gameObject.GetComponent<IInventoryItem> ();
        if (item != null) {
            inventory.addItem (item); // where inventory is the inventory object ref
        }
    }
}