using System;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public string name = "Character";
    public float MovementSpeed = 1f;
    public int InventoryCapacity = 4;
    public int maximumUpgrades = 3;
    public int currentUpgrades = 0;
    public Sprite uiUpgradeImage;
    public float hirePrice;
    public float upgradePrice;
    public float nextUpgradeCostOffset;

    public void Upgrade()
    {
        if (currentUpgrades > 0)
        {
            MovementSpeed -= currentUpgrades;
            InventoryCapacity -= currentUpgrades;
        }
        
        currentUpgrades = Mathf.Clamp(currentUpgrades + 1, 0, maximumUpgrades);
        InitializeUpgrades();
    }

    public void InitializeUpgrades()
    {
        MovementSpeed = MovementSpeed + (currentUpgrades/2);
        InventoryCapacity = InventoryCapacity + currentUpgrades;
    }
}
