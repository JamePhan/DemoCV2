using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UtilityUI : MonoBehaviour
{
    public      Image                    _icon;
    public      TextMeshProUGUI          _txtStat;
    public      TextMeshProUGUI          _txtUpgradeStat;
    public      TextMeshProUGUI          _txtPrice;
    public      Button                   _btnUpgrade;

    public      UtilitySO                _scriptableObject;
    private     float                    _upgradeStat;

    public void SetUtilityUI(UtilitySO so)
    {
        _scriptableObject       = so;
        _icon.sprite            = so.Icon;
        _txtStat.text           = GetCurrentStat().ToString(); 
        _upgradeStat            = so.UpgradeStat;
        _txtUpgradeStat.text    = so.UpgradeStat.ToString();
        CheckPrice();
        _txtPrice.text          = so.Price.ToString();
        _btnUpgrade.onClick.AddListener(Upgrade);
    }

    public float GetCurrentStat()
    {
        float currentStat = 0;
        switch (_scriptableObject.name)
        {
            case "Hp":
                currentStat = Mathf.Round(CharacterBehaviour2.Instance._character.Health * 1000f) / 1000f;
                break;

            case "Damage":
                currentStat = Mathf.Round(CharacterBehaviour2.Instance._character.Damage * 1000f) / 1000f;
                break;

            case "AtkSpeed":
                currentStat = Mathf.Round(CharacterBehaviour2.Instance._character.AttackSpeed * 1000f) / 1000f;
                break;

            case "AtkRange":
                currentStat = Mathf.Round(CharacterBehaviour2.Instance._character.AttackRange * 1000f) / 1000f;
                break;
        }

        return currentStat;
    }

    public void Upgrade()
    {
        switch (_scriptableObject.name)
        {
            case "Hp":
                CharacterBehaviour2.Instance.IncreaseHealth(_upgradeStat);
                Pay();
                UpdateCurrentStats();
                break;

            case "Damage":
                CharacterBehaviour2.Instance.IncreaseDamage(_upgradeStat);
                Pay();
                UpdateCurrentStats();
                break;

            case "AtkSpeed":
                CharacterBehaviour2.Instance.IncreaseAtkSpeed(_upgradeStat);
                Pay();
                UpdateCurrentStats();
                break;

            case "AtkRange":
                CharacterBehaviour2.Instance.IncreaseAtkRange(_upgradeStat);
                Pay();
                UpdateCurrentStats();
                break;
        }
    }

    public void UpdateCurrentStats()
    {
        _txtStat.text = GetCurrentStat().ToString();
    }

    public void CheckPrice()
    {
        if(GoldManager.Instance.Gold < _scriptableObject.Price) _btnUpgrade.interactable = false;
        else _btnUpgrade.interactable = true;
    }

    public void Pay()
    {
        GoldManager.Instance.DecreaseGold(_scriptableObject.Price);
        UpgradeManager.Instance.UpdateAllButton();
    }
}
