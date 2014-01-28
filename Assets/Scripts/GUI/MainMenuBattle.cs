using UnityEngine;
using System.Collections;

public class MainMenuBattle : MonoBehaviour {
    private bool goingRight = true;

    private float timeToToss = 1.2f;

    public Sprite[] weapons;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (goingRight)
            transform.Translate(1, 0, 0);
        else
            transform.Translate(-1, 0, 0);

        if (transform.position.x > 20)
            goingRight = false;
        else if (transform.position.x < -20)
            goingRight = true;

        timeToToss -= Time.deltaTime;

        if (timeToToss <= 0)
        {
            GenerateTossUp();
            timeToToss = 0.3f;// Random.Range(0.3f, 3.0f);
        }
	}

    void GenerateTossUp()
    {
        if (GameObject.FindGameObjectsWithTag("FlyingShit").Length < 15)
        {
            int rand = Random.Range(0, weapons.Length);

            GameObject generatedItem = new GameObject();// (GameObject)GameObject.Instantiate(new GameObject());
            generatedItem.tag = "FlyingShit";
            generatedItem.AddComponent<SpriteRenderer>();
            generatedItem.GetComponent<SpriteRenderer>().sprite = weapons[rand];
            generatedItem.AddComponent<Rigidbody2D>();
            generatedItem.transform.position = this.transform.position;
            generatedItem.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
            generatedItem.transform.Rotate(new Vector3(0, 0, Random.Range(0, 30)));
            generatedItem.rigidbody2D.velocity = new Vector2(Random.Range(-15, 15), Random.Range(5, 25));
          
            Destroy(generatedItem, 0.3f);
        }
    }
}
