using UnityEngine;

[CreateAssetMenu(fileName = "Launch Info Upgrade", menuName = "Spider Upgrades/Launch Info")]
public class LaunchInfo : Upgrade
{
    public override void Apply(SpiderLauncher spiderLauncher)
    {
        spiderLauncher.showLaunchInfo = true;
    }
}
