using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public ShopTurretInformation BallistaTurret;
    public ShopTurretInformation BlasterTurret;
    public ShopTurretInformation CannonTurret;
    public ShopTurretInformation FireTurret;
    public ShopTurretInformation PoisonTurret;
    public ShopTurretInformation BuffTurret;

    public Text BallistaPrice;
    public Text BlasterPrice;

    public Text CannonPrice;

    public Text FirePrice;

    public Text PoisonPrice;

    public Text BuffPrice;

    BuildManager buildManager;

    Shop shopGO;

    private void Start () {
        buildManager = BuildManager.instance;
        GameObject gcGO = GameObject.Find ("Shop");
        shopGO = gcGO.GetComponent<Shop> ();
        BallistaPrice.text = shopGO.BallistaTurret.CostOfTurret.ToString ();    //Updates shop turret information on starting application
        BlasterPrice.text = shopGO.BlasterTurret.CostOfTurret.ToString ();
        CannonPrice.text = shopGO.CannonTurret.CostOfTurret.ToString ();
        FirePrice.text = shopGO.FireTurret.CostOfTurret.ToString ();
        PoisonPrice.text = shopGO.PoisonTurret.CostOfTurret.ToString ();
        //BuffPrice.text = shopGO.PoisonTurret.CostOfTurret.ToString ();

    }
    //Buttons for selecting turrets
    public void SelectBallistaTurret () {
        Debug.Log ("Ballista Turret Selected");
        buildManager.SelectTurret (BallistaTurret);
    }

    public void SelectBlasterTurret () {
        Debug.Log ("Blaster Turret Selected");
        buildManager.SelectTurret (BlasterTurret);
    }
    public void SelectBuffTurret () {
        Debug.Log ("Buff Turret Selected");
        buildManager.SelectTurret (BuffTurret);
    }
    public void SelectCannonTower () {
        Debug.Log ("Cannon Tower Selected");
        buildManager.SelectTurret (CannonTurret);
    }
    public void SelectFireTurret () {
        Debug.Log ("Fire Turret Selected");
        buildManager.SelectTurret (FireTurret);
    }
    public void SelectPoisonTurret () {
        Debug.Log ("Poison Turret Selected");
        buildManager.SelectTurret (PoisonTurret);
    }
}