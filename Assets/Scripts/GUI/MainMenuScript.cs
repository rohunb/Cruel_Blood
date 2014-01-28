using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

    
    public GUITexture gameStart;
    public bool isStartGameButton;
    // Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    void OnMouseDown()
    {
        if(isStartGameButton)
        {
            StatsAndGlobals.Instance.ResetStats();
            Application.LoadLevel("Scene1");
        }
    }
}
