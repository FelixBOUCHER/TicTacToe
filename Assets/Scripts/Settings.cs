/**************************************
* Author: Thomas Knox
* ~ Settings ~ 
* Keeps track of game settings;
**************************************/

public static class Settings
{
    /// <summary>
    /// The difficulty of the game AI;
    /// 0: Easy, 1: Normal, 2: Hard
    /// </summary>
    public static int difficulty = 1;

    /// <summary>
    /// true: Player One's Turn
    /// </summary>
    public static bool playerOneTurn = true;

    /// <summary>
    /// How many times the player has won;
    /// </summary>
    public static int wins = 0;

    /// <summary>
    /// How many times the player has lost;
    /// </summary>
    public static int losses = 0;

    /// <summary>
    /// How many times the player has tied;
    /// </summary>
    public static int ties = 0;

    /// <summary>
    /// Reset game settings;
    /// </summary>
    public static void Reset()
    {
        wins = 0;
        losses = 0;
        ties = 0;

        playerOneTurn = true;
    }
}