using UnityEngine;

public static class PrefsDriver {
    private static string GetLevelPrefName(int level)
    {
        return level.ToString() + "Level";
    }

    public static void SetStarsForLevel(int level, int stars)
    {
        PlayerPrefsUtility.SetEncryptedInt(GetLevelPrefName(level), stars);
    }

    public static int GetStarsForLevel(int level)
    {
        return PlayerPrefsUtility.GetEncryptedInt(GetLevelPrefName(level));
    }

    public static int[] SetStarsForLevelRange(int startLevel, int count)
    {
        int[] result = new int[count];
        for(int i = 0; i<count; i++)
        {
            result[i] = PlayerPrefsUtility.GetEncryptedInt(GetLevelPrefName(startLevel+i));
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
