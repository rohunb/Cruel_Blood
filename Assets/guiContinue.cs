using UnityEngine;
using System.Collections;

public class guiContinue : MonoBehaviour {

    GUITexture guiTexture;
	// Use this for initialization
    void Awake()
    {
        guiTexture = GetComponent<GUITexture>();
    }
	void Start () {
        guiTexture.transform.position = Vector3.zero;
        guiTexture.pixelInset = new Rect(Screen.width / 1.37f, Screen.height / 1.58f, Screen.width / 4.55f, Screen.height / 7.86f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown()
	{
		Application.LoadLevel("Scene1");
	}
}
