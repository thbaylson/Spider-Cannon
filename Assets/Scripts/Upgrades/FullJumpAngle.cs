using UnityEngine;

[CreateAssetMenu(fileName = "Full Jump Angle Upgrade", menuName = "Spider Upgrades/Full Jump Angle")]
public class FullJumpAngle : Upgrade, IUpgrade
{
    public new string Name = "Full Jump Angle";
    public new string Description = "You can now angle your jumps from 0 to 180 degrees!";
    public new int Cost = 50;

    public void Apply()
    {
        Debug.Log("Full Jump Angle Upgrade Applied");
    }
}
