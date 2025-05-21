using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGame
{
    class SaveFile
    {
        public int gold;
        public List<Upgrade> upgrades = new();
    }

    static string Path => System.IO.Path.Combine(Application.persistentDataPath, "save.json");

    public static void Save(int gold, List<Upgrade> owned)
    {
        var file = new SaveFile { gold = gold, upgrades = new(owned) };
        File.WriteAllText(Path, JsonUtility.ToJson(file));
    }

    public static void Load(out int gold, out List<Upgrade> owned)
    {
        gold = 0;
        owned = new List<Upgrade>();

        if (!File.Exists(Path)) return;

        var file = JsonUtility.FromJson<SaveFile>(File.ReadAllText(Path));
        gold = file.gold;
        owned = new List<Upgrade>(file.upgrades);
    }
}
