using System;
using System.Collections.Generic;

[Serializable]
public class Map {
	public List<ElementSerializeObject> list;

	public Map()
	{
		list = new List<ElementSerializeObject>();
	}

	public void Add(ElementSerializeObject element)
	{
		list.Add(element);
	}

	public IEnumerator<ElementSerializeObject> GetEnumerator()
	{
		return list.GetEnumerator();
	}
}
