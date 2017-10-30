using UnityEngine;

public class ToolboxHandler : MonoBehaviour {

    private GameObject writer;
	// Use this for initialization
	void Start () {
        writer = GameObject.Find("LineWriter");
        Debug.Log(writer.name);
	}
	
	public void valueChange(int tool)
    {
        writer.GetComponent<LineWriter>().ChangeTool(tool);
    }
}
