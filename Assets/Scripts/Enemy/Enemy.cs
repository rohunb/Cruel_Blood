using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public bool markedForDeath;
    public Transform player;
    public int damage;
    public int health;
    public float speed;
    public float attackSpeed = 2.0f;

    public Weapon CurrentWeapon;
    public enum EnemyStates { Hunting, Attacking };
    public EnemyStates EnemyState = EnemyStates.Hunting;

    public int BloodValue = 50;

    private float timeSinceLastAttack = 0.0f;

    public bool CanAttack = false;

    public bool invincible = false;
    float invincibilityTimer = 0.0f;
    private const float TIME_INVINCIBLE = 1.5f;

    public float alpha = 1.0f;


	// Use this for initialization
	public virtual void Start () {
        Debug.Log("enemy start");
        markedForDeath = false;
        timeSinceLastAttack = attackSpeed;
        player = GameObject.FindGameObjectWithTag("Player").transform;

	}
	
	// Update is called once per frame
	public virtual void Update () {
        LookAtPlayer();

        if(!CanAttack)
            timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack > attackSpeed)
            CanAttack = true;
        else
            CanAttack = false;

        if (invincible)
        {
            invincibilityTimer += Time.deltaTime;
            alpha = Mathf.Abs(Mathf.Cos(invincibilityTimer * 9));
            Color spriteColor = this.GetComponent<SpriteRenderer>().color;

            this.GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);

            if (invincibilityTimer >= TIME_INVINCIBLE)
            {
                invincible = false;
                invincibilityTimer = 0;

                this.GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 1.0f);
            }
        }
        
	}
    public void LookAtPlayer()
    {
        float angle = Mathf.Atan2(player.position.y - transform.position.y,
            player.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));

    }

    public void CallCooldown()
    {
        CanAttack = false;
        timeSinceLastAttack = 0;
    }

    public void HaltMovement()
    {
        EnemyState = EnemyStates.Hunting;
    }

    /// <summary>
    /// Deals damage to this enemy
    /// </summary>
    /// <param name="damage">Amount of damage to apply</param>
    public void DoDamage(int damage)
    {
        if (!invincible)
        {
            //Deal damage
            health -= damage;
            
            //If less then 0 health, destroy object
            if (health <= 0)
            {
                markedForDeath = true;
                GUIManager.Instance.CreateBloodSplatter(this.transform.position);
                PlaySound();
                Destroy(this.gameObject);
            }

        }

        invincible = true;
    }

    protected virtual void OnDestroy()
    {
        StatsAndGlobals.Instance.AddLitresToPurse(this.BloodValue);
        StatsAndGlobals.Instance.IncrementTotalKills();
    }
    public AudioClip weaponSound;
    public void PlaySound()
    {
        //if (SoundManager.instance.SoundOn)
        //    audio.Play();
    }
}
