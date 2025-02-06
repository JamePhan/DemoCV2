using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpbarFollow : MonoBehaviour
{
    public      TextMeshProUGUI         _txt_hp;
    public      Image                   _img_hpBar;
    private     RectTransform           _rectTransform;

    public void Init()
    {
        _rectTransform = GetComponent<RectTransform>();
        SpawnManager.playerPosDelegate += FollowPlayer;
    }

    public void FollowPlayer(Vector3 playerPosition)
    {
        _rectTransform.position = new Vector3(playerPosition.x, playerPosition.y + 3f, playerPosition.z - 1.5f);
    }

    public void UpdateHpBar(int hpCurrent, int hpMax)
    {
        _txt_hp.text = hpCurrent.ToString();
        _img_hpBar.fillAmount = (float) hpCurrent / hpMax;
    }
}
