using BepInEx;
using GachaFeverAP.Utils;
using UnityEngine;

namespace GachaFeverAP.Archipelago;

public static class LoginGUI
{
    public const string PluginName = "Gacha Fever AP";
    public const string PluginVersion = "0.1.0";

    public const string ModDisplayInfo = $"{PluginName} v{PluginVersion}";
    private static string APDisplayInfo => $"Archipelago v{SessionManager.APVersion}";
    public static SessionManager SessionManager;

    public static void Awake()
    {
        SessionManager = new SessionManager();
        ArchipelagoConsole.Awake();
        ArchipelagoConsole.LogMessage($"{ModDisplayInfo} loaded!");
    }

    public static void OnGUI()
    {
        // show the mod is currently loaded in the corner
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(Screen.width - 316, 16, 300, 20), ModDisplayInfo);
        GUI.contentColor = Color.white;
        ArchipelagoConsole.OnGUI();

        string statusMessage;
        // show the Archipelago Version and whether we're connected or not
        if (SessionManager.Authenticated)
        {
            // if your game doesn't usually show the cursor this line may be necessary
            // Cursor.visible = false;

            statusMessage = " Status: Connected";
            GUI.contentColor = Color.black;
            GUI.Label(new Rect(Screen.width - 316, 50, 300, 20), APDisplayInfo + statusMessage);
            GUI.contentColor = Color.white;
        }
        else
        {
            // if your game doesn't usually show the cursor this line may be necessary
            // Cursor.visible = true;

            statusMessage = " Status: Disconnected";
            GUI.contentColor = Color.black;
            GUI.Label(new Rect(Screen.width - 316, 50, 300, 20), APDisplayInfo + statusMessage);
            GUI.Label(new Rect(Screen.width - 316, 70, 150, 20), "Host: ");
            GUI.Label(new Rect(Screen.width - 316, 90, 150, 20), "Player Name: ");
            GUI.Label(new Rect(Screen.width - 316, 110, 150, 20), "Password: ");
            GUI.contentColor = Color.white;

            SessionManager.ServerData.Uri = GUI.TextField(new Rect(Screen.width - 166, 70, 150, 20),
                SessionManager.ServerData.Uri);
            SessionManager.ServerData.SlotName = GUI.TextField(new Rect(Screen.width - 166, 90, 150, 20),
                SessionManager.ServerData.SlotName);
            SessionManager.ServerData.Password = GUI.TextField(new Rect(Screen.width - 166, 110, 150, 20),
                SessionManager.ServerData.Password);

            // requires that the player at least puts *something* in the slot name
            if (GUI.Button(new Rect(Screen.width - 316, 130, 100, 20), "Connect") &&
                !SessionManager.ServerData.SlotName.IsNullOrWhiteSpace())
                SessionManager.Connect();
        }
        // this is a good place to create and add a bunch of debug buttons
    }
}