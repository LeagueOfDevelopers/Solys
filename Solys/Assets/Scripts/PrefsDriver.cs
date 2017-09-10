using UnityEngine;

public static class PrefsDrier {
    public static void SetStarsForLevel(string level, int stars)
    {
        PlayerPrefs.SetInt(level.ToString() + "Level", stars);
    }

    public static int GetStarsForLevel(string level)
    {
        return PlayerPrefs.GetInt(level.ToString() + "Level", 0);
    }

    public static int[] SetStarsForLevelRange(string startLevel, int count)
    {
        int[] result = new int[count];
        for(int i = 0; i<count; i++)
        {
            result[i] = PlayerPrefs.GetInt((startLevel+i).ToString() + "Level", 0);
        }
        return result;
    }
}
