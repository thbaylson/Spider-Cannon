using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Extra Jumps Upgrade", menuName = "Spider Upgrades/Extra Jump")]
public class ExtraJumps : Upgrade, IUpgrade
{
    public new string Name = "Extra Jump";
    public new string Description = "You get one additional jump!";
    public new int Cost = 200;

    public void Apply()
    {
        Debug.Log("Extra Jump Upgrade Applied");
    }
}
