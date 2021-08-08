using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerRoot;
    [SerializeField] private InventoryGrid m_Grid;
    [SerializeField] private RectTransform m_gridRoot;
    [SerializeField] private RectTransform m_PlayerRootRect;
    [SerializeField] private Text Armor;
    [SerializeField] private Text Weight;
    [SerializeField] private GameObject m_Element;
    [SerializeField] private Transform m_inventoryRoot;
    [SerializeField] private GameObject m_DragElement;
    [SerializeField] private UIGroupHandler PlayerGroup;
    [SerializeField] private Button DropButton;
    
    
    private int m_width = 4;
    private int m_height = 4;
    internal static List<InventoryGrid.Element> m_elements = new List<InventoryGrid.Element>();

    internal static Text internalArmor;
    internal static Text internalWeight;
    internal static GameObject internladragitem;
    internal static InventoryGrid internalgrid;
    internal static RectTransform InternalRectRootPlayer;
    

    internal void Awake()
    {
	    internalArmor = Armor;
	    internalWeight = Weight;
	    internladragitem = m_DragElement;
	    internalgrid = m_Grid;
	    InternalRectRootPlayer = m_PlayerRootRect;
	}
    

    private void LateUpdate()
    {
        if(Player.m_localPlayer != null)
            InvGUIHook();
    }
    
    
    private void InvGUIHook()
    {
	     InventoryGui.instance.m_player = m_PlayerRootRect;
	     InventoryGui.instance.m_playerGrid = m_Grid;
	     InventoryGui.instance.m_armor = Armor;
	     InventoryGui.instance.m_weight = Weight;
	     InventoryGui.instance.m_dragItemPrefab = m_DragElement;
	     InventoryGui.instance.m_uiGroups[1] = PlayerGroup;
	     InventoryGui.instance.m_dropButton = DropButton;
	     InventoryGui.instance.m_dragInventory = m_Grid.m_inventory;
	     InventoryGui.instance.m_splitInventory = m_Grid.m_inventory;
	     m_Grid.m_inventory = Player.m_localPlayer.GetInventory();
         m_Grid.m_elements = m_elements;
         m_Grid.m_elementSpace = 80;
         m_Grid.m_width = 4;
         m_Grid.m_height = 30;
         m_Grid.m_gridRoot = m_gridRoot;
         m_Grid.m_elementPrefab = m_Element;
         
    }
}
