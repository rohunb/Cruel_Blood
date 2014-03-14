using UnityEngine;
using System.Collections;

public class Ogre : MeleeEnemy
{

    public GameObject grapple;
    public GrapplePlayer hook;
    public float grappleMinRange;
    public float grappleMaxRange;
    public float grappleReloadTimer;
    public float grappleSpeed;
    public float grapplePullTimer = 2.0f;
    public float throwForce = 500f;

    public  float currentTimer = 0f;
    public  float timer2 = 0f;
    Vector2 grappleStartPos;
    LineRenderer chain;
    PlayerMove playerMove;
    bool playerHooked;
    float distanceToHook;
    public float distToPlayer;

    enum OgreState { CanGrapple, Throwing, Pulling, CanAttack, Hunting }
    OgreState state;

    public override void Start()
    {
        base.Start();
        grappleStartPos = grapple.transform.localPosition;
        chain = gameObject.GetComponent<LineRenderer>();
        playerMove = player.GetComponent<PlayerMove>();
        state = OgreState.Hunting;
        currentTimer = grappleReloadTimer;
    }

    public override void Update()
    {
        distanceToHook = Vector2.Distance(grapple.transform.localPosition, grappleStartPos);
        distToPlayer = Vector2.Distance(player.position, transform.position);
        
        CheckState();
        //Debug.Log("state: " + state);
        switch (state)
        {
            case OgreState.Throwing:
                ThrowGrapple();
                break;
            case OgreState.Pulling:
                PullGrappleBack();
                break;
            case OgreState.CanGrapple:
                //ThrowGrapple();
                break;
            case OgreState.CanAttack:
                break;
            case OgreState.Hunting:
                base.Update();
                break;
            default:
                base.Update();
                break;

        }
        if(distToPlayer<=attackRange)
        {
            Eat();
        }
        currentTimer += Time.deltaTime;
        timer2 += Time.deltaTime;
    }
    void Eat()
    {
        playerMove.DoDamage(damage);
        hook.playerHooked = false;
        hook.canGrapple = false;
        Invoke("ThrowPlayer", 0.75f);
        
    }
    void ThrowPlayer()
    {
        player.rigidbody2D.AddForce(transform.up * throwForce);
        Invoke("ResumeHunting", 0.75f);
        
    }
    void ResumeHunting()
    {
        hook.canGrapple = true;
        state = OgreState.Hunting;
        currentTimer = 0f;
    }
    void CheckState()
    {

        if (state != OgreState.Pulling && distToPlayer <= grappleMaxRange && distToPlayer >= grappleMinRange && currentTimer >= grappleReloadTimer)
        {
            state = OgreState.Throwing;
            timer2 = 0f;
            return;
        }
        if (state == OgreState.Throwing && (timer2 >= grapplePullTimer || hook.playerHooked || hook.collidedWithArena))
        {
            state = OgreState.Pulling;
            timer2 = 0f;
            return;
        }
        


    }
    void ThrowGrapple()
    {

        grapple.transform.Translate(Vector2.up * Time.deltaTime * grappleSpeed, Space.Self);
        currentTimer = 0f;
        
        //        state = OgreState.Throwing;
    }
    void PullGrappleBack()
    {
        if (hook.playerHooked)
        {

            player.position = hook.transform.position;

        }
        grapple.transform.localPosition = Vector2.Lerp(grapple.transform.localPosition, grappleStartPos, Time.deltaTime);
        if (timer2 > grapplePullTimer || Vector2.Distance(grapple.transform.localPosition, grappleStartPos) < .2f)
        {
            grapple.transform.localPosition = grappleStartPos;
            hook.collidedWithArena = false;
            timer2 = 0f;
            currentTimer = 0f;
            state = OgreState.Hunting;
        }

    }

}
