using UnityEngine;

public static class PrefsDriver {
    public static void SetStarsForLevel(int level, int stars)
    {
        PlayerPrefs.SetInt(level.ToString() + "Level", stars);
    }

    public static int GetStarsForLevel(int level)
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

    public static int GetSumOfStarsForLevelRange(int startLevel, int lastLevel)
    {
        int sum = 0;
        for (int i = startLevel; i <= lastLevel; i++)
            sum += GetStarsForLevel(i);
        return sum;
    }
}
