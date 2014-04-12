using UnityEngine;
using System.Collections;

public class guiTextChanger : MonoBehaviour {
    public static string textToWrite;

    GUIText guiText;
    void Awake()
    {
        guiText = GetComponent<GUIText>();
    }
	void Start()
    {
        textToWrite = "start";
        guiText.transform.position = Vector3.zero;
        guiText.pixelOffset = new Vector2(Screen.width / 2f, Screen.height/1.41f);
        guiText.fontSize = Mathf.RoundToInt(Screen.width / 51.0f);

    }
	// Update is called once per frame
	void Update () {
        guiText.text = textToWrite;
	}
}
