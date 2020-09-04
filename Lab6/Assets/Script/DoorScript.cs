using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	public Inventory inventory;

	public bool inRange = false;
	public GameObject key;

	public GameObject key2;
	void Start () {
		// register with the event handler
		inventory.ItemUsed += Inventory_ItemUsed;
	}
	void Inventory_ItemUsed (object sender, InventoryEventArgs e) {
		// check if the correct item is in use
		if ((e.item as MonoBehaviour).gameObject == key) {
			key = null;
		}
		if ((e.item as MonoBehaviour).gameObject == key2) {
			key2 = null;
		}

		if (key == null && key2 == null) {
			gameObject.GetComponent<Door> ().Open ();
		}
	}
	private void OnTriggerEnter (Collider other) {
		inRange = true;
	}
	private void OnTriggerExit (Collider other) {
		inRange = false;
		if (gameObject.GetComponent<Door> ().opened) {
			gameObject.GetComponent<Door> ().Close ();
		}
	}
}