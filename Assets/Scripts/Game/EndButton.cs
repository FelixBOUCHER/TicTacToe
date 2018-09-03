/**************************************
* Author: Thomas Knox
* ~ EndButton ~ 
* Handles changing the state at the
*    end of the round;
**************************************/
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Menu: Returns to the Main Menu
    // Replay: Restarts the Game
    public enum Type
    {
        Menu, Replay
    }
    [Tooltip("The selection the button represents;")]
    public Type type;

    /******************************************
    * When the mouse is clicked on the object;
    ******************************************/
    public void OnPointerClick(PointerEventData eventData)
    {
        switch(type)
        {
            // load the menu
            case Type.Menu:
                Settings.Reset();
                SceneManager.LoadScene("Menu");
                break;
            // reload the game
            case Type.Replay:
                SceneManager.LoadScene("Game");
                break;
        }
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