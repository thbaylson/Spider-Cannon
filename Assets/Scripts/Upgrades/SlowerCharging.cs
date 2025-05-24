using UnityEngine;

[CreateAssetMenu(fileName = "Slower Charging Upgrade", menuName = "Spider Upgrades/Slower Charging")]
public class SlowerCharging : Upgrade
{
    [SerializeField] private float newChargeRate = 5f;
    public override void Apply(SpiderLauncher spiderLauncher)
    {
        spiderLauncher.GetComponent<ChargeBar>()?.SetChargeRate(newChargeRate);
    }
}
