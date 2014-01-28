using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	
	public GUISkin skin;
	public bool Paused = false;
	public bool PauseButton;
	void Update(){
		if(Paused)
		{
			Time.timeScale = 0;

		}
		else
		{
			Time.timeScale = 1;

		}
		if(Input.GetKeyDown(KeyCode.P))
		{
			Paused=!Paused;
		}
	}
	void OnMouseDown()
	{
		if(PauseButton)
		{
			Paused=!Paused;
		}

	}
	void OnGUI(){
		
		if(Paused)
			Pause();
	}
	void Pause(){
		GUI.skin = skin;
		GUILayout.BeginArea(new Rect((Screen.width*0.5f)-50,(Screen.height*0.5f)-100,100,200));
		
		if (GUILayout.Button("Resume"))
		{
			Paused=!Paused;
		}
		if (GUILayout.Button("MainMenu"))
		{
			Application.LoadLevel(0);
		}
		if (GUILayout.Button("QuitGame"))
		{
			Application.Quit();
		}
		GUILayout.EndArea();
	}
	
}
