using TMPro;
using UnityEngine;

public class PanelInforCharacter : MonoBehaviour
{
    public TextMeshProUGUI _txt_hp;
    public TextMeshProUGUI _txt_damage;
    public TextMeshProUGUI _txt_atkSpeed;
    public TextMeshProUGUI _txt_speed;
    public TextMeshProUGUI _txt_atkRange;
    public TextMeshProUGUI _txt_expBonus;

    public void InitInfor(int health, int damage, float atkSpeed, float moveSpeed, float atkRange, int expBonus)
    {
        _txt_hp.text = health + "";
        _txt_damage.text = damage + "";
        _txt_atkSpeed.text = atkSpeed + "";
        _txt_speed.text = moveSpeed + "";
        _txt_atkRange.text = atkRange + "";
        _txt_expBonus.text = expBonus + "";
    }
}
