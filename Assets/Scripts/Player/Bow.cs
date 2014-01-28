using UnityEngine;
using System.Collections;

public class Bow : Weapon {

    public GameObject arrow;
    public Vector2 Direction;

    public RangerHand animation;
    private float timeToStopAni = 0.0f;

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        if (BeginAttack)
        {
            this.gameObject.renderer.enabled = true;
           animation = transform.parent.GetComponentInChildren<RangerHand>();
            if (animation != null)
                animation.PlayAnimation();
            BeginAttack = false;
            doAttack = true;
        }

        if (doAttack)
        {
            if (arrow != null)
            {
                GameObject newArrow = (GameObject)GameObject.Instantiate(arrow, transform.position, Quaternion.identity);
                newArrow.rigidbody2D.velocity = Direction * (AttackSpeed * 10);
                newArrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90)));

                Arrow arrowscript = newArrow.GetComponent<Arrow>();
                arrowscript.Damage = this.Damage;
                arrowscript.Owner = this.gameObject.transform.parent.tag;
                DestroyObject(newArrow, 10.0f);
                doAttack = false;
            }
        }

        timeToStopAni += Time.deltaTime;
        if (timeToStopAni >= 0.35f)
        {
            if (animation != null)
                animation.StopAnimation();
            finishedAnimation = true;
            timeToStopAni = 0.0f;
        }
	}

    public override void PerformAttack()
    {
        
        if (!BeginAttack && !doAttack)
        {
            PlaySound();
            BeginAttack = true;
            finishedAnimation = false;
        }
    }
    
}
