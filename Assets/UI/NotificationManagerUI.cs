using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManagerUI : Singleton<NotificationManagerUI>
{
    public delegate void DelegateVerifyNotification();
    public static event DelegateVerifyNotification      _DelegateConfirmNoti;
    
    public GameObject                                   _pn_classicNoti;
    public TextMeshProUGUI                              _txt_classicNoti;

    public GameObject                                   _pn_verifyNoti;
    public TextMeshProUGUI                              _txt_verifyNoti;
    public Button                                       _btn_yes;
    public Button                                       _btn_no;

    public void ShowClassicNotification(string content)
    {
        _pn_classicNoti.SetActive(true);
        _txt_classicNoti.text = content;

        ClosePanelCheckBoxUI.Instance.Init(_pn_classicNoti);
        ClosePanelCheckBoxUI.Instance.Enable();
    }

    public void ShowVerifyNotification(string content)
    {
        _pn_verifyNoti.SetActive(true);
        _txt_verifyNoti.text = content;
        _btn_yes.onClick.AddListener(ConfirmVerifyNoti);
        _btn_no.onClick.AddListener(CancelVerifyNoti);
    }

    public void ConfirmVerifyNoti()
    {
        _DelegateConfirmNoti?.Invoke();
        DisableVerifyNoti();
    }

    public void CancelVerifyNoti()
    {
        DisableVerifyNoti();
    }

    public void DisableClassicNoti()
    {
        _pn_classicNoti.SetActive(false);
    }

    public void DisableVerifyNoti()
    {
        _pn_verifyNoti.SetActive(false);
    }
}
