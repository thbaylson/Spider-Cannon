using UnityEngine;

[CreateAssetMenu(fileName = "Charge Jump Upgrade", menuName = "Spider Upgrades/Charge Jump")]
public class ChargeJump : Upgrade
{
    public override void Apply(SpiderLauncher spiderLauncher)
    {
        spiderLauncher.canCharge = true;
    }
}
