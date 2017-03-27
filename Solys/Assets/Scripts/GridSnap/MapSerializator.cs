using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class MapSerializator : MonoBehaviour {

	public GameObject[] types = new GameObject[Enum.GetValues(typeof(MapElement.ElementsType)).Length];
	


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
			Instantiate(types[(int)element.type],element.position,element.rotation,gameObject.transform);
		}
	}
}
