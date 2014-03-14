using UnityEngine;
using System.Collections;

public class GrapplePlayer : MonoBehaviour
{
    private Transform player;
    public bool playerHooked;
    public float meleeRange = 2.0f;
    public Transform ogre;
    OgreBoss ogreBoss;
    public bool canGrapple = true;
    public bool collidedWithArena = false;
    // Use this for initialization
    void Start()
    {
        playerHooked = false;
        ogreBoss = ogre.GetComponent<OgreBoss>();
        player = ogreBoss.player;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (playerHooked)
    //    {
    //        Vector3 playerPos = player.position;
    //        playerPos.x = transform.position.x;
    //        playerPos.y = transform.position.y;
    //        player.position = playerPos;

    //    }
    //    if (player && Vector2.Distance(player.position, ogre.position) <= meleeRange)
    //    {
    //        playerHooked = false;
    //        //player.GetComponent<PlayerWSADMovement>().enabled = true;

    //    }
    //}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && canGrapple)
        {
            //other.GetComponent<PlayerWSADMovement>().enabled = false;

            //player = other.transform;

            playerHooked = true;
            
        }
        if(other.tag=="Arena")
        {

            collidedWithArena = true;
        }
    }
    public void CannotGrapple()
    {
        canGrapple=false;
        playerHooked = false;
        Invoke("CanGrapple",0.3f);
    }
    void CanGrapple()
    {
        canGrapple=true;
    }
}
