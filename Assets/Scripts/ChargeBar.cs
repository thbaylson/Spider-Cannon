using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    [SerializeField] private Image chargeBar;
    [SerializeField] private GameObject chargeBarParent;
    public float CurrentCharge { get; private set; }
    
    public float minForce = 5f;
    public float maxForce = 20f;
    private float maxCharge = 100f;
    public float chargeRate = 10f;

    private SpiderLauncher spiderLauncher;

    void Awake()
    {
        spiderLauncher = GetComponent<SpiderLauncher>();
        spiderLauncher.OnLaunched += HideUI;
    }

    void Start()
    {
        CurrentCharge = 0f;
        UpdateChargeBar();
    }

    private void UpdateChargeBar()
    {
        chargeBar.fillAmount = CurrentCharge / maxCharge;
    }

    void Update()
    {
        if (spiderLauncher.launched) return;

        // While the launch key is held down, charge the bar.
        if (Input.GetKey(KeyCode.Space))
        {
            CurrentCharge += chargeRate * Time.deltaTime;
            // If the charge exceeds the max, reset it to 0.
            CurrentCharge = CurrentCharge > maxCharge ? 0f : CurrentCharge;
            UpdateChargeBar();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            float launchForce = Mathf.Lerp(minForce, maxForce, CurrentCharge / maxCharge);
            spiderLauncher.Launch(launchForce);
        }
    }

    public void HideUI()
    {
        chargeBarParent.SetActive(false);
    }

    public void ShowUI()
    {
        CurrentCharge = 0f;
        UpdateChargeBar();
        chargeBarParent.SetActive(true);
    }
}
