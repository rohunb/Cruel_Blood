using UnityEngine;
using System.Collections;

public class howtoplay : MonoBehaviour {

	// Use this for initialization
	GameObject screen1;
	GameObject screen2;
    GUITexture howToPlayTexture;
	void Awake () {
        howToPlayTexture = GetComponent<GUITexture>();
	}
	void Start()
    {
        howToPlayTexture.transform.position = Vector3.zero;
        howToPlayTexture.pixelInset = new Rect((Screen.width / 2f) - (Screen.width / (4.3f*2)), (Screen.height / 2f) - (Screen.height / (4.3f*2f)) + Screen.height/12.0f , Screen.width / 4.3f, Screen.height / 4.3f);
        //howToPlayTexture.pixelInset = new Rect(-136, -40, Screen.width / 4.3f, Screen.height / 4.3f);
    }
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown()
	{
		Application.LoadLevel("MainHelp");

	}
}
