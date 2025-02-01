using UnityEngine;

public static class GameDifficulty
{
    public static int Difficulty = (int)GameDifficultyEnum.None;
}

public enum GameDifficultyEnum
{
    Easy = 0,
    Medium = 1,
    Hard = 2,
    Testing = 3,
    None = -1
}
