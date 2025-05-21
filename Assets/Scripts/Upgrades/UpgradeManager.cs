using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private SpiderLauncher spiderLauncher;
    [SerializeField] private UpgradeDatabase upgradeDB;
    [SerializeField] private TMP_Text goldAmountText;

    private int gold = 0;
    private HashSet<Upgrade> ownedUpgrades = new();

    private void Start()
    {
        spiderLauncher.OnRunEnded += HandleRunEnded;

        SaveGame.Load(out gold, out ownedUpgrades);
        UpdateGoldAmountText();
    }

    public bool Buy(Upgrade upgrade)
    {
        if (upgrade == null || ownedUpgrades.Contains(upgrade) || !CanAfford(upgrade.Cost)) return false;

        gold -= upgrade.Cost;
        ownedUpgrades.Add(upgrade);
        ApplyUpgrade(upgrade);

        UpdateGoldAmountText();
        SaveGame.Save(gold, ownedUpgrades);
        return true;
    }

    public bool CanAfford(int price) => gold >= price;

    public void AddGold(int amount)
    {
        // Never add negative gold.
        gold += Mathf.Max(0, amount);
        UpdateGoldAmountText();
        SaveGame.Save(gold, ownedUpgrades);
    }

    public void SetGold(int amount)
    {
        gold = amount;
        UpdateGoldAmountText();
        SaveGame.Save(gold, ownedUpgrades);
    }

    private void ApplyUpgrade(Upgrade def)
    {
    }

    private void UpdateGoldAmountText()
    {
        goldAmountText.text = $"Gold: {gold}";
    }

    private void HandleRunEnded(float distanceTraveled)
    {
        AddGold((int)distanceTraveled);
    }
}
