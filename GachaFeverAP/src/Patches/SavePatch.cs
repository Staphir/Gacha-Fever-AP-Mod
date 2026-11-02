using HarmonyLib;

namespace GachaFeverAP.Patches;

[HarmonyPatch]
public class SavePatch
{
    // TODO patch OnSave() : replace by send object (how to patch configs like sound or screen size)
    // Note : Save on modDataJson call when receive new archipelago object
    
}