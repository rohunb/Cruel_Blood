using UnityEngine;
using System.Collections;

public class StatsAndGlobals : MonoBehaviour
{
    public static StatsAndGlobals Instance;

    #region Statistics
    private int totalLitresOfBlood = 0;

    public int TotalLitresOfBlood
    {
        get { return totalLitresOfBlood; }
        private set { totalLitresOfBlood = value; }
    }

    private int totalHighestCombo = 0;

    public int TotalHighestCombo
    {
        get { return totalHighestCombo; }
        private set { totalHighestCombo  = value; }
    }

    private int totalNumberOfBossesKilled = 0;

    public int TotalNumberOfBossesKilled
    {
        get { return totalNumberOfBossesKilled ; }
        private set { totalNumberOfBossesKilled = value; }
    }

    private int totalHighestWaveReached = 0;

    public int TotalHighestWaveReached
    {
        get { return totalHighestWaveReached ; }
        private set { totalHighestWaveReached = value; }
    }

    private int totalTimesHit = 0;

    public int TotalTimesHit
    {
        get { return totalTimesHit; }
        private set { totalTimesHit = value; }
    }

    private int totalNumKills = 0;

    public int TotalNumKills
    {
        get { return totalNumKills ; }
        private set { totalNumKills = value; }
    }

    #endregion

    #region Globals

    private bool muteSounds;

    public bool MuteSounds
    {
        get { return muteSounds; }
        private set { muteSounds = value; }
    }


    private int currentLitresOfBlood = 0;

    public int CurrentLitresOfBlood
    {
        get { return currentLitresOfBlood ; }
        private set { CurrentLitresOfBlood = value; }
    }
   #endregion

    #region UpgradeMods
    public const int AXE_ROT_INCREASE = 30;
    public const int SPEAR_MULTI_HIT = 1;
    public const float HAMMER_SWING_SPEED = 0.2f;
    public const int BOW_DAMAGE = 1;
    public const float SWORD_DASH_DIST = 0.5f;
    public const float SWORD_SWING_SPEED = 0.2f;
    public const int SWORD_ANGLE = 15;

    public int axeLevel = 0;
    public int spearLevel = 0;
    public int hammerLevel = 0;
    public int bowLevel = 0;
    public int swordLevel = 0;
    public int armourLevel = 3;
    public int speedLevel = 0;

    
    #endregion

    // Use this for initialization
	void Awake () {
        
     axeLevel = 0;
     spearLevel = 0;
     hammerLevel = 0;
     bowLevel = 0;
     swordLevel = 0;
     armourLevel = 3;
     speedLevel = 0;
     currentLitresOfBlood = 0;
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ResetStats()
    {
        totalHighestCombo = 0;
        totalHighestWaveReached = 0;
        totalLitresOfBlood = 0;
        totalNumberOfBossesKilled = 0;
        totalNumKills = 0;
        totalTimesHit = 0;
    }

    public void IncrementTimesHit()
    {
        totalTimesHit++;
    }

    /// <summary>
    /// Increments total blood and current purse by value provided
    /// </summary>
    /// <param name="litresToAdd">Number of litres to add to your purse</param>
    public void AddLitresToPurse(int litresToAdd)
    {
        currentLitresOfBlood += litresToAdd;
        totalLitresOfBlood += litresToAdd;
    }

    public void CheckHighestCombo(int comboToCheck)
    {
        if (comboToCheck > totalHighestCombo)
            totalHighestCombo = comboToCheck;
    }

    public void IncrementTotalKills()
    {
        totalNumKills++;
    }
    /// <summary>
    /// True to turn mute on, false to turn mute off
    /// </summary>
    /// <param name="onOrOff"></param>
    public void SetMute(bool onOrOff)
    {
        muteSounds = onOrOff;
    }
    public void ReduceBlood(int blood)
    {

        currentLitresOfBlood -= blood;

    }
}
