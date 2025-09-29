using System.Collections.Generic;
using System.Linq;
using BepInEx;
using EasyDeliveryAPI;
using UnityEngine;
using System;
using BepInEx.Logging;
using static DesktopDotExe;
using HarmonyLib;
using System.Reflection;
namespace BoblesIsStickers
{
    [HarmonyPatch]
    public class SnowcatPatch : HarmonyPatch
    {
        static string[] StickerLookup = ["Quad (10)", "Quad (11)", "Quad (5)", "Quad (4)", "Quad (3)", "Quad (2)", "Quad (6)", "Quad (7)", "Quad (8)", "Quad (1)", "Quad", "Quad (9)"];


        static MethodInfo TargetMethod()
        {
            return (typeof(SnowcatManager).GetMethod("SetBobbles", BindingFlags.NonPublic | BindingFlags.Instance));
        }
        static void Postfix(SnowcatManager __instance)
        {
            __instance.carDisplayBobble.transform.parent.Find("Stickers").gameObject.SetActive(true);
            for (int i = 0; i < __instance.snowcatsActive.Length - 1; i++)
            {
                __instance.carDisplayBobble.transform.parent.Find("Stickers").Find(StickerLookup[i]).gameObject.SetActive(__instance.snowcatsActive[i]);
            }
        }
    }
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    
    public class Bobles : BaseUnityPlugin
    {
        private void Awake()
        {
            HarmonyLib.Harmony harmony = new HarmonyLib.Harmony("GameRuleAPI");
            harmony.PatchAll();
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
