using UnityEngine;
using System.Collections;

public class HookLookAtPlayer : MonoBehaviour {

	// Use this for initialization
	Transform player;
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        OgreBoss ogreBoss = transform.parent.gameObject.GetComponent<OgreBoss>();

        if (!ogreBoss.throwingGrapple && !ogreBoss.pullingGrapple)
            LookAtPlayer();
	}
	public void LookAtPlayer()
	{
		float angle = Mathf.Atan2(player.position.y - transform.position.y,
		                          player.position.x - transform.position.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
		
	}
}
