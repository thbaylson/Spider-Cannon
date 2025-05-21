using UnityEngine;

[CreateAssetMenu(fileName = "Extra Jumps Upgrade", menuName = "Spider Upgrades/Extra Jump")]
public class ExtraJumps : Upgrade
{
    public override void Apply(SpiderLauncher spiderLauncher)
    {
        spiderLauncher.IncreaseMaxJumps(1);
    }
}
