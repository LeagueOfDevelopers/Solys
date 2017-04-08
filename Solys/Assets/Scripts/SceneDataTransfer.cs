using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SceneDataTransfer
{
    #region Singleton
         static SceneDataTransfer(){}
         private static readonly SceneDataTransfer _instance = new SceneDataTransfer();
         public static SceneDataTransfer Instance{ get { return _instance; } }
    #endregion
   
   public bool LoadLevelData(int id)
   {
        TextAsset asset = Resources.Load("Level/"+id, typeof(TextAsset)) as TextAsset;
        if(asset == null) return false;
		string levelData = asset.text;
		Resources.UnloadAsset(asset);

		SceneDataTransfer.Instance.CurrentSceneID = id;
		SceneDataTransfer.Instance.LoadingLevelData = levelData;
		Debug.Log(levelData);
        return true;
   }

   public int CurrentSceneID;
   public string LoadingLevelData;
}
