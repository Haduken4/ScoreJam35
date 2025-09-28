using UnityEngine;

public static class GlobalGameData
{
    // Score and leaderboard
    public static int Score = 0;
    public static int PersonalBest = 0;
    public static string PlayerName = "";

    // Gameplay data
    public static int MinimumDieRoll = 1;

    public static void ResetToDefault(bool includeHighScore = false)
    {
        Score = 0;
        if(includeHighScore)
        {
            PersonalBest = 0;
            PlayerName = "";
        }
        MinimumDieRoll = 1;
    }
}
