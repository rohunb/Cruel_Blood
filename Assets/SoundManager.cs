using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	// Use this for initialization
	public static SoundManager instance;
	public static bool created;
	public bool SoundOn;
	void Awake()
	{

		if (!created) {
			// this is the first instance - make it persist
			DontDestroyOnLoad(this.gameObject);
			created = true;
		} else {
			// this must be a duplicate from a scene reload - DESTROY!
			Destroy(this.gameObject);
		} 
	}
	void Start () {
		instance = this;
		SoundOn = true;

	}
	
	// Update is called once per frame
	void Update () {

	}
}
