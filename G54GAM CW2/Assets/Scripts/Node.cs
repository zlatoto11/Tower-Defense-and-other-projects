using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    private Renderer rend;

    public GameObject turretSpawnlocation;
    [Header ("Optional")]
    public GameObject turret = null; // if turret exists on node
    public ShopTurretInformation turretToUpgradeTo;
    public bool isUpgraded = false;
    BuildManager buildManager;
    TurretManagementScript turretManagement;
    Turret _turretupgrade;

    private void Start () {
        buildManager = BuildManager.instance;
        GameObject UIGO = GameObject.Find ("SellandUpgradeUI");
        turretManagement = UIGO.GetComponent<TurretManagementScript> ();
        if (turret != null) {
            _turretupgrade = turret.GetComponent<Turret> (); //Using this to set Upgrade and Sell UI
        }
    }
    void OnMouseEnter () {
        if (!buildManager.CanBuild) {
            return;
        }
    }

    private void OnMouseDown () {
        if (turret != null) {
            buildManager.selectNode (this); //

            _turretupgrade = turret.GetComponent<Turret> ();
            turretManagement.upgradeTurretDetails (_turretupgrade.range, _turretupgrade.fireRate,
                _turretupgrade.damage, _turretupgrade.upgradeCost, turretToUpgradeTo.sellCost, _turretupgrade.level);

            return;
        }
        if (!buildManager.CanBuild) {
            return;
        }

        //Build Turret
        BuildTurret (buildManager.GetTurretToBuild ());
    }

    public Vector3 GetTurretSpawnLocation () {
        return turretSpawnlocation.transform.position;
    }

    void BuildTurret (ShopTurretInformation turretToBuild) {
        if (PlayerInfo.Credits < turretToBuild.CostOfTurret) {
            Debug.Log ("Not Enough Money to build this turret");
            return;
        }
        PlayerInfo.Credits -= turretToBuild.CostOfTurret;
        Debug.Log (turretToBuild.selectedPrefab);
        GameObject _turret = (GameObject) Instantiate (turretToBuild.selectedPrefab, GetTurretSpawnLocation (), Quaternion.identity);
        turret = _turret;

        turretToUpgradeTo = turretToBuild;

        Debug.Log ("Turret built! Money left : " + PlayerInfo.Credits);
    }

    public void TurretUpgrade () {
        Turret _turretupgrade = turret.GetComponent<Turret> ();
        if (PlayerInfo.Credits < _turretupgrade.upgradeCost) {
            Debug.Log ("Cant upgrade this turret");
            return;
        }
        PlayerInfo.Credits -= _turretupgrade.upgradeCost;
        _turretupgrade.fireRate += _turretupgrade.fireRateUpgrade;
        _turretupgrade.range += _turretupgrade.rangeUpgrade;
        _turretupgrade.damage += _turretupgrade.damageUpgrade;
        _turretupgrade.upgradeCost += _turretupgrade.upgradeInflation;
        _turretupgrade.level++;

        turretManagement.upgradeTurretDetails (_turretupgrade.range, _turretupgrade.fireRate,
            _turretupgrade.damage, _turretupgrade.upgradeCost, turretToUpgradeTo.sellCost, _turretupgrade.level);

        Debug.Log ("Turret Upgraded! Money left : " + PlayerInfo.Credits);
    }

    public void TurretSell () {
        Debug.Log (PlayerInfo.Credits);
        PlayerInfo.Credits += turretToUpgradeTo.sellCost;
        Debug.Log (PlayerInfo.Credits);
        Destroy (turret);
    }
}