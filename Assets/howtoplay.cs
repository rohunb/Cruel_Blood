using UnityEngine;
using System.Collections;

public class howtoplay : MonoBehaviour {

	// Use this for initialization
	GameObject screen1;
	GameObject screen2;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown()
	{
		Application.LoadLevel("MainHelp");

	}
}
