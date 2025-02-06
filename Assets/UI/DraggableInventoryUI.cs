using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableInventoryUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public  Transform       canvas;
    public  Transform       previousParent;
    private RectTransform   rect;
    private CanvasGroup     canvasGroup;
    public  Inventory       inventory;

    public void Init(Transform canvas, Inventory inventory)
    {
        this.canvas         = canvas;
        this.inventory      = inventory;
        rect                = GetComponent<RectTransform>();
        canvasGroup         = GetComponent<CanvasGroup>();
    }

    public InventoryKind GetInventoryKind()
    {
        return inventory.Kind;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousParent = transform.parent;
        
        transform.SetParent(canvas);
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(transform.parent == canvas)
        {
            transform.SetParent(previousParent);
            rect.position = previousParent.GetComponent<RectTransform>().position;
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void BackToInventoryBag()
    {
        transform.SetParent(previousParent);
    }
}
