using UnityEngine;
using System.Collections;

public class EnemyArcher : RangedEnemy {
    
    public GameObject arrow;
    public float reloadTime=2.0f;
    private float currentTime;
   
    //public float range = 2f;
    //public float speed = 0.5f;
    //public bool markedForDeath;
	// Use this for initialization
	public override void Start () {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //currentTime = 0f;
        //markedForDeath = false;
        base.damage = 1;
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {


        if (LerpUntilInRange())
        {
            this.gameObject.GetComponentInChildren<RangerHand>().PlayAnimation();
            if (currentTime >= reloadTime)
            {
                
                Shoot();
            }
        }
        
        currentTime += Time.deltaTime;
      
        base.Update();

	}
    private void Shoot()
    {
        GameObject arrowClone = Instantiate(arrow, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation) as GameObject;
        Arrow arrowscript = arrowClone.GetComponent<Arrow>();
        GameObject.Destroy(arrowClone, 10.0f);
        if (arrowscript != null)
        {
            arrowscript.Owner = this.gameObject.tag;
            arrowscript.Damage = this.damage;
            Vector2 Direction = new Vector2(player.position.x - this.transform.position.x,player.position.y -  transform.position.y);
            arrowClone.rigidbody2D.velocity = Direction * (base.speed * 5);
            arrowClone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg) - 90));
            
        }
        //ProjectileDamager instance = arrowClone.GetComponent<ProjectileDamager>();
       // instance.damage = damage;
        this.gameObject.GetComponentInChildren<RangerHand>().StopAnimation();
        currentTime = 0f;
    }
}
