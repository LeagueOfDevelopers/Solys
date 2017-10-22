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

    public static bool IsPackBought(int firstScene)
    {
        return PlayerPrefsUtility.GetEncryptedInt("BoughtPack"+firstScene, 0) == 1;
    }

    public static void BuyPack(int firstScene)
    {
        PlayerPrefsUtility.SetEncryptedInt("BoughtPack" + firstScene, 1);
    }

    public static bool IsPackGroupUnlocked(int group)
    {
        return PlayerPrefsUtility.GetEncryptedInt("GroupUnlocked" + group, 0) == 1;
    }

    public static void UnlockPackGroup(int group)
    {
        PlayerPrefsUtility.SetEncryptedInt("GroupUnlocked" + group, 1);
    }

    public static int GetPower()
    {
        if (PlayerPrefsUtility.GetEncryptedInt("StartPowerValueUsed", 0) == 1)
            return PlayerPrefsUtility.GetEncryptedInt("CurrentPower", 0);
        else
        {
            PlayerPrefsUtility.SetEncryptedInt("StartPowerValueUsed", 1);
            PlayerPrefsUtility.SetEncryptedInt("CurrentPower", 60);
            return 60;
        }
    }

    public static void AddPower(int count = 30)
    {
        PlayerPrefsUtility.SetEncryptedInt("CurrentPower", count + GetPower());
    }

    public static bool SpendPower()
    {
        if(GetPower() > 0)
        {
            PlayerPrefsUtility.SetEncryptedInt("CurrentPower", GetPower()-1);
            return true;
        }

        return false;

    }
}
