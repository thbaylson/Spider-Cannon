using UnityEngine;

[CreateAssetMenu(fileName = "Full Jump Angle Upgrade", menuName = "Spider Upgrades/Full Jump Angle")]
public class FullJumpAngle : Upgrade
{
    public override void Apply(SpiderLauncher spiderLauncher)
    {
        spiderLauncher.GetComponent<AngleArrow>()?.SetMaxAngle(180f);
    }
}
