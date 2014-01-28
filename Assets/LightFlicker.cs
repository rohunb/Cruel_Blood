using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	// Use this for initialization
	float intensity;
	public float maxIntensity=4;
	public float minIntensity=2;
	void Start () {
		intensity = light.intensity;
	

	}
	
	// Update is called once per frame
	void Update () {
		intensity += Random.Range(-10,10)/50f;
		if(intensity<2)intensity = 2;
		if(intensity>4)intensity = 4;

		this.light.intensity = intensity;
	
	}
}
