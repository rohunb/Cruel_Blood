using UnityEngine;
using System.Collections;

public class guiBloodCount : MonoBehaviour {

	public int bloodAmount=0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.guiText.text = "Blood: "+ StatsAndGlobals.Instance.CurrentLitresOfBlood;
	}
}
