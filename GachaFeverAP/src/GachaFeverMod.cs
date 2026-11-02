using BepInEx;
using BepInEx.Logging;
using GachaFeverAP.Archipelago;
using GachaFeverAP.Patches;
using GachaFeverAP.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GachaFeverAP;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class GachaFeverMod : BaseUnityPlugin
{
    private const string PluginName = "Gacha Fever AP";
    private const string PluginVersion = "0.1.0";

    private const string ModDisplayInfo = $"{PluginName} v{PluginVersion}";
    private const string APDisplayInfo = $"Archipelago v{SessionManager.APVersion}";
    public static ManualLogSource Log;
    public static SessionManager sessionManager;

    private void Awake()
    {
        // Plugin startup logic
        Log = Logger;
        sessionManager = new SessionManager();
        LoadPatch.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        ArchipelagoConsole.LogMessage($"{ModDisplayInfo} loaded!");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnGUI()
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
                sessionManager.Connect();
        }
        // this is a good place to create and add a bunch of debug buttons
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Scene_Menu") return;
        MenuButtons.LockAll();
        MenuButtons.menuLocked = true;
    }
}