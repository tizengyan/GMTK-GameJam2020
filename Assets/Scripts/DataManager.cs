using UnityEngine;

public static class DataManager {
    public static int TotalScore { get; set; } = 0;
    public static int HighScore { get; set; } = 0;
    public static bool IsGameStart { get; set; } = false;

    public static void ClearData() {
        TotalScore = 0;
        IsGameStart = false;
    }

}
