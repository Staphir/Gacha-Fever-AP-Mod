using BepInEx;
using BepInEx.Logging;
using GachaFeverAP.Archipelago;

namespace GachaFeverAP;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class GachaFeverMod : BaseUnityPlugin
{
    public static ManualLogSource Log;
    public static SessionManager SessionManager;

    private void Awake()
    {
        // Plugin startup logic
        Log = Logger;
        LoginGUI.Awake();
    }
}