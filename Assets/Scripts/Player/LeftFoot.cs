using UnityEngine;
using System.Collections;

public class LeftFoot : MonoBehaviour {

	// Use this for initialization
	float timeStep;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeStep += transform.parent.transform.rigidbody2D.velocity.magnitude/20;

		this.transform.localPosition = new Vector3(0.5f,Mathf.Sin(timeStep)/2.0f,0);

	}
}
