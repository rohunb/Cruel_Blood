using UnityEngine;
using System.Collections;

public class DrawChain : MonoBehaviour {
    LineRenderer chain;
    public Transform startPos;
	// Use this for initialization
	void Start () {
        chain = GetComponent<LineRenderer>();
        //startPos = transform.position ;
	}
	
	// Update is called once per frame
	void Update () {
        chain.SetPosition(0, startPos.position);
        chain.SetPosition(1, transform.position);
	}

}
