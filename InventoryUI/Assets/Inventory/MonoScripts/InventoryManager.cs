using System;
using System.Collections.Generic;
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

    internal static InventoryGui test;

    internal void Awake()
    {
	    test = InventoryGui.instance;
	    
	    internalArmor = Armor;
	    internalWeight = Weight;
	    internladragitem = m_DragElement;
	    internalgrid = m_Grid;
	    InternalRectRootPlayer = m_PlayerRootRect;
	    test.m_player = m_PlayerRootRect;
	    test.m_playerGrid = m_Grid;
	    test.m_armor = Armor;
	    test.m_weight = Weight;
	    test.m_dragItemPrefab = m_DragElement;
	    test.m_uiGroups[1] = PlayerGroup;
	    test.m_dropButton = DropButton;
	    test.m_dragInventory = m_Grid.m_inventory;
	    test.m_splitInventory = m_Grid.m_inventory;
	    InventoryGrid tempgrid = m_Grid;
	    tempgrid.m_onSelected = (Action<InventoryGrid, ItemDrop.ItemData, Vector2i, InventoryGrid.Modifier>)Delegate.Combine(tempgrid.m_onSelected, new Action<InventoryGrid, ItemDrop.ItemData, Vector2i, InventoryGrid.Modifier>(test.OnSelectedItem));
	    InventoryGrid tempgrid2 = m_Grid;
	    tempgrid2.m_onRightClick = (Action<InventoryGrid, ItemDrop.ItemData, Vector2i>)Delegate.Combine(tempgrid2.m_onRightClick, new Action<InventoryGrid, ItemDrop.ItemData, Vector2i>(test.OnRightClickItem));

	    
	}
    

    private void Update()
    {
        if(Player.m_localPlayer != null)
            InvGUIHook();
    }
    
    
    private void InvGUIHook()
    {
	     
	     m_Grid.m_inventory = Player.m_localPlayer.GetInventory();
         m_Grid.m_elements = m_elements;
         m_Grid.m_elementSpace = 80;
         m_Grid.m_width = 4;
         m_Grid.m_height = 30;
         m_Grid.m_gridRoot = m_gridRoot;
         m_Grid.m_elementPrefab = m_Element;
         test.UpdateInventory(Player.m_localPlayer);
         test.UpdateItemDrag();
         test.UpdateInventoryWeight(Player.m_localPlayer);
	    
    }
}
