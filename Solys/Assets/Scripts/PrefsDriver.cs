using UnityEngine;

public static class PrefsDriver {
    const string keyForAvailableStars = "AvailableStars";
    private static string GetLevelPrefName(int level)
    {
        return level.ToString() + "Level";
    }

    public static void SetStarsForLevel(int level, int stars)
    {
        if ((stars - GetStarsForLevel(level)) > 0)
        {
            AddAvailableStars(stars - GetStarsForLevel(level));
            PlayerPrefsUtility.SetEncryptedInt(GetLevelPrefName(level), stars);
            
        }
    }

    public static int GetStarsForLevel(int level)
    {
        return PlayerPrefsUtility.GetEncryptedInt(GetLevelPrefName(level));
    }
    public static bool SpendAvailableStars(int stars)
    {
        if (GetAvailableStars()>=stars)
        {
            PlayerPrefsUtility.SetEncryptedInt(keyForAvailableStars, GetAvailableStars()-stars);
            return true;
        }
        return false;
    }
    public static int GetAvailableStars()
    {
        return PlayerPrefsUtility.GetEncryptedInt(keyForAvailableStars);
    }
    public static void AddAvailableStars(int stars)
    {
        PlayerPrefsUtility.SetEncryptedInt(keyForAvailableStars, stars+ PlayerPrefsUtility.GetEncryptedInt("AvailableStars",0));
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
