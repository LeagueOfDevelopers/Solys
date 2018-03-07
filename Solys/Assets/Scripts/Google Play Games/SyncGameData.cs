using System.Collections;
using System.Collections.Generic;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SyncGameData : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(JsonUtility.ToJson(GetSaveData()));
	}



    public JsonSave GetSaveData()
    {
        JsonSave save = new JsonSave();
        save.availableStars = PrefsDriver.GetAvailableStars();
        save.power = PrefsDriver.GetPower();
        save.startPowerUsed = PrefsDriver.GetStartPowerUsed();

        List<int> starsForLevels_levels = new List<int>();
        List<int> starsForLevels_stars = new List<int>();
        List<int> packBought = new List<int>();
        List<int> packGroupUnlocked = new List<int>();

        for (int i = 3; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            int stars = PrefsDriver.GetStarsForLevel(i);
            bool isPackBought = PrefsDriver.IsPackBought(i);
            if (stars > 0)
            {
                starsForLevels_levels.Add(i);
                starsForLevels_stars.Add(stars);
            }
            if (isPackBought)
                packBought.Add(i);
        }

        for (int i = 0; i < 30; i++)
        {
            if (PrefsDriver.IsPackGroupUnlocked(i))
                packGroupUnlocked.Add(i);
        }

        save.starsForLevel_levels = starsForLevels_levels.ToArray();
        save.starsForLevel_stars = starsForLevels_stars.ToArray();
        save.packBought = packBought.ToArray();
        save.packGroupUnlocked = packGroupUnlocked.ToArray();

        return save;
    }

    public void LoadData(JsonSave save)
    {
        PrefsDriver.SetPower(save.power);
        PrefsDriver.SetAvailableStars(save.availableStars);
        PrefsDriver.SetStartPowerUsed(save.startPowerUsed);

        for(int i = 0; i<save.starsForLevel_levels.Length; i++)
            PrefsDriver.SetStarsForLevel(save.starsForLevel_levels[i],
                save.starsForLevel_stars[i]);

        foreach (int pack in save.packBought)
        {
            PrefsDriver.BuyPack(pack);
        }

        foreach (int group in save.packGroupUnlocked)
        {
            PrefsDriver.UnlockPackGroup(group);
        }
    }


}
