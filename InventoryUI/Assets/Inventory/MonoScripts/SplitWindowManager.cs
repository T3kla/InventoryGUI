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
        SplitGUI.m_splitOkButton = OK;
        SplitGUI.m_splitCancelButton = Cancel;
        SplitGUI.m_splitAmount = SliderText;
        SplitGUI.m_splitSlider = InvSlider;
        SplitGUI.m_splitPanel = SliderGO.GetComponent<RectTransform>();
        SplitGUI.m_splitIcon = Icon;
        SplitGUI.m_splitPanel.gameObject.SetActive(false);
        SplitGUI.m_splitIconName = ItemName;
        
        OK.onClick.AddListener(SplitGUI.OnSplitOk);
        Cancel.onClick.AddListener(SplitGUI.OnSplitCancel);
        InvSlider.onValueChanged.AddListener(SplitGUI.OnSplitSliderChanged);

        

        
        SliderGO.transform.SetParent(OldSplitter.transform);
        SliderGO.transform.SetSiblingIndex(OldSplitter.transform.GetSiblingIndex());

        Debug.Log("Split Manager Loaded");
    }
   
}
