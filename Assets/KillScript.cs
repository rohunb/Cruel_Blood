using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour {

	// Use this for initialization

	public float time=1;
	float timeElapsed=0;

	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

		timeElapsed += Time.deltaTime;
		if(timeElapsed >= time)
		{
			Destroy(gameObject);
		}
	}

    void OnApplicationQuit()
    {
        Destroy(this.gameObject);
    }
}
