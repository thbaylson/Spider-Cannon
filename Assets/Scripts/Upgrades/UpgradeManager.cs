using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    [SerializeField] private UpgradeDatabase upgradeDB;

    public int gold = 0;
    public HashSet<Upgrade> ownedUpgrades = new();
    
    private SpiderLauncher spiderLauncher;
    private TMP_Text goldAmountText;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }

        Instance = this;
        DontDestroyOnLoad(gameObject);        
    }

    private void Start()
    {
        spiderLauncher = FindFirstObjectByType<SpiderLauncher>();
        if (spiderLauncher == null)
        {
            Debug.LogError("SpiderLauncher not found in the scene.");
            return;
        }
        else
        {
            Debug.Log("SpiderLauncher found in the scene.");
        }

        spiderLauncher.OnRunEnded += HandleRunEnded;

        goldAmountText = GameObject.Find("GoldAmountText").GetComponent<TMP_Text>();
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
        gold += amount;
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
        gold += (int)distanceTraveled;
    }
}
