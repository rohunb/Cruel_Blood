using UnityEngine;
using System.Collections;

public class WASDMove : MonoBehaviour {
    public float speed = 2.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal") * speed;// *Time.deltaTime;
        move.y = Input.GetAxis("Vertical") * speed;// *Time.deltaTime;
        rigidbody2D.velocity = move;
        
	}
}
