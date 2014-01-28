using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(!SoundManager.instance.SoundOn)
		{
			audio.enabled = false;
		}else audio.enabled = true;

	
	}
}
