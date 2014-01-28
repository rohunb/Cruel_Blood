using UnityEngine;
using System.Collections;

public class Hammer : Weapon {

	// Use this for initialization
    protected override void Start()
    {
        base.Start();
	}
	
	// Update is called once per frame
    protected  override void Update()
    {
        base.Update();
	}

    void OnCollisionEnter2D(Collision2D hit)
    {
        if (this.transform.parent.gameObject.tag == "Player")
        {
            base.PlayerCollisions(hit);
        }
    }
    public AudioClip weaponSound;
    public override void PlaySound()
    {
        if (SoundManager.instance.SoundOn)
            audio.Play();
    }
}
