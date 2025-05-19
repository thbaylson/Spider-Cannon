using System;
using UnityEngine;

public class Upgrade : ScriptableObject
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
}

 public interface IUpgrade
{
    public void Apply();
}