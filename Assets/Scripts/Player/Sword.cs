using UnityEngine;
using System.Collections;

public class Sword : Weapon {

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}
    public AudioClip weaponSound;
    public override void PlaySound() {
        if (SoundManager.instance.SoundOn)
            audio.Play();
    }
}
