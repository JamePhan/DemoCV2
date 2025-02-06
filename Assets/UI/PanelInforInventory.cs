using TMPro;
using UnityEngine;

public class PanelInforInventory : MonoBehaviour
{
    public TextMeshProUGUI _txt_damage;
    public TextMeshProUGUI _txt_hp;

    public void InitInfor(int damage, int hp)
    {
        _txt_damage.text = damage + "";
        _txt_hp.text = hp + "";
    }
}
