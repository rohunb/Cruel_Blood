using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    public int Damage = 1;
    public float AttackSpeed = 5.0f;
    public float BeginningAngle = 45;
    protected bool BeginAttack = false;
    public float PlayerSpeedMod = 1.0f;

    protected bool doAttack = false;
    protected int attackDir;

    protected const int LEFT = 0;
    protected const int RIGHT = 1;
    protected const float BASE_ATK_SPEED = 2.0f;

    protected float endAngle;
    protected float amountMoved = 0.0f;

    public int multiHitCount = 1;
    protected int blowsDelivered = 0;

    public bool finishedAnimation = true;

    public GUIManager guiManager;

    //public GUIText collisionText;
    // Use this for initialization
    protected virtual void Start()
    {
        guiManager = GameObject.Find("GUIManager").GetComponent<GUIManager>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
           
        if (BeginAttack)
        {
            PlaySound();
            amountMoved = 0.0f;
            GetAttackDirection();

            if (attackDir == LEFT)
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.parent.rotation.eulerAngles.z - BeginningAngle));
                endAngle = BeginningAngle * 2;
            }
            else if (attackDir == RIGHT)
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.parent.rotation.eulerAngles.z + BeginningAngle));
                endAngle = BeginningAngle * 2;
            }
            
            this.gameObject.renderer.enabled = true;
            this.gameObject.collider2D.enabled = true;
            BeginAttack = false;
            doAttack = true;
        }

        if (doAttack)
        {
            if (attackDir == LEFT)
            {
                this.transform.Rotate(new Vector3(0, 0, 1), BASE_ATK_SPEED * AttackSpeed);
                amountMoved += BASE_ATK_SPEED * AttackSpeed;
            }
            else if (attackDir == RIGHT)
            {
                this.transform.Rotate(new Vector3(0, 0, 1), -BASE_ATK_SPEED * AttackSpeed);
                amountMoved += BASE_ATK_SPEED * AttackSpeed;
            }

            if (amountMoved > endAngle)
            {
                doAttack = false;                
                this.gameObject.renderer.enabled = false;
                this.gameObject.collider2D.enabled = false;
                finishedAnimation = true;
                endAngle = 0;
            }
        }

    }

    private void GetAttackDirection()
    {
        attackDir = Random.Range(0, 2);
    }

    public virtual void PerformAttack()
    {
        if (!BeginAttack && !doAttack)
        {
            BeginAttack = true;
            finishedAnimation = false;
        }
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        
        if (this.transform.parent.gameObject.tag == "Player")
        {            
            PlayerCollisions(hit);
        }
        else if (this.transform.parent.gameObject.tag == "Enemy")
        {
            EnemyCollisions(hit);
        }
    }

    /// <summary>
    /// Handle collisions involving the player if this weapon is owned by the player
    /// </summary>
    /// <param name="hit"></param>
    protected virtual void PlayerCollisions(Collision2D hit)
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

                    if (!enemyScript.invincible)
                    {
                        //Deal damage to the enemy
                        enemyScript.DoDamage(player.damage);

                        GUIManager.Instance.IncrementCombo(1);
                        StatsAndGlobals.Instance.CheckHighestCombo(GUIManager.Instance.ComboStreak);

                        //If we are allowed only one swing
                        if (multiHitCount == 1)
                        {
                            //end our movement
                            player.HaltMovement();
                            // BeginningAngle = endAngle;
                            player.currentWeapon.collider2D.enabled = false;
                            endAngle = 0;


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
                                endAngle = 0;
                            }
                        }
                    }
                }
            }
        }

    }
    public virtual void PlaySound() { }
    /// <summary>
    /// Handles Enemy collisions to deal damage to player
    /// </summary>
    /// <param name="hit"></param>
    protected virtual void EnemyCollisions(Collision2D hit)
    {
        //Get the player
        Enemy enemy = this.transform.parent.gameObject.GetComponent<Enemy>();

        //Check if we are in an attacking state
        if (enemy.EnemyState  == Enemy.EnemyStates.Attacking)
        {
         
            //if we collided with the player
            if (hit.gameObject.tag == "Player")
            {  
                //if we are not using a bow
                if (!(enemy.CurrentWeapon is Bow))
                {
                    //Get the enemy script
                    PlayerMove playerScript = hit.gameObject.GetComponent<PlayerMove>();

                    //Deal damage to the enemy
                    playerScript.DoDamage(enemy.damage);

                    //Begin cooldown
                    enemy.CallCooldown();

                    //If we are allowed only one swing
                    if (multiHitCount == 1)
                    {
                        //end our movement
                        enemy.HaltMovement();
                       // BeginningAngle = endAngle;
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
                            BeginningAngle = endAngle;
                        }
                    }
                }
            }
        }
    }
}
