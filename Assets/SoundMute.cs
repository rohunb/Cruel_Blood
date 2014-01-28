using UnityEngine;
using System.Collections;

public class SoundMute : MonoBehaviour {

	// Use this for initialization

	public Texture Mute;
	public Texture unMute;
	bool muted;
	void Start () {
		this.guiTexture.texture = unMute;
	}
	
	// Update is called once per frame
	void Update () {
			
	
	}
	void OnMouseDown()
	{
		Debug.Log ("Mouse");
		if(!muted)
		{
			this.guiTexture.texture = Mute;
			muted = true;
		}else{ this.guiTexture.texture = unMute;
			muted = false;
		}
		SoundManager.instance.SoundOn = !muted;
	}
}
