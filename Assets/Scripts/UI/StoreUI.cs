using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private StoreItemUI itemPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private ScrollRect scrollViewRect;

    private List<StoreItemUI> displayedItems = new();

    void OnEnable()
    {
        Populate();
        upgradeManager.OnGoldChanged += RefreshAll;
        StartCoroutine(ForceLayoutRefresh());
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
        foreach (Upgrade upgrade in upgradeDB.upgrades)
        {
            StoreItemUI ui = Instantiate(itemPrefab, content);
            ui.Show(upgrade, upgradeManager);
            displayedItems.Add(ui);
        }

        StartCoroutine(ForceLayoutRefresh());
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

    // Force a layout refresh to ensure the scroll view is updated correctly. Populating a
    //  ScrollView at runtime can sometimes cause issues.
    private IEnumerator ForceLayoutRefresh()
    {
        yield return null;

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content);
        Canvas.ForceUpdateCanvases();

        scrollViewRect.verticalNormalizedPosition = 1f;
    }
}
