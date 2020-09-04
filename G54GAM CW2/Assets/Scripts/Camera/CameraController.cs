using UnityEngine;

public class CameraController : MonoBehaviour {

	public float panSpeed = 30f;
	public float panBorderThickness = 10f;

	public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 150f;
	public Vector2 panLimit;

	public Vector3 camCoords;


	private void Start() {
		camCoords = Camera.main.transform.position;
	}
	void Update () {
		//Starts scrolling the camera when close to the edge of the screen. Distance determined by panBorderThickness
		if (Input.mousePosition.y >= Screen.height - panBorderThickness) {
			transform.Translate (Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.mousePosition.y <= panBorderThickness) {
			transform.Translate (Vector3.back * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.mousePosition.x >= Screen.width - panBorderThickness) {
			transform.Translate (Vector3.right * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.mousePosition.x <= panBorderThickness) {
			transform.Translate (Vector3.left * panSpeed * Time.deltaTime, Space.World);
		}
		
		float scroll = Input.GetAxis ("Mouse ScrollWheel"); //For zooming in and out

		Vector3 pos = transform.position;

		pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
		pos.y = Mathf.Clamp (pos.y, minY, maxY);
		
		//Clamps the camera around the map
		pos.x = Mathf.Clamp (pos.x, -panLimit.x + camCoords.x, panLimit.x + camCoords.x);
		pos.z = Mathf.Clamp (pos.z, -panLimit.y + camCoords.y, panLimit.y + camCoords.y);

		transform.position = pos;

	}
}