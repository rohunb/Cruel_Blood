using UnityEngine;
using System.Collections;

public class guiTextChanger : MonoBehaviour {
    public static string textToWrite;
	
	void Start()
    {
        textToWrite = "start";
    }
	// Update is called once per frame
	void Update () {
        guiText.text = textToWrite;
	}
}
