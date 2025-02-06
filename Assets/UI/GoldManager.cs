using TMPro;
using UnityEditor;
using UnityEngine;

public class GoldManager : Singleton<GoldManager>, IDataPersistence
{
    public int Gold { get; private set; }
    public TextMeshProUGUI _txt_gold;
    public DataPersistenceManager _dataPersistenceManager;

    private void Start()
    {
        this.Gold = _dataPersistenceManager.GetGold();
        UpdateUI_Gold();
    }

    public void IncreaseGold(int count)
    {
        if (count <= 0) return;
        Gold += count;
        SoundManager.Instance.UI_Coins();
        UpdateUI_Gold();
    }

    public bool DecreaseGold(int count)
    {
        if (Gold < count) return false;
        Gold -= count;
        UpdateUI_Gold();
        return true;
    }

    public void UpdateUI_Gold()
    {
        _txt_gold.text = Gold.ToString();
    }

    public void TestIncreaseGold()
    {
        IncreaseGold(1000);
        Debug.Log($"Gold increased by 10. Current Gold: {Gold}");
    }

    public void SaveData(ref GameData data)
    {
        data.Gold = this.Gold;
    }
}

//[CustomEditor(typeof(GoldManager))]
//public class GoldManagerEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        GoldManager goldManager = (GoldManager)target;

//        if (GUILayout.Button("Test Increase Gold"))
//        {
//            goldManager.TestIncreaseGold();
//        }
//    }
//}
