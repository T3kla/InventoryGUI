using System.Reflection;
using BepInEx;
using HarmonyLib;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InventorySwapper
{
    
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    public class InventorySwapper : BaseUnityPlugin
    {
        private GameObject menu;
        private Harmony _harmony;
        public const string PluginGUID = "com.odinplus.InventorySwap";
        public const string PluginName = "InventorySwap";
        public const string PluginVersion = "0.0.1";
        private void Awake()
        {
            LoadAssets();
            GUIManager.OnPixelFixCreated += LoadInventoryWidget;
        }

        private void LoadAssets()
        {
            AssetBundle assetBundle = AssetUtils.LoadAssetBundleFromResources("inventory", typeof(InventorySwapper).Assembly);
            menu = assetBundle.LoadAsset<GameObject>("OldInventory");
            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginGUID);
        }

        private void LoadInventoryWidget()
        {
            if (SceneManager.GetActiveScene().name == "main")
            {
                Instantiate(menu, GUIManager.PixelFix.transform, false);
            }
        }
        
        [HarmonyPatch(typeof(InventoryGrid), nameof(InventoryGrid.UpdateGui))]
        public static class InventoryGrid_UpdateGui_w
        {
            public static void Postfix(InventoryGrid __instance)
            {
                if (__instance.name == "PlayerGrid")
                {
                    __instance.m_gridRoot = InventoryManager.internalgrid.GetComponent<RectTransform>();
                    __instance.m_elements = InventoryManager.m_elements;
                    __instance.m_inventory = Player.m_localPlayer.m_inventory;
                  
                    InventoryManager.internalgrid.m_onRightClick = __instance.m_onRightClick;
                    InventoryManager.internalgrid.m_onSelected = __instance.m_onSelected;
                    InventoryManager.internalgrid.enabled = true;
                }
            }
                
        }

    }
}