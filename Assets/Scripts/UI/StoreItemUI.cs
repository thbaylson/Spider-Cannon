using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Button buyButton;

    private Upgrade upgrade;
    private UpgradeManager upgradeManager;

    public void Show(Upgrade upgrade, UpgradeManager upgradeManager)
    {
        this.upgrade = upgrade;
        this.upgradeManager = upgradeManager;

        nameText.text = upgrade.Name;
        descriptionText.text = $"Description: {upgrade.Description}";
        costText.text = $"Cost: {upgrade.Cost} G";

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(TryBuy);

        RefreshUI();
    }

    private void TryBuy()
    {
        if (upgradeManager.Buy(upgrade))
        {
            RefreshUI();
        }
    }

    public void RefreshUI()
    {
        bool canBuy = upgradeManager.CanBuy(upgrade, upgrade.Cost);
        int stackCount = upgradeManager.StackCount(upgrade);

        buyButton.interactable = canBuy;
        var buttonText = stackCount > 0 ? "Owned" : "Buy";
        buyButton.GetComponentInChildren<TMP_Text>().text = $"{buttonText} {stackCount}/{upgrade.MaxStack}";
    }
}
