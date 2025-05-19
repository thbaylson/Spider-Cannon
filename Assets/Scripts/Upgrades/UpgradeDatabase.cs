using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spider Upgrades/Upgrade Database")]
public class UpgradeDatabase : ScriptableObject
{
    public List<Upgrade> upgrades = new();

    public List<Upgrade> GetRandomNext(int amount)
    {
        var pool = new List<Upgrade>(upgrades);
        var result = new List<Upgrade>();
        for (int i = 0; i < amount; i++)
        {
            var pick = pool[Random.Range(0, pool.Count)];
            result.Add(pick);
            pool.Remove(pick);
        }
        return result;
    }
}
