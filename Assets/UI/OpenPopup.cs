using UnityEngine;

public class OpenPopup : MonoBehaviour
{
    public GameObject popupPrefab;

    protected Canvas m_canvas;

    protected void Start()
    {
        m_canvas = GetComponentInParent<Canvas>();
    }

    public void Open()
    {
        var popup = Instantiate(popupPrefab) as GameObject;
        popup.SetActive(true);
        popup.transform.localScale = Vector3.zero;
        popup.transform.SetParent(m_canvas.transform, false);
        //popup.GetComponent<Popup>().Open();
    }
 
}
