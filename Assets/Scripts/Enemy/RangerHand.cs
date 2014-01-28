using UnityEngine;
using System.Collections;

public class RangerHand : MonoBehaviour {

	// Use this for initialization

	float timeElapsed;
	float animationTime;
	bool animationPlaying;
	Vector3 startingLocalPosition;
	void Start () {
        GetAnimationTime();
		startingLocalPosition= this.transform.localPosition;
	}

    void GetAnimationTime()
    {
        if (transform.parent.gameObject.tag == "Enemy")
        {
            animationTime = transform.parent.GetComponent<EnemyArcher>().reloadTime;
        }
        else if (transform.parent.gameObject.tag == "Player")
        {
            Bow bow = transform.parent.GetChild(0).GetComponent<Bow>();
            if (bow != null)
                animationTime = 2;
        }
    }

	// Update is called once per frame
    void Update()
    {
        
        timeElapsed += Time.deltaTime;

        if (animationPlaying)
        {
            transform.localPosition = new Vector3(0, -Mathf.Sin(timeElapsed / (animationTime * 2)) + 0.5f, startingLocalPosition.z);

        }
        else
        {
            transform.localPosition = startingLocalPosition;
            timeElapsed = 0;
        }
    }

	public void PlayAnimation()
	{
        if (animationTime == 0.0f)
            GetAnimationTime();

		if(!animationPlaying)
		{
			timeElapsed = 0;
		}
		animationPlaying = true;
	}
	public void StopAnimation()
	{
		animationPlaying = false;
	}
}
