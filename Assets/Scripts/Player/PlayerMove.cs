using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    public enum PlayerState { Stationary, Attacking };
    public PlayerState playerState;
    //public enum Weapons { Sword, Hammer, Bow, Spear, Axe };
    public Weapon currentWeapon;

    bool pressed = false;
    Vector2 velocity = Vector2.zero;
    Vector2 previousPosition = Vector2.zero;
    public float playerSpeed = 30.0f;
    public float playerSlowDown = 0.75f;

    public int damage = 1;

    public int health = 3;

    Vector2 forwardMovement;
    public float movementSpeed = 25;

    bool invincible = false;
    float invincibilityTimer = 0.0f;
    private const float TIME_INVINCIBLE = 1.5f;

    public float alpha = 1.0f;

    private bool dead = false;
    private float timeTillScreen = 0.0f;
    private const float TIME_TO_GAMEOVER = 0.50f;
	GameObject[] BodyParts;
    // Use this for initialization
    void Start()
    {
        playerSpeed = 30.0f;
        playerSlowDown = 0.75f;
        playerState = PlayerMove.PlayerState.Stationary;
        EquipWeapon("Axe");
		BodyParts = GameObject.FindGameObjectsWithTag("PlayerBodyPart");
    }

    // Update is called once per frame
    void Update()
    {
        ManageTouch();
        //DebugMovement();
        //CheckBounds();

        if (Input.GetKey("space"))
        {
            playerState = PlayerMove.PlayerState.Attacking;
            currentWeapon.PerformAttack();
        }
        if (invincible)
        {
            invincibilityTimer += Time.deltaTime;
            alpha = Mathf.Abs(Mathf.Cos(invincibilityTimer * 9));
            Color spriteColor = this.GetComponent<SpriteRenderer>().color;
			foreach(GameObject part in BodyParts)
			{
            	part.GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
			}
			this.GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            if (invincibilityTimer >= TIME_INVINCIBLE)
            {
                invincible = false;
                invincibilityTimer = 0;
				foreach(GameObject part in BodyParts)
				{
					part.GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 1.0f);
				}
               this.GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 1.0f);
            }
        }
        if (dead)
        {
            Color spriteColor = this.GetComponent<SpriteRenderer>().color;
            timeTillScreen += Time.deltaTime;
            //this.GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0.0f);
			this.gameObject.SetActive(false);
            if(timeTillScreen >= TIME_TO_GAMEOVER)
                Application.LoadLevel("GameOver");
        }
    }

    /// <summary>
    /// Handles touch input
    /// </summary>
    void ManageTouch()
    {

        //If we are stationary and not already sliding finger
        if (!pressed && playerState == PlayerState.Stationary)
        {
            //Make sure we are grabbing player, gather beginning movement parameters
            if (Input.touchCount > 0)
            {
                Vector2 touchPos = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
                //Collider2D overlap = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(touchPos));
                if (Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(touchPos)))
                {
                    previousPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 0));
                    pressed = true;
                }
            }
        }

        //if sliding finger
        if (pressed)
        {
            //Check input end
            if (Input.touchCount == 0 || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended))
            {
                //Perform movement
                Vector2 thisPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 0));

                if (thisPos == previousPosition)
                    return;
                
                Vector2 diff = new Vector3(thisPos.x - previousPosition.x, thisPos.y - previousPosition.y, 0);
                float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
                

                //Dont move if using bow
                if (!(currentWeapon is Bow))
                {
                    velocity = (diff / diff.magnitude) * (playerSpeed * currentWeapon.PlayerSpeedMod);
                }
                else if (currentWeapon is Bow)
                {
                    Bow bow = currentWeapon as Bow;
                    bow.Direction = diff / diff.magnitude;
                }
                //Rotate towards movement

                pressed = false;
                playerState = PlayerMove.PlayerState.Attacking;

                currentWeapon.PerformAttack();
            }
        }


        //set velocity
        rigidbody2D.velocity = velocity;
        //slow down velocity
        velocity *= playerSlowDown;

        //if velocity is below threshold, halt movement, set state
        if (velocity.magnitude <= 1.0f && currentWeapon.finishedAnimation)
        {
            playerState = PlayerState.Stationary;
            velocity = Vector2.zero;
        }
    }

    public void HaltMovement()
    {
        velocity = Vector2.zero;
        rigidbody2D.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D hit)
    {

    }

    /// <summary>
    /// Equips the specified weapon
    /// </summary>
    /// <param name="weaponToEquip"></param>
    public void EquipWeapon(string weaponToEquip)
    {
        currentWeapon.renderer.enabled = false;
        Collider2D currentWeapColl = currentWeapon.GetComponent<Collider2D>();
        if (currentWeapColl != null)
        {
            currentWeapColl.collider2D.enabled = false;
        }

        switch (weaponToEquip)
        {
            case "Sword":
                currentWeapon = gameObject.GetComponentInChildren<Sword>();
                break;

            case "Hammer":
                currentWeapon = gameObject.GetComponentInChildren<Hammer>();
                break;

            case "Bow":
                currentWeapon = gameObject.GetComponentInChildren<Bow>();
                currentWeapon.renderer.enabled = true;
                break;

            case "Spear":
                currentWeapon = gameObject.GetComponentInChildren<Spear>();
                break;

            case "Axe":
                currentWeapon = gameObject.GetComponentInChildren<Axe>();
                break;

            default:
                break;
        }
       
    }
    public AudioClip SoundFX;
    public  void PlaySound()
    {
        if (SoundManager.instance.SoundOn)
        {
            //Debug.Log("s");
            audio.Play();
        }
    }
    public void DoDamage(int damage)
    {
        if (!dead)
        {
            if (!invincible)
            {
                health -= damage;
                StatsAndGlobals.Instance.CheckHighestCombo(GUIManager.Instance.ComboStreak);
                StatsAndGlobals.Instance.IncrementTimesHit();
                GUIManager.Instance.UpdateHealthDisplay(health);
                GUIManager.Instance.ResetCombo();
                PlaySound();
            }

            invincible = true;

            if (health <= 0)
            {
                dead = true;
                invincible = false;
                GUIManager.Instance.CreateBloodSplatter(this.transform.position);                
            }
        }

    }


}