using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using GachaFeverAP.Utils;
using HarmonyLib;
using UnityEngine;

namespace GachaFeverAP.Patches;

[HarmonyPatch]
public static class LoadPatch
{
    public static void Awake()
    {
        Harmony.CreateAndPatchAll(typeof(LoadPatch));
    }

    [HarmonyPatch(typeof(Sc_SaveData), "OnLoad")]
    [HarmonyPrefix]
    private static bool OnLoad(Sc_SaveData __instance)
    {
        if (!File.Exists(Constants.ModDataJsonPath))
        {
            CreateNewDataJsonFile();
        }
        
        var save = JsonUtility.FromJson<SaveDataStruct>(File.ReadAllText(Constants.ModDataJsonPath));
        MethodInfo applySaveData = AccessTools.Method(typeof(Sc_SaveData), "ApplySaveData");
        applySaveData.Invoke(__instance, [save]);

        return false;
    }
    private static void CreateNewDataJsonFile()
    {
        Directory.CreateDirectory(Constants.ModDataDirectory);
        File.Copy(Constants.OriginalDataJsonPath, Constants.ModDataJsonPath);
    }
}