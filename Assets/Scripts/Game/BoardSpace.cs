/**************************************
* Author: Thomas Knox
* ~ BoardSpace ~ 
* Allows selection of a board space;
**************************************/
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardSpace : MonoBehaviour, IPointerClickHandler
{
    // the game manager attached to the camera
    private GameManager manager;

    // id or index of the board space
    private int id = -1;
    
    /******************************************
    * Runs once at the start;
    ******************************************/
    void Start ()
    {
        // get the game manager
        manager = Camera.main.GetComponent<GameManager>();

        // parse id from object name
        id = int.Parse(name);
	}

    /******************************************
    * When the mouse is clicked on the object;
    ******************************************/
    public void OnPointerClick(PointerEventData eventData)
    {
        manager.SetSpace(id);
    }
}