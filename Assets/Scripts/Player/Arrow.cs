using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    public string Owner;
    public int Damage = 1;

	// Use this for initialization
	void Start () {
        PlaySound();
	}
	
	// Update is called once per frame
	void Update () {
        if (rigidbody2D.velocity.magnitude < 1.0f)
        {
            Destroy(this.gameObject);
        }
	}
    public AudioClip weaponSound;
    public void PlaySound()
    {
        if (SoundManager.instance.SoundOn)
            audio.Play();
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag != Owner)
        {
            if (hit.gameObject.tag == "Enemy")
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                enemy.DoDamage(Damage);
                Destroy(this.gameObject);    
            }
            else if (hit.gameObject.tag == "Player")
            {
                PlayerMove player = hit.gameObject.GetComponent<PlayerMove>();
                player.DoDamage(Damage);
                Destroy(this.gameObject);    
            }
            //else if(hit.gameObject.tag != "Weapon")
            //    Destroy(this.gameObject);     
        }
    }
}
