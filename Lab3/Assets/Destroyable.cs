using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {
    GameController gameController;
    public int timeBonus = 10;
    // Use this for initialization
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // destroy this object
        DestroyObject(gameObject);
    }
    private void OnDestroy()
    {
        // tell the game controller
        if (gameController != null)
        {
            gameController.TargetDestroyed();
        }
    }
}
