using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {
    public static BuildManager instance;
    private ShopTurretInformation turretToBuild;
    private Node selectedNode;
    public TurretManagementScript UIScript;
    void Awake () {
        if (instance != null) {
            Debug.Log ("More than one BM in Scene");
        }
        instance = this;
    }

    // 
    public bool CanBuild { get { return turretToBuild != null; } }  //checks if we can build on the current node
    public void SelectTurret (ShopTurretInformation selectedTurret) {   //Keeps a reference to the selected turret
        turretToBuild = selectedTurret;
        selectedNode = null;

        UIScript.Hide();    //Hide the turret upgrade script upon selecting a turret from the shop
    }
    public void selectNode(Node node){  //Selects the current node
        selectedNode = node;
        turretToBuild = null;

        UIScript.setNode(node);
    }

    public ShopTurretInformation GetTurretToBuild(){
        return turretToBuild;
    }

    
}