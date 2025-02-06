using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : Singleton<RewardManager>
{
    [Header("Preference")]
    public      GameObject                  _PanelReward;
    public      Image                       _first;
    public      Image                       _second;
    public      InventorySO                 _inventoryRewardFirst;
    public      InventorySO                 _inventoryRewardSecond;
    public      TextMeshProUGUI             _txt_coinReward;
    public      DataPersistenceManager      _dataPersistenceManager;
    public      Button                      _btn_reward;

    [Header("Stats")]
    public      int                         _coinReward;
    public      int                         _timeMinute;
    public      float                       _timeSecond;

    [Header("Others")]
    public      InventorySO[]               _allInventory;

    protected override void InAwake()
    {
        GetAllInventoryData();
        _btn_reward.onClick.AddListener(Reward);
    }

    private void GetAllInventoryData()
    {
        _allInventory = Resources.LoadAll<InventorySO>("Inventory");
    }

    public void GetRandomReward()
    {
        _timeMinute = GameManager.Instance._timeMinute;
        _timeSecond = GameManager.Instance._timeSecond;

        _PanelReward.SetActive(true);
        GetCoinReward();
        GetInventoryReward();
    }

    private void GetInventoryReward()
    {
        InventorySO[] quality;

        if(_timeMinute <= 2)
        {
            quality = GetInventory("Common");
        }
        else if(_timeMinute <= 5)
        {
            quality = GetInventory("Purple");
        }
        else
        {
            quality = GetInventory("Legendary");
        }
        int first = Random.Range(0, quality.Length - 1);
        int second = Random.Range(0, quality.Length - 1);
        _inventoryRewardFirst = quality[first];
        _inventoryRewardSecond = quality[second];
        _first.sprite = _inventoryRewardFirst.Icon;
        _second.sprite = _inventoryRewardSecond.Icon;

    }

    private InventorySO[] GetInventory(string quality)
    {
        return _allInventory.Where(item => item.Name.Contains(quality)).ToArray();
    }

    private void GetCoinReward()
    {
        if (_timeMinute <= 2)
        {
            _coinReward = Random.Range(100, 500);
        }
        else if (_timeMinute <= 4)
        {
            _coinReward = Random.Range(500, 1000);
        }
        else
        {
            _coinReward = Random.Range(1000, 10000);
        }
        _txt_coinReward.text = _coinReward.ToString();
    }

    public void Reward()
    {
        _dataPersistenceManager.AddInventory(_inventoryRewardFirst.Name);
        _dataPersistenceManager.AddInventory(_inventoryRewardSecond.Name);
        GoldManager.Instance.IncreaseGold(_coinReward);
        GameManager.Instance.ResetGame();
        _PanelReward.SetActive(false);
    }

}
