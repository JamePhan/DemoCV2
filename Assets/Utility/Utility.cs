using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Utility
{
    public Sprite                   _icon;
    public TextMeshProUGUI          _stat;
    public TextMeshProUGUI          _upgradeStat;
    public Button                   _upgradeBtn;

    public Utility(Sprite icon, TextMeshProUGUI stat, TextMeshProUGUI upgradeStat, Button upgradeBtn)
    {
        _icon = icon;
        _stat = stat;
        _upgradeStat = upgradeStat;
        _upgradeBtn = upgradeBtn;
    }


}
