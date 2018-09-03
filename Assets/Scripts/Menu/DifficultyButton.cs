/**************************************
* Author: Thomas Knox
* ~ DifficultyButton ~ 
* UI Button; Selects game difficulty;
**************************************/
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class DifficultyButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum Difficulty
    {
        Easy, Normal, Hard
    }
    [Tooltip("The difficulty the button represents;")]
    public Difficulty difficulty;

    // the menu manager
    private Menu menu;

    /******************************************
    * Runs once at the start;
    ******************************************/
    void Start()
    {
        // get the menu manager from the camera
        menu = Camera.main.GetComponent<Menu>();
    }

    /******************************************
    * When the mouse is clicked on the object;
    ******************************************/
    public void OnPointerClick(PointerEventData eventData)
    {
        // set the new difficulty
        Settings.difficulty = (int)difficulty;

        SceneManager.LoadScene("Game");
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