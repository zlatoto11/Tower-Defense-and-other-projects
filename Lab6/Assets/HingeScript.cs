using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeScript : MonoBehaviour {
	public Inventory inventory;

	public bool inRange = false;
	public GameObject leverEnd;
	// Use this for initialization
	void Start () {
		inventory.ItemUsed += Inventory_ItemUsed;
	}
	
	void Inventory_ItemUsed (object sender, InventoryEventArgs e) {
		// check if the correct item is in use
		if ((e.item as MonoBehaviour).gameObject == leverEnd) {
			// check if in range
			if (inRange) {
				gameObject.GetComponent<Hinge> ().Open ();
			}
		}
	}
	private void OnTriggerEnter (Collider other) {
		inRange = true;
	}
	private void OnTriggerExit (Collider other) {
		inRange = false;
		if (gameObject.GetComponent<Hinge> ().hingeup) {
			gameObject.GetComponent<Hinge> ().Close();
		}
	}
}
