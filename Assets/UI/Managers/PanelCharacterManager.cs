using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCharacterManager : Singleton<PanelCharacterManager>, IDataPersistence
{
    public DataPersistenceManager       _dataPersistenceManager;
    public List<CharacterData>          _ListCharacterData;
    public Dictionary<string, Button>   _ListCharacterBtn;

    public Button                       _Button_CharacterJohn;
    public Button                       _Button_CharacterJames;
    public Button                       _Button_CharacterSheriff;
    public Button                       _Button_CharacterBob;
    public GameObject                   _imgSelected;
    public Button                       _btnSelect;

    public CharacterData                _characterIsSelected;
    public Character                    _character;

    protected override void InAwake()
    {
        AutoSelectCharacter();
    }

    void Start()
    {
        _ListCharacterData = _dataPersistenceManager.GetListCharacterData();
        InitListCharacterBtn();
        LoadListCharacter();
        DisableBtnSelect();
        
    }

    public void InitListCharacterBtn()
    {
        _ListCharacterBtn = new Dictionary<string, Button>
        {
            {"John", _Button_CharacterJohn},
            {"James", _Button_CharacterJames},
            {"Sheriff", _Button_CharacterSheriff},
            {"Bob", _Button_CharacterBob},
        };
    }

    public void DisableBtnSelect()
    {
        _btnSelect.interactable = false;
        _btnSelect.onClick.RemoveAllListeners();
    }

    public void EnableBtnSelect()
    {
        _btnSelect.interactable = true;
        _btnSelect.onClick.AddListener(SelectCharacter);
    }

    public void LoadListCharacter()
    {
        foreach (var characterData in _ListCharacterData)
        {
            if(characterData.IsUnlocked)
            {
                GameObject imgLock = _ListCharacterBtn[characterData.Name].transform.GetChild(1).gameObject;
                imgLock.SetActive(false);
                _ListCharacterBtn[characterData.Name].onClick.AddListener(() => CharacterIsSelected(characterData));
            }
            else
            {
                _ListCharacterBtn[characterData.Name].onClick.AddListener(() => UnlockCharacter(characterData.Name));
            }
        }
    }

    public void CharacterIsSelected(CharacterData characterData)
    {
        _characterIsSelected = characterData;
        EffectSelected();
        EnableBtnSelect();
        _character = Character.GetCharacter(characterData.Name);
        GameManager.Instance.CharacterChose(_character);
        if (_character == null)
        {
            Debug.LogError($"Failed to load character: {characterData.Name}");
            return;
        }

        FlexiblePanelUI.Instance.DisplayCharacterInfor(_character.Health, _character.Damage, _character.AttackSpeed, _character.MoveSpeed, _character.AttackRange, _character.PercentExpBonusEarn);
    }
    
    public void AutoSelectCharacter()
    {
        _character = Character.GetCharacter("John");
        GameManager.Instance.CharacterChose(_character);
        UpdateCharacterStats();
    }

    public void EffectSelected()
    {
        if (_imgSelected != null) _imgSelected.SetActive(false);
        _imgSelected = _ListCharacterBtn[_characterIsSelected.Name].transform.GetChild(2).gameObject;
        _imgSelected.SetActive(true);
    }

    public void UnlockCharacter(string characterName)
    {
        foreach (var characterData in _ListCharacterData)
        {
            if(characterData.Name == characterName && !characterData.IsUnlocked)
            {
                if (GoldManager.Instance.Gold < characterData.Cost)
                {
                    NotificationManagerUI.Instance.ShowClassicNotification("NOT ENOUGH GOLD");
                    return;
                }
                _characterIsSelected = characterData;
                NotificationManagerUI.Instance.ShowVerifyNotification("ARE YOU SURE WANT TO UNLOCK ?");
                NotificationManagerUI._DelegateConfirmNoti += ConfirmUnlock;
            }
        }
    }

    public void SelectCharacter()
    {
        UpdateCharacterStats();
        PanelManager.Instance.ShowPanel("Panel_Play");
    }

    public void UpdateCharacterStats()
    {
        GameManager.Instance.playerInfor.ResetCharacterStats();
        GameManager.Instance.playerInfor.SetCharacterStats(_character.Health, _character.Damage);
    }

    public void ConfirmUnlock()
    {
        foreach(var characterData in _ListCharacterData)
        {
            if(characterData.Name == _characterIsSelected.Name)
            {
                characterData.IsUnlocked = true;
            }
        }
        GoldManager.Instance.DecreaseGold(_characterIsSelected.Cost);
    }

    public void SaveData(ref GameData data)
    {
        data.ListCharacter = this._ListCharacterData;
    }
}
