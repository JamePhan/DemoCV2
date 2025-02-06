using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableInventoryBagUI : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        if (eventData.pointerDrag.TryGetComponent<DraggableInventoryUI>(out var draggableUI))
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
            if(draggableUI.previousParent.TryGetComponent<DroppableInventoryUI>(out var inven))
            {
                inven.inventory = null;
            }
            InventoryManager.Instance.UpdatePlayerInfor();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
