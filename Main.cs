using System;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
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
        public static GameObject m_dragGo;
        public static GameObject MyDragItem;
        public static GameObject MyContainer;
        private void Awake()
        {
            LoadAssets();
            GUIManager.OnPixelFixCreated += LoadInventoryWidget;
            m_dragGo = new GameObject();
            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginGUID);
        }

        private void OnDestroy()
        {
            Config.Save();
        }

        private void LoadAssets()
        {
            AssetBundle assetBundle = AssetUtils.LoadAssetBundleFromResources("inventory", typeof(InventorySwapper).Assembly);
            menu = assetBundle.LoadAsset<GameObject>("OldInventory");
            MyDragItem = assetBundle.LoadAsset<GameObject>("drag_itemz");
            MyContainer = assetBundle.LoadAsset<GameObject>("Container");
            assetBundle?.Unload(false);
        }

        private void LoadInventoryWidget()
        {
            if (SceneManager.GetActiveScene().name is not ("loading" or "main")) return;
            var thing = Instantiate(menu, GUIManager.PixelFix.transform, false);
            thing.transform.localPosition = new Vector3(0f, -83.06f, 0f);
            var Container2 = Instantiate(MyContainer, GUIManager.PixelFix.transform, false);
            Container2.transform.localPosition = new Vector3(0f, 0f, 0f);
        }

        [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.Awake))]
        public static class InventoryGui_Patch
        {
            public static void Postfix(InventoryGui __instance)
            {
                InventoryManager.test = __instance;
                ContainerManager.ContainerGUI = __instance;
                
                var font = __instance.m_dragItemPrefab.gameObject.transform.Find("amount").GetComponent<Text>();
                    font.font = MyDragItem.GetComponentInChildren<Text>().font;
                    font.fontSize = 120;
                    font.horizontalOverflow = HorizontalWrapMode.Overflow;
                    font.verticalOverflow = VerticalWrapMode.Overflow;
                    font.resizeTextForBestFit = false;
                    font.color = new Color(0.8196079f, 0.7882354f, 0.7607844f, 1f);
                    __instance.m_dragItemPrefab.gameObject.transform.Find("amount").gameObject.GetComponent<RectTransform>().localScale =
                        new Vector3(0.125f, 0.125f, 0);
                    
                __instance.m_dragItemPrefab.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(60f, 60f);
            }
        }
        
        [HarmonyPatch(typeof(InventoryGrid), nameof(InventoryGrid.UpdateGui))]
        public static class InventoryGrid_UpdateGui_w
        {
            public static void Prefix(InventoryGrid __instance)
            {
                if (__instance.name == "PlayerGrid")
                {
                    __instance.m_gridRoot = InventoryManager.internalgrid.GetComponent<RectTransform>();
                    InventoryManager.internalgrid.m_onSelected = __instance.m_onSelected;
                    InventoryManager.internalgrid.m_onRightClick = __instance.m_onRightClick;
                    __instance.m_elementPrefab = InventoryManager.internalElement;
                    InventoryManager.internalgrid.m_uiGroup = __instance.m_uiGroup;
                }

                if (__instance.name == "ContainerGrid")
                {
                    __instance.m_gridRoot = ContainerManager.internalcontainer.GetComponent<RectTransform>();
                    ContainerManager.internalcontainer.m_onSelected = __instance.m_onSelected;
                    ContainerManager.internalcontainer.m_onRightClick = __instance.m_onRightClick;
                    __instance.m_elementPrefab = InventoryManager.internalElement;
                    ContainerManager.internalcontainer.m_uiGroup = __instance.m_uiGroup;
                }
            }
        }

    }


    
}