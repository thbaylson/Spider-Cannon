using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private SpiderLauncher spiderLauncher;
    [SerializeField] private UpgradeDatabase upgradeDB;
    public UpgradeDatabase UpgradeDB => upgradeDB;
    [SerializeField] private TMP_Text goldAmountText;

    private int gold = 0;
    private List<Upgrade> ownedUpgrades = new();

    public event Action<int> OnGoldChanged;

    private void Start()
    {
        spiderLauncher.OnRunEnded += HandleRunEnded;

        SaveGame.Load(out gold, out ownedUpgrades);
        foreach (var upgrade in ownedUpgrades)
        {
            upgrade.Apply(spiderLauncher);
        }
        UpdateGoldAmountText();
    }

    public int StackCount(Upgrade upgrade) => ownedUpgrades.Where(x => x.Name == upgrade.Name)?.Count() ?? 0;

    public bool Buy(Upgrade upgrade)
    {
        if (!CanBuy(upgrade, upgrade.Cost)) return false;

        gold -= upgrade.Cost;
        ownedUpgrades.Add(upgrade);
        upgrade.Apply(spiderLauncher);

        UpdateGoldAmountText();
        SaveGame.Save(gold, ownedUpgrades);
        return true;
    }

    public bool CanBuy(Upgrade upgrade, int price)
    {
        return upgrade != null && StackCount(upgrade) < upgrade.MaxStack && gold >= price;
    }

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

    public void SetUpgrades(List<Upgrade> upgrades)
    {
        ownedUpgrades = upgrades;
        SaveGame.Save(gold, ownedUpgrades);
    }

    private void UpdateGoldAmountText()
    {
        goldAmountText.text = $"Gold: {gold}";
        OnGoldChanged?.Invoke(gold);
    }

    private void HandleRunEnded(float distanceTraveled)
    {
        AddGold((int)distanceTraveled);
    }
}
