using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class MapSerializator : MonoBehaviour {
    
    public Vector2 MapSize=new Vector2(3,3);
	public GameObject[] types = new GameObject[Enum.GetValues(typeof(MapElement.ElementsType)).Length];
	public bool DeserializeOnLoad = true;
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
	{
		if(DeserializeOnLoad && SceneDataTransfer.Instance.LoadingLevelData.Length>0)
		{
			Deserialize(SceneDataTransfer.Instance.LoadingLevelData);
			SceneDataTransfer.Instance.LoadingLevelData = string.Empty;
		}
	}

	public string Serialize()
	{
		Map obj = new Map();
		for(int i = 0; i<transform.childCount; i++)
		{
			GameObject child = transform.GetChild(i).gameObject;
			MapElement element = child.GetComponent<MapElement>();
			if(element != null)
				obj.Add(element.GetSerializationObject());
		}
		string result = JsonUtility.ToJson(obj);
		return result;
	}

	public void Deserialize(string serializedString)
	{
		
		Map obj = JsonUtility.FromJson<Map>(serializedString);
		foreach(ElementSerializeObject element in obj)
		{
			var mapObj = Instantiate(types[(int)element.type],element.position,element.rotation,gameObject.transform);
			mapObj.GetComponent<MapElement>().isDragable = element.isDragable;
			mapObj.GetComponent<MapElement>().type = (MapElement.ElementsType)element.type;
		}
	}

	/*public static void Instantiate(MapElement.ElementsType type, Vector3 position)
	{
		var mapObj = Instantiate(types[(int)type],position,  ,gameObject.transform);
		mapObj.GetComponent<MapElement>().isDragable = element.isDragable;
		mapObj.GetComponent<MapElement>().type = (MapElement.ElementsType)element.type;
	}*/ ///TODO
}
