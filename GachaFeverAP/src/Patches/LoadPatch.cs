using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace GachaFeverAP.Patches;

public static class LoadPatch
{
    // TODO patch OnLoad() with new data.json and data.txt path
    public static void Awake()
    {
        Harmony.CreateAndPatchAll(typeof(LoadPatch));
    }

    [HarmonyPatch(typeof(Sc_SaveData), "OnLoad")]
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var enumerator = instructions.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var instruction = enumerator.Current;
            if (instruction.opcode == OpCodes.Ldstr && instruction.operand.ToString() == "LOAD depuis data.json : ")
            {
                yield return new CodeInstruction(OpCodes.Ldstr, "OUI LOAD depuis custom_data.json : ");
                continue;
            }

            yield return instruction;
        }
    }
}