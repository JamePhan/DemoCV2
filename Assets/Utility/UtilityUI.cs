using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UtilityUI : MonoBehaviour
{
    public      Image                    _icon;
    public      TextMeshProUGUI          _txtStat;
    public      TextMeshProUGUI          _txtUpgradeStat;
    public      TextMeshProUGUI          _txtPrice;
    public      Button                   _btnUpgrade;

    public void SetUtilityUI(int stat, UtilitySO so)
    {
        _icon.sprite            = so.Icon;
        _txtStat.text           = stat.ToString();
        _txtUpgradeStat.text    = so.UpgradeStat.ToString();
        _txtPrice.text          = so.Price.ToString();
        _btnUpgrade.onClick.AddListener(Upgrade);
    }

    public void Upgrade()
    {

    }
}
