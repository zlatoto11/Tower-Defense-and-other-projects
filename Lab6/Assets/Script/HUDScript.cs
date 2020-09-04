using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

	public Inventory inventory;
	// Use this for initialization
	void Start () {
		inventory.ItemAdded += InventoryItemAdded;
	}

	// Update is called once per frame
	void Update () {

	}

	private void InventoryItemAdded (object sender, InventoryEventArgs e) {
		Transform panel = transform.Find ("InventoryHud");
		foreach (Transform slot in panel) {
			Image image = slot.GetComponent<Image> ();
			// new
			InventoryItemClickable button = slot.GetComponent<InventoryItemClickable> ();

			if (!image.enabled) {
				image.enabled = true;
				image.sprite = e.item.itemImage;
				button.item = e.item; // new

				break;
			}
		}
	}
}