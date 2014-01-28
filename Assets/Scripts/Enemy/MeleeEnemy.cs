using UnityEngine;
using System.Collections;

public class MeleeEnemy : Enemy {

	Vector3 difference;
	Vector3 velocity; 
	public float attackRange=5.0f;
    private const float ATTACK_COUNTDOWN = 0.5f;
    private float timeTillAttack = ATTACK_COUNTDOWN;
   
	public override void Start () {
       
        CurrentWeapon = gameObject.GetComponentInChildren<Sword>();
        if (CurrentWeapon == null)
            CurrentWeapon = gameObject.GetComponentInChildren<Spear>();

        if (CurrentWeapon is Sword)
            CurrentWeapon.renderer.enabled = false;

        CurrentWeapon.collider2D.enabled = false;
		base.speed = 2f;
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {

		difference = player.transform.position - this.transform.position;
        if (EnemyState == EnemyStates.Hunting)
        {
            if (CanAttack)
            {
                if (difference.sqrMagnitude < attackRange)
                {
                    if (timeTillAttack <= 0)
                    {
                        Debug.Log("Attacl");
                        CurrentWeapon.renderer.enabled = true;
                        CurrentWeapon.collider2D.enabled = true;
                        CurrentWeapon.PerformAttack();
                        EnemyState = EnemyStates.Attacking;
                        timeTillAttack = ATTACK_COUNTDOWN;
                    }
                    else
                        timeTillAttack -= Time.deltaTime;
                }
                else
                {
                    velocity = difference.normalized * base.speed * Time.deltaTime;
                    transform.Translate(velocity.x, velocity.y, 0, Space.World);                  
                }
            }
            else
            {
                velocity = -difference.normalized * base.speed * Time.deltaTime;
                transform.Translate(velocity.x, velocity.y, 0, Space.World);
            }
        }

        if (EnemyState == EnemyStates.Attacking)
        {
            if (CurrentWeapon.finishedAnimation)
                EnemyState = EnemyStates.Hunting;
        }

        base.Update();
        
	}
   
}
