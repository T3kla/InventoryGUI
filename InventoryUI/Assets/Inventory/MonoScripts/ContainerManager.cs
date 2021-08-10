using UnityEngine;
using UnityEngine.UI;

public class ContainerManager : MonoBehaviour
{
    [SerializeField] private Text m_GraveText;
    [SerializeField] private Button m_TakeAll;
    [SerializeField] private InventoryGrid ContainerGrid;
    [SerializeField] private UIGroupHandler ContainerGroup;
    [SerializeField] private Text m_ContainerWeight;
    [SerializeField] private GameObject m_ContainerGO;


    internal static InventoryGrid internalcontainer;
    internal static GameObject OldContainer;
    internal static InventoryGui ContainerGUI;
    internal static Button takeallbutton;
    
    private void Awake()
    {
        internalcontainer = ContainerGrid;
        
        ContainerGUI.m_takeAllButton = m_TakeAll;
        ContainerGUI.m_containerName = m_GraveText;
        ContainerGUI.m_containerWeight = m_ContainerWeight;
        OldContainer = ContainerGUI.m_container.gameObject;
        ContainerGrid.m_elements = OldContainer.GetComponentInChildren<InventoryGrid>().m_elements;
        ContainerGUI.m_uiGroups[1] = ContainerGroup;
        
        m_TakeAll.onClick.AddListener(ContainerGUI.OnTakeAll);
        
        
        m_ContainerGO.transform.SetParent(OldContainer.gameObject.transform);
        m_ContainerGO.transform.SetSiblingIndex(OldContainer.gameObject.transform.GetSiblingIndex());
        
        OldContainer.transform.Find("Darken").gameObject.SetActive(false);
        OldContainer.transform.Find("selected_frame").gameObject.SetActive(false);
        OldContainer.transform.Find("Weight").gameObject.SetActive(false);
        OldContainer.transform.Find("Bkg").gameObject.SetActive(false);
        OldContainer.transform.Find("container_name").gameObject.SetActive(false);
        OldContainer.transform.Find("sunken").gameObject.SetActive(false);
        OldContainer.transform.Find("ContainerGrid").gameObject.SetActive(false);
        OldContainer.transform.Find("ContainerScroll").gameObject.SetActive(false);
        OldContainer.transform.Find("TakeAll").gameObject.SetActive(false);
        Debug.Log("Container Manager Loaded");
    }
}
