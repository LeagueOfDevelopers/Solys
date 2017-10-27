using System;
using UnityEngine;

[Serializable]
public class ElementSerializeObject{
	public Vector3 position;
	public Quaternion rotation;
	public MapElement.ElementsType type;
	public bool isDragable;
}
