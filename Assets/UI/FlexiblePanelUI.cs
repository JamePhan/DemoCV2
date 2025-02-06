using UnityEngine;

public class FlexiblePanelUI : Singleton<FlexiblePanelUI>
{
    public      RectTransform           _rect_canvas;

    [Header("Preferences InforInventory")]
    public      RectTransform           _rect_PanelInforInventory;
    public      GameObject              _panel_InforInventory;
    public      PanelInforInventory     _inforInventoryDisplay;

    private     Vector2                 _sizePanelInforInventory;

    [Header("Preferences InforCharacter")]
    public      RectTransform           _rect_PanelInforCharacter;
    public      GameObject              _panel_InforCharacter;
    public      PanelInforCharacter     _inforCharacterDisplay;

    private     Vector2                 _sizePanelInforCharacter;

    protected override void InAwake()
    {
        _sizePanelInforInventory = _rect_PanelInforInventory.sizeDelta;
        _sizePanelInforCharacter = _rect_PanelInforCharacter.sizeDelta;
    }

    public void DisplayInventoryInfor(int damage, int health)
    {
        _inforInventoryDisplay.InitInfor(damage, health);
        ShowPanel(Input.mousePosition, _sizePanelInforInventory, _rect_PanelInforInventory, true);

        ClosePanelCheckBoxUI.Instance.Init(_panel_InforInventory);
        ClosePanelCheckBoxUI.Instance.Enable();
    }

    public void DisplayCharacterInfor(int health, int dmg, float atkSpeed, float moveSpeed, float atkRange, int expBonus)
    {
        _inforCharacterDisplay.InitInfor(health, dmg, atkSpeed, moveSpeed, atkRange, expBonus);
        //ShowPanel(Input.mousePosition, _sizePanelInforCharacter, _rect_PanelInforCharacter, false);

        //ClosePanelCheckBoxUI.Instance.Init(_panel_InforCharacter);
        //ClosePanelCheckBoxUI.Instance.Enable();
    }

    public void ShowPanel(Vector2 position, Vector2 _sizePanel, RectTransform panel, bool isFlexibility)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rect_canvas,
            position,
            null,
        out anchoredPosition);

        Vector2 targetPosition = anchoredPosition + new Vector2(_sizePanel.x / 2, -_sizePanel.y / 2);

        if (anchoredPosition.x + _sizePanel.x > _rect_canvas.rect.width / 2)
        {
            targetPosition.x = anchoredPosition.x - _sizePanel.x / 2;
        }

        if (anchoredPosition.x - _sizePanel.x < -_rect_canvas.rect.width / 2)
        {
            targetPosition.x = anchoredPosition.x + _sizePanel.x / 2;
        }

        if (anchoredPosition.y - _sizePanel.y < -_rect_canvas.rect.height / 2)
        {
            targetPosition.y = anchoredPosition.y + _sizePanel.y / 2;
        }

        if (isFlexibility) panel.anchoredPosition = targetPosition;

        panel.gameObject.SetActive(true);
    }
}
