/**************************************
* Author: Thomas Knox
* ~ Menu ~ 
* Manages the state of the main menu;
**************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using CotcSdk;
using CotcSdkTemplate;

public class Menu : MonoBehaviour
{
    [Tooltip("The canvas holding the UI elements;")]
    public Transform canvas;
    private List<Text> ui = new List<Text>();

    [Tooltip("Speed at which to fade the UI out; ex. 0.9f")]
    public float fadeSpeed;

    // should the scene fade out
    private bool fade = false;

    /******************************************
    * Runs once at the start;
    ******************************************/
    void Start ()
    {
        // get all of the ui elements from the canvas
        int childCount = canvas.childCount;
        for(int i = 0; i < childCount; i++)
        {
            ui.Add(canvas.GetChild(i).GetComponent<Text>());
        }
	}

    /******************************************
    * Runs once per frame;
    ******************************************/
    void Update ()
    {

	}

    /// <summary>
    /// Start fading out the scene;
    /// </summary>
    public void FadeOut()
    {
        fade = true;
    }
}