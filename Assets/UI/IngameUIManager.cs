using UnityEngine;
using UnityEngine.UI;

public class IngameUIManager : MonoBehaviour
{
    public Button _btn_settings;

    void Start()
    {
        _btn_settings.onClick.AddListener(GetComponent<OpenPopup>().Open);
    }

}
