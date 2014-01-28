using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    public static float health=100f;
	
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("health " + health);
        if (health <= 0f)
        {
            Debug.Log("dead");
        }
	}
}
