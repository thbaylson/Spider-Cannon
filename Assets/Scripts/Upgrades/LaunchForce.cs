using UnityEngine;

[CreateAssetMenu(fileName = "Launch Force Upgrade", menuName = "Spider Upgrades/Launch Force")]
public class LaunchForce : Upgrade
{
    [SerializeField] private float newLaunchForce = 1000f;
    public override void Apply(SpiderLauncher spiderLauncher)
    {
        spiderLauncher.GetComponent<ChargeBar>().SetMaxLaunchForce(newLaunchForce);
    }
}
