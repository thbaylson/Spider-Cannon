using System.Collections.Generic;
using UnityEngine;

public class StoreUI : MonoBehaviour
{
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private StoreItemUI itemPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private int numItems = 1;

    private List<StoreItemUI> displayedItems = new();

    void OnEnable()
    {
        Populate();
        upgradeManager.OnGoldChanged += RefreshAll;
    }

    void OnDisable()
    {
        upgradeManager.OnGoldChanged -= RefreshAll;
        Clear();
    }

    private void Populate()
    {
        Clear();
        UpgradeDatabase upgradeDB = upgradeManager.UpgradeDB;
        foreach (Upgrade upgrade in upgradeDB.GetRandomNext(numItems))
        {
            StoreItemUI ui = Instantiate(itemPrefab, content);
            ui.Show(upgrade, upgradeManager);
            displayedItems.Add(ui);
        }
    }

    private void RefreshAll(int _)
    {
        foreach (StoreItemUI item in displayedItems)
        {
            item.RefreshUI();
        }
    }

    private void Clear()
    {
        foreach (StoreItemUI item in displayedItems)
        {
            Destroy(item.gameObject);
        }
        displayedItems.Clear();
    }
}
