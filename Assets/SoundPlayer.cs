using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {

        if (SoundManager.instance.SoundOn)
        {
            Debug.Log("s");
            audio.Play();
        }
	}
	
	
}
