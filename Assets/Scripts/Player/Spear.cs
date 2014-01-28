using UnityEngine;
using System.Collections;

public class Spear : Weapon
{
    private Animation ani;

    // Use this for initialization
    protected override void Start()
    {
        base.AttackSpeed = 10.0f;
        ani = this.GetComponent<Animation>();
        ani.Stop();
        //  animation.animation.Stop();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (BeginAttack)
        {
            this.gameObject.renderer.enabled = true;
            if (this.gameObject.GetComponent<Collider2D>() != null)
            {
                this.gameObject.collider2D.enabled = true;
            }
            ani.Play();
            //transform.position += transform.up * 0.5f;
            BeginAttack = false;
            doAttack = true;

        }

        if (doAttack)
        {
            if (!ani.isPlaying)
            {
                finishedAnimation = true;
                // transform.localPosition = new Vector3(0.75f, 0.72f, 0.0f);//new Vector3(0.75f, 0.72f, 0.0f);
                doAttack = false;
                if (this.transform.parent.gameObject.tag != "Enemy")
                    this.gameObject.renderer.enabled = false;
                if (this.gameObject.GetComponent<Collider2D>() != null)
                    this.gameObject.collider2D.enabled = false;
            }
        }
    }

    public override void PerformAttack()
    {
        if (!BeginAttack && !doAttack)
        {
            BeginAttack = true;
            finishedAnimation = false;
        }
    }
    public AudioClip weaponSound;
    public override void PlaySound()
    {
     
            if (SoundManager.instance.SoundOn)
                audio.Play();
    }
    void OnCollisionEnter2D(Collision2D hit)
    {
        if (this.transform.parent.gameObject.tag == "Player")
        {
            base.PlayerCollisions(hit);
        }
        else if (this.transform.parent.gameObject.tag == "Enemy")
        {
            base.EnemyCollisions(hit);
        }
    }

    /// <summary>
    /// Handle collisions involving the player if this weapon is owned by the player
    /// </summary>
    /// <param name="hit"></param>
    protected override void PlayerCollisions(Collision2D hit)
    {
        //Get the player
        PlayerMove player = this.transform.parent.gameObject.GetComponent<PlayerMove>();

        //Check if we are in an attacking state
        if (player.playerState == PlayerMove.PlayerState.Attacking)
        {
            //if we collided with an enemy
            if (hit.gameObject.tag == "Enemy")
            {
                //if we are using a sword
                if (!(player.currentWeapon is Bow))
                {
                    //Get the enemy script
                    Enemy enemyScript = hit.gameObject.GetComponent<Enemy>();

                    //Deal damage to the enemy
                    enemyScript.DoDamage(player.damage);
                    
                    enemyScript.CallCooldown();

                    //If we are allowed only one swing
                    if (multiHitCount == 1)
                    {
                        //end our movement
                        player.HaltMovement();
                        BeginningAngle = endAngle;
                        ani.Stop();
                    }
                    else
                    {
                        //Otherwise, increment the blows dealt
                        blowsDelivered++;

                        //If we have delivered the maximum number of blows
                        if (blowsDelivered >= multiHitCount)
                        {
                            //Halt the player and reset hit index
                            player.HaltMovement();
                            blowsDelivered = 0;
                            ani.Stop();
                        }
                    }
                }
            }
        }

    }
    protected override void EnemyCollisions(Collision2D hit)
    {
        //Get the player
        Enemy enemy = this.transform.parent.gameObject.GetComponent<Enemy>();

        //Check if we are in an attacking state
        if (enemy.EnemyState == Enemy.EnemyStates.Attacking)
        {
            //if we collided with an enemy
            if (hit.gameObject.tag == "Player")
            {
                //if we are not using a bow
                if (!(enemy.CurrentWeapon is Bow))
                {
                    //Get the enemy script
                    PlayerMove playerScript = hit.gameObject.GetComponent<PlayerMove>();

                    //Deal damage to the enemy
                    playerScript.DoDamage(enemy.damage);

                    //If we are allowed only one swing
                    if (multiHitCount == 1)
                    {
                        //end our movement
                        enemy.HaltMovement();
                        BeginningAngle = endAngle;
                        ani.Stop();
                    }
                    else
                    {
                        //Otherwise, increment the blows dealt
                        blowsDelivered++;

                        //If we have delivered the maximum number of blows
                        if (blowsDelivered >= multiHitCount)
                        {
                            //Halt the player and reset hit index
                            enemy.HaltMovement();
                            blowsDelivered = 0;
                            ani.Stop();
                        }
                    }
                }
            }
        }
    }
}
