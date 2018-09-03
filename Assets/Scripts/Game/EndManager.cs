/**************************************
* Author: Thomas Knox
* ~ EndManager ~ 
* Manages the end of the round menu;
**************************************/
using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{
    [Tooltip("Text UI for how many wins;")]
    public Text wins;

    [Tooltip("Text UI for how many losses;")]
    public Text losses;

    [Tooltip("Text UI for how many ties;")]
    public Text ties;

    /******************************************
    * Runs once at the start of the scene;
    ******************************************/
    void Start ()
    {
        // set the player scores
        wins.text = Settings.wins.ToString();
        losses.text = Settings.losses.ToString();
        ties.text = Settings.ties.ToString();
    }
}