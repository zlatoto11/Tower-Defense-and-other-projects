using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hinge : MonoBehaviour {
	public bool hingeup = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Open () {
		Debug.Log ("Opening");
		StartCoroutine (HingeAnimation (gameObject.transform, gameObject.transform.position + new Vector3(0f,10f,0f), 1));
		hingeup = true;
	}

	public void Close () {
		Debug.Log ("Closing");
		//tartCoroutine (HingeAnimation (gameObject.transform, gameObject.transform.position + new Vector3(0f,-10f,0f), 1));
	}

	public IEnumerator HingeAnimation(Transform transform, Vector3 position, float timeToMove)
   {
      Vector3 currentPos = transform.position;
      float t = 0f;
       while(t < 1)
       {
             t += Time.deltaTime / timeToMove;
             transform.position = Vector3.Lerp(currentPos, position, t);
             yield return null;
      }
    }
}
