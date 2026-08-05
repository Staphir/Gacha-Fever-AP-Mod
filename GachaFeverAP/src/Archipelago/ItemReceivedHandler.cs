using Archipelago.MultiClient.Net.Models;
using GachaFeverAP.Utils;
using HarmonyLib;

namespace GachaFeverAP.Archipelago;

public static class ItemReceivedHandler
{
    public static void OnItemReceived(ItemInfo receivedItem)
    {
        // TODO add in custom data.json then OnLoad() 
        var itemId = receivedItem.ItemId;
        switch (itemId)
        {
            case < (int)Constants.BaseId.Weapon:
                DataSaveIO.AddUnlockGacha(Constants.ModDataJsonPath, itemId);
                break;
            case < (int)Constants.BaseId.Object:
                DataSaveIO.AddUnlockWeapon(Constants.ModDataJsonPath, itemId);
                break;
            case < (int)Constants.BaseId.Filler:
                DataSaveIO.AddUnlockObject(Constants.ModDataJsonPath, itemId);
                break;
            default:
                // filler
                break;
        }
        
        var saveData = UnityEngine.Object.FindAnyObjectByType<Sc_SaveData>();
        AccessTools.Method(typeof(Sc_SaveData), "OnLoad").Invoke(saveData, null);
    }
}