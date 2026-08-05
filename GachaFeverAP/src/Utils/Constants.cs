using UnityEngine;

namespace GachaFeverAP.Utils;

public class Constants
{
    private const int BaseID = 50000;

    public enum BaseId
    {
        Gacha = BaseID,
        Weapon = BaseID + 100,
        Object = BaseID + 200,
        Filler = BaseID + 300
    }

    public static class DataListName
    {
        public const string Gacha = "tab_unlockHeroes";
        public const string Weapon = "tab_i_unlockWeapon";
        public const string Object = "tab_i_unlockItem";
    }

    public static readonly string ModDataDirectory = Application.persistentDataPath + "/Mod";
    public static readonly string ModDataJsonPath = Application.persistentDataPath + "/Mod/data.json";
    public static readonly string OriginalDataJsonPath = Application.persistentDataPath + "/data.json";
}