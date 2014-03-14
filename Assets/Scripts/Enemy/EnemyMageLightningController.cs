using UnityEngine;
using System.Collections;
//[RequireComponent(typeof(LineRenderer))]

public class EnemyMageLightningController : RangedEnemy
{
    public float castTime = 3f;

    public GameObject lightning;
    //public float range = 2f;
    //public float speed = 0.5f;
    private float currentTimer;
    private bool startedCasting;
    //public float damage = 10f;
    //public bool markedForDeath;
    // Use this for initialization
    PlayerMove playerMove;
    public override void Start()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        currentTimer = 0f;
        startedCasting = false;
        base.damage = 1;
        //markedForDeath = false;

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {


        if (LerpUntilInRange())
        {
            if (!startedCasting)
            {
                startedCasting = true;
                currentTimer = 0f;
            }
        }
        else
        {
            startedCasting = false;

        }
        if (currentTimer >= castTime && startedCasting)
        {
            //in range and firing
            CastLightning();

        }
        currentTimer += Time.deltaTime;
        base.Update();
    }
    private void CastLightning()
    {
        GameObject lightningClone = Instantiate(lightning, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 180f))) as GameObject;
        LineRenderer lightningLine = lightningClone.GetComponent<LineRenderer>();
        lightningLine.SetPosition(1, new Vector3(player.position.x, player.position.y, 0f));
        lightningLine.SetPosition(0,
            new Vector3(transform.position.x, transform.position.y, 0f));

        playerMove.DoDamage(damage);
        //PlayerHealth.health -= damage;
        currentTimer = 0f;

    }

}
