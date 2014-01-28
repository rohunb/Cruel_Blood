using UnityEngine;
using System.Collections;

public class GrapplePlayer : MonoBehaviour {
    private GameObject player;
    private bool playerHooked;
    public float meleeRange=2.0f;
	// Use this for initialization
	void Start () {
        playerHooked = false;
        
	}
	
	// Update is called once per frame
	void Update () {
	    if(playerHooked)
        {
            player.transform.position = transform.position;
            
        }
        if(player && Vector2.Distance(player.transform.position,transform.position)<=meleeRange)
        {
            playerHooked = false;
            //player.GetComponent<PlayerWSADMovement>().enabled = true;

        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            //other.GetComponent<PlayerWSADMovement>().enabled = false;
            player = other.gameObject;
            playerHooked=true;
            
        }
    }
}
