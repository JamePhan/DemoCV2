using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : Singleton<PanelManager>
{
    [Header("Buttons")]
    public Button _btn_Inventory;
    public Button _btn_Character;
    public Button _btn_Play;
    public Button _btn_Exit;

    [Header("Panels")]
    public List<PanelModel> ListOfPanel;

    private Queue<GameObject> _queue = new Queue<GameObject>();

    private void Start()
    {
        //ShowPanel("Panel_Play");
        _btn_Inventory.onClick.AddListener(() => ShowPanel("Panel_Inventory"));
        _btn_Character.onClick.AddListener(() => ShowPanel("Panel_Character"));
        _btn_Play.onClick.AddListener(() => ShowPanel("Panel_Play"));
        _btn_Exit.onClick.AddListener(() => Application.Quit());
    }

    public PanelModel GetPanelById(string panelId)
    {
        PanelModel panelModel = ListOfPanel.FirstOrDefault(panel => panel.PanelId == panelId);
        if (panelModel == null) 
        { 
            Debug.LogWarning($"Try to use panelId = {panelId}, but not found"); 
            return null;
        }
        return panelModel;
    }

    public void ShowPanel(string panelId)
    {
        PanelModel panelModel = GetPanelById(panelId);
        if (panelModel == null) return;

        if (AnyPanelIsShowing())
        {
            GameObject panel = _queue.Dequeue();
            panel.SetActive(false);
        }

        panelModel.PanelPrefab.SetActive(true);
        _queue.Enqueue(panelModel.PanelPrefab);
    }

    //public void HidePanel(string panelId)
    //{
    //    PanelModel panelModel = GetPanelById(panelId);
    //    if (panelModel == null) return;
    //    panelModel.PanelPrefab.SetActive(false);
        
    //}

    public bool AnyPanelIsShowing()
    {
        return GetAmountPanelsInQueue() > 0;
    }

    public int GetAmountPanelsInQueue()
    {
        return _queue.Count;
    }
}
