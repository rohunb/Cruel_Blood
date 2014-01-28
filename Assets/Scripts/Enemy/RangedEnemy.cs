using UnityEngine;
using System.Collections;

public class RangedEnemy : Enemy {

    public float range;

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
    public override void Update()
    {
        //LerpUntilInRange();
        base.Update();
	
	}
    //returns true once in range
    public bool LerpUntilInRange()
    {
        if (Vector2.Distance(transform.position, player.position) > range)
        {
            transform.position = Vector2.Lerp(transform.position, player.position, speed * Time.deltaTime);
            return false;
        }
        else
            return true;

    }
}
