using UnityEngine;
using System.Collections;

public class OgreBoss : MeleeEnemy
{
    public GameObject hook;

    public float grappleMinRange;
    public float grappleMaxRange;
    public float grappleReloadTimer;
    private float currentTimer;
    public float grappleSpeed;
    private Vector2 grappleStartPos;
    public bool throwingGrapple;
    public bool pullingGrapple;
    public bool ableToMove;
    LineRenderer chain;
    public GrapplePlayer grapple;
    PlayerMove playerMove;
    public float throwForce = 50f;
    public bool canAttack;
    // Use this for initialization
    public override void Start()
    {
        currentTimer = grappleReloadTimer;
        grappleStartPos = hook.transform.localPosition;
        throwingGrapple = false;
        pullingGrapple = false;
        ableToMove = true;
        chain = hook.GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMove = player.GetComponent<PlayerMove>();
        //grapple = gameObject.GetComponentInChildren<GrapplePlayer>();
        base.Start();

    }

    // Update is called once per frame
    public override void Update()
    {
        float distanceToHook = Vector2.Distance(hook.transform.localPosition, grappleStartPos);
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceToPlayer <= grappleMaxRange && distanceToPlayer >= grappleMinRange && currentTimer >= grappleReloadTimer && grapple.canGrapple)
        {
            throwingGrapple = true;
            currentTimer = 0f;
        }
        if (distanceToHook <= 0.1f && pullingGrapple)
            hook.transform.localPosition = grappleStartPos;
        //Debug.Log(distanceToHook);
        if (distanceToHook >= grappleMaxRange)
        {

            pullingGrapple = true;
            throwingGrapple = false;
            currentTimer = 0f;
        }
        if (throwingGrapple)
        {

            hook.transform.Translate(Vector2.up * Time.deltaTime * grappleSpeed, Space.Self);
            if (currentTimer >= 2.0f || grapple.playerHooked)
            {
                throwingGrapple = false;
                pullingGrapple = true;
                currentTimer = 0f;
            }
        }
        if (pullingGrapple)
        {
            //hook.transform.Translate(Vector2.up * Time.deltaTime * grappleSpeed*-1);
            hook.transform.localPosition = Vector2.Lerp(hook.transform.localPosition, grappleStartPos, Time.deltaTime);
            if (currentTimer > 2.0f)
            {
                throwingGrapple = false;
                pullingGrapple = false;
                hook.transform.localPosition = grappleStartPos;

                ableToMove = true;
                currentTimer = 0f;
            }

        }

        if (distanceToPlayer <= attackRange)
        {
            Eat();
        }

        if (throwingGrapple || pullingGrapple)
        {
            ableToMove = false;
        }
        if (ableToMove)
        {
            base.Update();
        }
        //Debug.Log(ableToMove);
        //chain.SetPosition(0, grappleStartPos);
        //chain.SetPosition(1, hook.transform.localPosition);
        currentTimer += Time.deltaTime;


    }

    private void Eat()
    {

        ableToMove = false;
        playerMove.DoDamage(damage);
        Invoke("Throw", 0.5f);
        
    }
    void Throw()
    {
        Debug.Log("throw");
        grapple.CannotGrapple();
        Rigidbody2D _rigidbody = player.GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(transform.forward * throwForce);

    }
}
