using UnityEngine;
using System.Collections;

public class RangerBoss : RangedEnemy {
    public GameObject arrow;
    public float reloadTime = 2.0f;
    private float currentTime;
	// Use this for initialization
	
    public override void Start () {
        base.damage = 1;
        base.range = 5;
        base.Start();
	}
	
	// Update is called once per frame
    public override void Update()
    {
        if (LerpUntilInRange() && currentTime >= reloadTime)
        {
            MultiShot();
        }
        currentTime += Time.deltaTime;
        
        base.Update();
	}
    private void MultiShot()
    {
        GameObject arrowClone1 = Instantiate(arrow, transform.position, transform.rotation) as GameObject;
        GameObject arrowClone2 = Instantiate(arrow, transform.position, 
            Quaternion.Euler(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z+30f)) as GameObject;
        GameObject arrowClone3 = Instantiate(arrow, transform.position,
            Quaternion.Euler(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z - 30f)) as GameObject;

        Arrow instance = arrowClone1.GetComponent<Arrow>();
        Vector2 Direction = new Vector2(player.position.x - this.transform.position.x, player.position.y - transform.position.y);
           
        if (instance != null)
        {
            instance.Owner = this.gameObject.tag;
            instance.Damage = this.damage;
             instance.rigidbody2D.velocity = Direction.normalized * (base.speed * 5);
            instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg) - 90));
            GameObject.Destroy(instance, 5.0f);
        }
        instance = arrowClone2.GetComponent<Arrow>();
        if (instance != null)
        {
            instance.Owner = this.gameObject.tag;
            instance.Damage = this.damage;
            instance.rigidbody2D.velocity = arrowClone2.transform.up * (base.speed * 5);
            GameObject.Destroy(instance, 5.0f);
          
        }
        instance = arrowClone3.GetComponent<Arrow>();
        if (instance != null)
        {
            instance.Owner = this.gameObject.tag;
            instance.Damage = this.damage;
            instance.rigidbody2D.velocity = arrowClone3.transform.up * (base.speed * 5);
            GameObject.Destroy(instance, 5.0f);
        }
        
        //GameObject.Destroy(instance, 10.0f);

       
        currentTime = 0f;
    }
}
