using UnityEngine;
using System.Collections;

public class StatsScreenManager : MonoBehaviour {

    public GUIText[] statDisplays = new GUIText[6];

	// Use this for initialization
	void Start () {
      
            statDisplays[0].text = "Max Combo : " +  StatsAndGlobals.Instance.TotalHighestCombo;
            statDisplays[1].text = "Boss Kills : " + StatsAndGlobals.Instance.TotalNumberOfBossesKilled;
            statDisplays[2].text = "Number Of Kills : " + StatsAndGlobals.Instance.TotalNumKills;
            statDisplays[3].text = "Blood Spilled : " + StatsAndGlobals.Instance.TotalLitresOfBlood;
            statDisplays[4].text = "Levels Completed: " + StatsAndGlobals.Instance.TotalHighestWaveReached;
            statDisplays[5].text = "Times Hit : " + StatsAndGlobals.Instance.TotalTimesHit;
        
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0))
            Application.LoadLevel("UpgradeScene");
	}
}
