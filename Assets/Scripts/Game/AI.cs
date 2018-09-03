/**************************************
* Author: Thomas Knox
* ~ AI ~ 
* Handles the game and move logic;
**************************************/
using UnityEngine;
using System.Collections.Generic;

public class AI : MonoBehaviour
{
    // the game manager attached to the camera
    private GameManager manager;

    /******************************************
    * Runs once at the start;
    ******************************************/
    void Start()
    {
        // get the game manager
        manager = Camera.main.GetComponent<GameManager>();

        // if it's player two's turn from last match
        if (!Settings.playerOneTurn)
            Move();
    }

    /// <summary>
    /// Based on difficulty, decide how to move;
    /// </summary>
    public void Move()
    {
        // get free spaces
        List<int> freeSpaces = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            if (manager.spaces[i].text == "")
                freeSpaces.Add(i);
        }

        // based on difficulty, pick an appropriate space
        switch (Settings.difficulty)
        {
            case 0: Easy(freeSpaces); break;
            case 1: Normal(freeSpaces); break;
            case 2: Hard(freeSpaces); break;
        }
    }

    /*********************************************
    * Easy Difficulty: Pick a random space;
    *********************************************/
    void Easy(List<int> freeSpaces)
    {
        // if there is an open space
        if (freeSpaces.Count > 0)
        {
            // pick a random open space
            int random = Random.Range(0, freeSpaces.Count);

            // set the random space
            manager.SetSpace(freeSpaces[random]);
        }
    }

    /*********************************************
    * Normal Difficulty: Try to take a space
    *   adjacent to an ai occupied space;
    *********************************************/
    void Normal(List<int> freeSpaces)
    {
        // find all spaces filled by the ai
        List<int> aiSpaces = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            if (manager.spaces[i].text == manager.playerTwo)
                aiSpaces.Add(i);
        }

        // if the ai has gone
        if (aiSpaces.Count > 0)
        {
            // list of possible adjacent spaces that are empty
            List<int> adjacentEmpty = new List<int>();

            // check each space filled by ai
            for (int i = 0; i < aiSpaces.Count; i++)
            {
                /***********************
                * Horizontal Adjacent
                ***********************/
                // 0, 3, 6
                if (aiSpaces[i] % 3 == 0)
                {
                    // if the space to the right is empty
                    if (freeSpaces.Contains(aiSpaces[i] + 1))
                        adjacentEmpty.Add(aiSpaces[i] + 1);
                }
                // 1, 4, 7
                else if (aiSpaces[i] % 3 == 1)
                {
                    // if the space to the right or left is empty
                    if (freeSpaces.Contains(aiSpaces[i] + 1))
                        adjacentEmpty.Add(aiSpaces[i] + 1);
                    if (freeSpaces.Contains(aiSpaces[i] - 1))
                        adjacentEmpty.Add(aiSpaces[i] - 1);
                }
                // 2, 5, 8
                else if (aiSpaces[i] % 3 == 2)
                {
                    // if the space to the left is empty
                    if (freeSpaces.Contains(aiSpaces[i] - 1))
                        adjacentEmpty.Add(aiSpaces[i] - 1);
                }

                /***********************
                * Vertical Adjacent
                ***********************/
                // 0, 1, 2
                if (aiSpaces[i] <= 2)
                {
                    // if the space below is empty
                    if (freeSpaces.Contains(aiSpaces[i] + 3))
                        adjacentEmpty.Add(aiSpaces[i] + 3);
                }
                // 3, 4, 5
                else if (aiSpaces[i] <= 5)
                {
                    // if the space above or below is empty
                    if (freeSpaces.Contains(aiSpaces[i] + 3))
                        adjacentEmpty.Add(aiSpaces[i] + 3);
                    if (freeSpaces.Contains(aiSpaces[i] - 3))
                        adjacentEmpty.Add(aiSpaces[i] - 3);
                }
                // 6, 7, 8
                else if (aiSpaces[i] <= 8)
                {
                    // if the space above is empty
                    if (freeSpaces.Contains(aiSpaces[i] - 3))
                        adjacentEmpty.Add(aiSpaces[i] - 3);
                }
            }

            // if there are empty adjacent spaces
            if (adjacentEmpty.Count > 0)
            {
                // pick and set a random adjacent space
                int target = Random.Range(0, adjacentEmpty.Count);
                manager.SetSpace(adjacentEmpty[target]);

                return;
            }
        }

        // fallback - random space
        Easy(freeSpaces);
    }

    /*********************************************
    * Hard Difficulty: Try to take a space
    *   adjacent to an ai occupied space, as well
    *   as cut the player off;
    *********************************************/
    void Hard(List<int> freeSpaces)
    {
        // find all spaces filled by the player
        List<int> playerSpaces = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            if (manager.spaces[i].text == manager.playerOne)
                playerSpaces.Add(i);
        }

        // if the player has gone
        if (playerSpaces.Count > 0)
        {
            // list of possible second adjacent spaces that are empty
            List<int> cutoffSpaces = new List<int>();

            // check each space filled by the player
            for (int i = 0; i < playerSpaces.Count; i++)
            {
                /***********************
                * Horizontal Adjacent
                ***********************/
                // 0, 3, 6
                if (playerSpaces[i] % 3 == 0)
                {
                    // if the space to the right is also taken
                    if (playerSpaces.Contains(playerSpaces[i] + 1))
                        // if the space to the far right is open
                        if (freeSpaces.Contains(playerSpaces[i] + 2))
                            cutoffSpaces.Add(playerSpaces[i] + 2);
                }
                // 1, 4, 7
                else if (playerSpaces[i] % 3 == 1)
                {
                    // if the space to the right is also taken
                    if (playerSpaces.Contains(playerSpaces[i] + 1))
                        // if the space to the left is open
                        if (freeSpaces.Contains(playerSpaces[i] - 1))
                            cutoffSpaces.Add(playerSpaces[i] - 1);

                    // if the space to the left is also taken
                    if (playerSpaces.Contains(playerSpaces[i] - 1))
                        // if the space to the right is open
                        if (freeSpaces.Contains(playerSpaces[i] + 1))
                            cutoffSpaces.Add(playerSpaces[i] + 1);
                }
                // 2, 5, 8
                else if (playerSpaces[i] % 3 == 2)
                {
                    // if the space to the left is also taken
                    if (playerSpaces.Contains(playerSpaces[i] - 1))
                        // if the space to the far left is open
                        if (freeSpaces.Contains(playerSpaces[i] - 2))
                            cutoffSpaces.Add(playerSpaces[i] - 2);
                }

                /***********************
                * Vertical Adjacent
                ***********************/
                // 0, 1, 2
                if (playerSpaces[i] <= 2)
                {
                    // if the space below is also taken
                    if (playerSpaces.Contains(playerSpaces[i] + 3))
                        // if the space to the far bottom is open
                        if (freeSpaces.Contains(playerSpaces[i] + 6))
                            cutoffSpaces.Add(playerSpaces[i] + 6);
                }
                // 3, 4, 5
                else if (playerSpaces[i] <= 5)
                {
                    // if the space above is also taken
                    if (playerSpaces.Contains(playerSpaces[i] - 3))
                        // if the space below is open
                        if (freeSpaces.Contains(playerSpaces[i] + 3))
                            cutoffSpaces.Add(playerSpaces[i] + 3);

                    // if the space below is also taken
                    if (playerSpaces.Contains(playerSpaces[i] + 3))
                        // if the space above is open
                        if (freeSpaces.Contains(playerSpaces[i] - 3))
                            cutoffSpaces.Add(playerSpaces[i] - 3);
                }
                // 6, 7, 8
                else if (playerSpaces[i] <= 8)
                {
                    // if the space above is also taken
                    if (playerSpaces.Contains(playerSpaces[i] - 3))
                        // if the space to the far top is open
                        if (freeSpaces.Contains(playerSpaces[i] - 6))
                            cutoffSpaces.Add(playerSpaces[i] - 6);
                }
            }

            // if there are empty cutoff spaces
            if (cutoffSpaces.Count > 0)
            {
                // pick and set a random cutoff space
                int target = Random.Range(0, cutoffSpaces.Count);
                manager.SetSpace(cutoffSpaces[target]);

                return;
            }
        }

        // fallback - adjacent space
        Normal(freeSpaces);
    }
}