using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace GachaFeverAP.Utils;

public static class DataSaveIO
{
    public static List<int> GetUnlockGachas(string json)
    {
        return GetUnlockId(json, Constants.DataListName.Gacha, (int)Constants.BaseId.Gacha);
    }

    public static List<int> GetUnlockWeapons(string json)
    {
        return GetUnlockId(json, Constants.DataListName.Weapon, (int)Constants.BaseId.Weapon);
    }

    public static List<int> GetUnlockObjects(string json)
    {
        return GetUnlockId(json, Constants.DataListName.Object, (int)Constants.BaseId.Object);
    }

    private static List<int> GetUnlockId(string json, string tabName, int baseId)
    {
        var jsonObject = JObject.Parse(json);

        // Extract list from JSON
        var tabUnlock = (JArray)jsonObject[tabName];

        // Find indices with value 1 and add ID associate 
        var indices = new List<int>();

        if (tabUnlock == null) return indices;
        for (var i = 0; i < tabUnlock.Count; i++)
            if ((int)tabUnlock[i] == 1)
                indices.Add(i + baseId);

        return indices;
    }
}