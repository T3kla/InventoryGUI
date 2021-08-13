using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitWindowManager : MonoBehaviour
{
    [SerializeField] private Slider InvSlider;
    [SerializeField] private GameObject SliderGO;
    [SerializeField] private Text SliderText;
    [SerializeField] private Button Cancel;
    [SerializeField] private Button OK;
    [SerializeField] private Image Icon;
    [SerializeField] private Text ItemName;
    
    internal static InventoryGui SplitGUI;
    void Awake()
    {
        var OldSplitter = SplitGUI.m_splitPanel.gameObject;
        OldSplitter.transform.Find("darken").gameObject.SetActive(false);
        OldSplitter.transform.Find("win_bkg").gameObject.SetActive(false);

        OldSplitter.SetActive(true);
        InventorySwapper.InventorySwapper.staticGUI.m_splitOkButton = OK;
        InventorySwapper.InventorySwapper.staticGUI.m_splitCancelButton = Cancel;
        InventorySwapper.InventorySwapper.staticGUI.m_splitAmount = SliderText;
        InventorySwapper.InventorySwapper.staticGUI.m_splitSlider = InvSlider;
        InventorySwapper.InventorySwapper.staticGUI.m_splitPanel = SliderGO.GetComponent<RectTransform>();
        InventorySwapper.InventorySwapper.staticGUI.m_splitIcon = Icon;
        InventorySwapper.InventorySwapper.staticGUI.m_splitPanel.gameObject.SetActive(false);
        InventorySwapper.InventorySwapper.staticGUI.m_splitIconName = ItemName;
        
        
        //Comment these 3 lines out to build prefabs
        OK.onClick.AddListener(InventorySwapper.InventorySwapper.staticGUI.OnSplitOk);
        Cancel.onClick.AddListener(InventorySwapper.InventorySwapper.staticGUI.OnSplitCancel);
        InvSlider.onValueChanged.AddListener(InventorySwapper.InventorySwapper.staticGUI.OnSplitSliderChanged);

        

        
        SliderGO.transform.SetParent(OldSplitter.transform);
        SliderGO.transform.SetSiblingIndex(OldSplitter.transform.GetSiblingIndex());

        Debug.Log("Split Manager Loaded");
    }
   
}
