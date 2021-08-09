using System;
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
    
    internal static InventoryGrid internalgrid;
    internal static GameObject internalElement;

    internal static InventoryGui test;
    private bool Iran = false;
    internal static RectTransform oldplayergrid;
    internal static GameObject newparent;

    internal void Awake()
    {
	    internalElement = m_Element;
	    internalgrid = m_Grid;
	    oldplayergrid = test.m_player;
	    newparent = test.m_player.gameObject;
	    m_PlayerRoot.transform.SetParent(newparent.transform);
	    m_PlayerRoot.transform.SetSiblingIndex(newparent.transform.GetSiblingIndex());
	    var thing = oldplayergrid.GetComponentInChildren<UIGroupHandler>().m_groupPriority;
	    test.m_player.GetComponentInChildren<InventoryGrid>().m_scrollbar = m_Grid.m_scrollbar;
	    PlayerGroup.m_groupPriority = thing;
	    test.m_player = m_PlayerRootRect;
	    test.m_playerGrid = m_Grid;
	    test.m_armor = Armor;
	    test.m_weight = Weight;
	    
	    test.m_dragGo = InventorySwapper.InventorySwapper.m_dragGo;
	    
	    test.m_uiGroups[1] = PlayerGroup;
	    test.m_dropButton = DropButton;
	    
	    
	    DropButton.onClick.AddListener(test.OnDropOutside);
		InventoryGrid tempgrid = m_Grid;
		tempgrid.m_onSelected = (Action<InventoryGrid, ItemDrop.ItemData, Vector2i, InventoryGrid.Modifier>)Delegate.Combine(tempgrid.m_onSelected, new Action<InventoryGrid, ItemDrop.ItemData, Vector2i, InventoryGrid.Modifier>(test.OnSelectedItem));
		InventoryGrid tempgrid2 = m_Grid;
		tempgrid2.m_onRightClick = (Action<InventoryGrid, ItemDrop.ItemData, Vector2i>)Delegate.Combine(tempgrid2.m_onRightClick, new Action<InventoryGrid, ItemDrop.ItemData, Vector2i>(test.OnRightClickItem));
		
		//Turn off the old UI things we dont wanna lookat anymore
		Destroy(oldplayergrid.transform.Find("PlayerGrid").gameObject);
		oldplayergrid.transform.Find("Darken").gameObject.SetActive(false);
		oldplayergrid.transform.Find("Bkg").gameObject.SetActive(false);
		oldplayergrid.transform.Find("Armor").gameObject.SetActive(false);
		oldplayergrid.transform.Find("Weight").gameObject.SetActive(false);

    }

    private void OnGUI()
    {
	    if (Player.m_localPlayer != null && Iran == false)
	    {
		    Iran = true;
		    InvGUIHook();
	    }

    }


    private void InvGUIHook()
    {
	    m_Grid.m_elements = oldplayergrid.gameObject.GetComponentInChildren<InventoryGrid>().m_elements;
	    m_Grid.m_elementSpace = 80;
    }
}
