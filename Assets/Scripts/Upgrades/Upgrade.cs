using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public string Description { get; set; }
    [field: SerializeField] public int Cost { get; set; }
    [field: SerializeField] public int MaxStack { get; set; }
    public abstract void Apply(SpiderLauncher spiderLauncher);
}
