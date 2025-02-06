using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableInventoryUI : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public      InventoryKind       kind;
    private     Image               image;
    private     RectTransform       rect;
    public      Inventory           inventory;

    private void Awake()
    {
        image       = GetComponent<Image>();
        rect        = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        if (eventData.pointerDrag.TryGetComponent<DraggableInventoryUI>(out var draggableUI))
        {
            if (draggableUI.GetInventoryKind() != kind) return;

            if (inventory != null)
            {
                if (transform.childCount > 0)
                {
                    if (transform.GetChild(0).TryGetComponent<DraggableInventoryUI>(out draggableUI)) draggableUI.BackToInventoryBag();
                }
            }

            this.inventory = draggableUI.inventory;
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
            InventoryManager.Instance.UpdatePlayerInfor();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }
}
