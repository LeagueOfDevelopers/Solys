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

    public bool isNeedToLoad = true;

	// Use this for initialization
	void Start () {
        //OpenSavedGame();
	}


    void OpenSavedGame()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution("Save", DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (isNeedToLoad)
                Load();
            else
                Save();
                
        }
        else
        {
            // handle error
        }
    }

    private void Load()
    {

    }

    private void Save()
    {

    }

   


}
