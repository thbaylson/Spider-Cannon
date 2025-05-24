using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    [SerializeField] private Image chargeBar;
    [SerializeField] private GameObject chargeBarParent;

    [SerializeField] private float minForce = 500f;
    [SerializeField] private float maxForce = 2000f;
    [SerializeField] private float chargeRate = 10f;
    private float maxCharge = 100f;
    private float currentCharge = 0;

    void Start()
    {
        currentCharge = 0f;
        UpdateChargeBar();
    }

    public void Charge()
    {
        currentCharge += chargeRate * Time.deltaTime;
        // If the charge exceeds the max, reset it to 0.
        currentCharge = currentCharge > maxCharge ? 0f : currentCharge;
        UpdateChargeBar();
    }

    public float GetLaunchForce()
    {
        return Mathf.Lerp(minForce, maxForce, currentCharge / maxCharge);
    }

    public float GetCurrentCharge()
    {
        return currentCharge;
    }

    public void HandleLaunch()
    {
        currentCharge = 0f;
        HideUI();
    }

    public void SetMaxLaunchForce(float amount)
    {
        maxForce = amount;
    }

    public void SetChargeRate(float rate)
    {
        chargeRate = rate;
    }

    private void UpdateChargeBar()
    {
        chargeBar.fillAmount = currentCharge / maxCharge;
    }

    private void HideUI()
    {
        chargeBarParent.SetActive(false);
    }

    public void ShowUI()
    {
        currentCharge = 0f;
        UpdateChargeBar();
        chargeBarParent.SetActive(true);
    }
}
