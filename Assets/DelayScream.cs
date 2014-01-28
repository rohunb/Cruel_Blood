using UnityEngine;
using System.Collections;

public class DelayScream : MonoBehaviour {

	// Use this for initialization
	public float DelayTime = 10	;
	float elapsedTime;
	public bool Repeating;
	void Start () {
		elapsedTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= DelayTime)
		{
			audio.Play();

			if(Repeating){elapsedTime=0;}
		}	
	}
}
