using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [Header("Stats")]
    public      List<UtilitySO>       ListUtility;


    [Header("Referemce")]
    public      GameObject          _btnUpgradeCanvas;
    public      Button              _btnUpgrade;
    public      GameObject          _upgradePanel;
    public      GameObject          _content;

    [Header("Other")]
    public      float               _radius;
    private     ResourcesLoad       _load;

    protected override void InAwake()
    {
        _btnUpgrade.onClick.AddListener(OpenUpgradePanel);
        _radius = 3f;
        _load = new ResourcesLoad();
        ListUtility = new List<UtilitySO>(_load.LoadAllUtility());
    }

    private void OnEnable()
    {
        
    }

    private void FixedUpdate()
    {
        if(EnterArea()) _btnUpgradeCanvas.SetActive(true);
        else _btnUpgradeCanvas.SetActive(false);
    }

    public bool EnterArea()
    {
        Collider[] playerCol = Physics.OverlapSphere(transform.position, _radius, LayerMask.GetMask("Player"));
        if (playerCol.Length != 0)
        {
            return true;
        }
        return false;
    }

    public void OpenUpgradePanel()
    {
        _upgradePanel.SetActive(true);
        InitUtilities();
    }

    public void InitUtilities()
    {
        foreach (var utilitySO in ListUtility)
        {
            AssetsManager.Instance.InitPrefab(AssetsManager.Instance._utility, (GameObject utility) => 
            {
                utility.transform.SetParent(_content.transform);
                utility.transform.GetComponent<UtilityUI>().SetUtilityUI(7 ,utilitySO);
            });
        }
    }
}
