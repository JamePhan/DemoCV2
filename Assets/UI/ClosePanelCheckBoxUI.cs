using UnityEngine;
using UnityEngine.UI;

public class ClosePanelCheckBoxUI : Singleton<ClosePanelCheckBoxUI>
{
    public      Button          _btn;
    private     GameObject      _panelClosed;

    protected override void InAwake()
    {
        _btn.onClick.AddListener(CloseBoxUI);
        Disable();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Init(GameObject panelClosed)
    {
        this._panelClosed = panelClosed;
    }

    public void CloseBoxUI()
    {
        this._panelClosed.SetActive(false);
        Disable();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
