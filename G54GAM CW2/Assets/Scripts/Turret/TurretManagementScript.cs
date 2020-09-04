using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretManagementScript : MonoBehaviour {
    public GameObject turretPanel;
    private Node selectedNode;
    public Text DamageText;
    public Text RangeText;
    public Text FireRateText;

    public Text UpgradeButtonText;

    public Text SellButtonText;
    public Text LevelText;
    public void setNode (Node _selectedNode) {
        selectedNode = _selectedNode;
        turretPanel.SetActive (true);
    }

    public void Hide () {
        turretPanel.SetActive (false);
    }

    public void Upgrade () {
        selectedNode.TurretUpgrade ();
    }

    public void Sell () {
        selectedNode.TurretSell ();
        Hide ();
    }
//Updating the Turret Upgrade and Sell UI when clicking on a node.
    public void upgradeTurretDetails (float range, float firerate, int damage, int upgradeCost, int sellCost, int TowerLevelText) {
        DamageText.text = "Damage: " + damage;
        RangeText.text = "Range: " + range;
        FireRateText.text = "Fire rate: " + firerate;
        UpgradeButtonText.text = "Upgrade Cost: " + upgradeCost;
        SellButtonText.text = "Sell Reimbursement: " + sellCost;
        LevelText.text = "Turret Level: " + TowerLevelText;
    }
}