using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public Button _btnUpgrade;

    protected override void InAwake()
    {
        _btnUpgrade.onClick.AddListener(OpenUpgradeTable);
    }

    public void OpenUpgradeTable()
    {
        Debug.Log("OPEN");
    }
}
