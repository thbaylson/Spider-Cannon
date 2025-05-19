using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charge Jump Upgrade", menuName = "Spider Upgrades/Charge Jump")]
public class ChargeJump : Upgrade, IUpgrade
{
    public new string Name = "Charge Jump";
    public new string Description = "You can now charge your jumps!";
    public new int Cost = 10;

    public void Apply()
    {
        Debug.Log("Charge Jump Upgrade Applied");
    }
}
