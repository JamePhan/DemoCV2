using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>, IDataPersistence
{
    [Header("Preferences")]
    public      DataPersistenceManager   _dataPersistenceManager;
    public      GameManager              _gameManager;
    public      GameObject               _canvas;

    public      GameObject               _inventoryUIPrefab;
    public      GameObject               _inventoryUI;
    
    public      TextMeshProUGUI          _txtAtk;
    public      TextMeshProUGUI          _txtHp;
    public      DroppableInventoryUI     _weaponSlot;
    public      DroppableInventoryUI     _armorSlot;
    public      DroppableInventoryUI     _spiritSlot;
    public      DroppableInventoryUI     _locketSlot;
    public      DroppableInventoryUI     _ringSlot;
    public      DroppableInventoryUI     _braceletSlot;

    [Header("Inventory")]
    public      List<Inventory>         ListOfInventory             = new List<Inventory>();
    public      List<GameObject>        ListOfInventoryUI           = new List<GameObject>();
    private     List<string>            ListInventoryNames          = new List<string>();
    private     bool                    _isFirstTimeInit            = true;
    
    private void OnEnable()
    {
        Display();
    }

    public void Display()
    {
        ListInventoryNames  = _dataPersistenceManager.GetListInventoryName();
        ListOfInventory = GetListOfInventory();

        if (!_isFirstTimeInit) return;
        InitListOfInventoryUI();
        _isFirstTimeInit = false;
    }

    public void UpdatePlayerInfor()
    {
        int totalAtk = 0, weaponAtk = 0, ringAtk = 0;
        int totalHp = 0, weaponHp = 0, armorHp = 0, ringHp = 0;

        if (_weaponSlot.inventory != null)
        {
            Weapon weapon = (Weapon)_weaponSlot.inventory;
            weaponAtk = weapon.Damage;
            weaponHp = weapon.Health;
        }
        if (_armorSlot.inventory != null)
        {
            Armor ar = (Armor)_armorSlot.inventory;
            armorHp = ar.Health;
        }
        if (_ringSlot.inventory != null)
        {
            Ring ring = (Ring)_ringSlot.inventory;
            ringAtk = ring.Damage;
            ringHp = ring.Health;
        }
        totalAtk = weaponAtk + ringAtk;
        totalHp = weaponHp + armorHp + ringHp;
        _txtAtk.text = totalAtk + "";
        _txtHp.text = totalHp + "";
        GameManager.Instance.playerInfor.ResetInventoryStats();
        GameManager.Instance.playerInfor.SetInventoryStats(totalHp, totalAtk);
    }

    public void SaveData(ref GameData data)
    {
        data.ListInventory = this.ListInventoryNames;
    }

    public List<Inventory> GetListOfInventory()
    {
        List<Inventory> _newList = new List<Inventory>();
        foreach (string inventoryName in ListInventoryNames)
        {
            _newList.Add(GetInventoryData(inventoryName));
        }
        return _newList;
    }

    public Inventory GetInventoryData(string weaponName)
    {
        InventorySO so = Resources.Load<InventorySO>("Inventory/" + weaponName);
        switch (so.Kind)
        {
            case InventoryKind.Weapon:
                return new Weapon(so);

            case InventoryKind.Armor:
                return new Armor(so);

            case InventoryKind.Ring:
                return new Ring(so);
        }
        return null;
    }

    public void InitListOfInventoryUI()
    {
        foreach (var inventory in ListOfInventory) 
        {
            GameObject newWeapon = InitInventoryUIDisplay(inventory);
            ListOfInventoryUI.Add(newWeapon);
        }
    }

    public GameObject InitInventoryUIDisplay(Inventory inventory)
    {
        GameObject newInventoryUI = Instantiate(_inventoryUIPrefab);
        newInventoryUI.transform.SetParent(_inventoryUI.transform, false);
        newInventoryUI.name = "InventoryUI_" + inventory.Name;
        newInventoryUI.GetComponent<Image>().sprite = inventory.Icon;
        newInventoryUI.GetComponent<DraggableInventoryUI>().Init(_canvas.transform, inventory);
        switch (inventory)
        {
            case Weapon weapon:
                newInventoryUI.AddComponent<Button>().onClick.AddListener(() => FlexiblePanelUI.Instance.DisplayInventoryInfor(weapon.Damage, weapon.Health));
                break;

            case Armor armor:
                newInventoryUI.AddComponent<Button>().onClick.AddListener(() => FlexiblePanelUI.Instance.DisplayInventoryInfor(0, armor.Health));
                break;

            case Ring ring:
                newInventoryUI.AddComponent<Button>().onClick.AddListener(() => FlexiblePanelUI.Instance.DisplayInventoryInfor(ring.Damage, ring.Health));
                break;
        }
        return newInventoryUI;
    }

}
