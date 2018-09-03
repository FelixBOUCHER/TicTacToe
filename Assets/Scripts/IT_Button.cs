using UnityEngine;
using UnityEngine.EventSystems;

public class IT_Button : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    /******************************************
    * Runs once at the start;
    ******************************************/
	
    void Start()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    /******************************************
    * When the mouse enters the object;
    ******************************************/
    public void OnPointerEnter(PointerEventData eventData)
    {
        // grow
        transform.localScale = new Vector3(1.0f, 1.1f, 1.0f);
    }

    /******************************************
    * When the mouse exits the object;
    ******************************************/
    public void OnPointerExit(PointerEventData eventData)
    {
        // reset size
        transform.localScale = Vector3.one;
    }
}