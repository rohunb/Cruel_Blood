using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

    public int levelCount;
    public int waveCount;
    public int[] numEnemiesToStartLevelWith;
    public int[] numWavesForLevel;
    
    public float[] enemySpawnTimerPerLevel;
    public float waveWaitTime;
    public float currentTime;
    SpawnEnemy enemySpawner;
    int numEmemies;
    private bool inCombatLevel;
    private int moreLevelsEnemiesToStart;
    private int moreLevelsNumWaves;
    public int numBosses;
    public bool IncreaseBossWaves;

    public bool waveComplete;
    public string LevelIDToLoadAfterLevelComplete = "StatsScene";
    private int bossWaves;
    public static bool readyToEndLevel;
    public static bool AllWavesSpawned;

    private float timeToScreenSwitch = 0.0f;
    private const float TIME_TIL_TRANSITION = 2.0f;

    private static bool created = false;
	// Use this for initialization
    void Awake()
    {
        if (!created)
        {
            // this is the first instance - make it persist
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            // this must be a duplicate from a scene reload - DESTROY!
            Destroy(this.gameObject);
        }
        //DontDestroyOnLoad(gameObject);

    }
	void Start () {
        levelCount = 0;
        waveCount = 0;
        inCombatLevel = false ;
        moreLevelsEnemiesToStart = numEnemiesToStartLevelWith[4];
        moreLevelsNumWaves = numWavesForLevel[4];
        bossWaves = 1;
        readyToEndLevel = false;
        AllWavesSpawned = false;
        Init();
	}
    void OnLevelWasLoaded(int level)
    {
        readyToEndLevel = false;
        AllWavesSpawned = false;
        //Debug.Log("level: " + levelCount);
        if (level == 1)
        {
            inCombatLevel = true;
            levelCount++;
            waveCount = 1;
            enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<SpawnEnemy>();
            Init();
        }
        else
            inCombatLevel = false;


    }
    public void Init()
    {
        currentTime = 0f;
        waveComplete = false;
        InitPlayer();
    }

    private void InitPlayer()
    {
        GameObject obj = GameObject.FindWithTag("Player");// GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();

        if (obj != null)
        {
            PlayerMove player = obj.GetComponent<PlayerMove>();

            Axe axe = player.GetComponentInChildren<Axe>();
            Hammer hammer = player.GetComponentInChildren<Hammer>();
            Bow bow = player.GetComponentInChildren<Bow>();
            Spear spear = player.GetComponentInChildren<Spear>();
            Sword sword = player.GetComponentInChildren<Sword>();

            axe.BeginningAngle = axe.BeginningAngle + (StatsAndGlobals.Instance.axeLevel * StatsAndGlobals.AXE_ROT_INCREASE);
            hammer.AttackSpeed = hammer.AttackSpeed + (StatsAndGlobals.Instance.hammerLevel * StatsAndGlobals.HAMMER_SWING_SPEED);
            bow.Damage = bow.Damage + (StatsAndGlobals.Instance.bowLevel * StatsAndGlobals.BOW_DAMAGE);
            spear.multiHitCount = spear.multiHitCount + (StatsAndGlobals.Instance.spearLevel * StatsAndGlobals.SPEAR_MULTI_HIT);
            switch (StatsAndGlobals.Instance.swordLevel)
            {
                case 0:
                    break;

                case 1:
                    sword.PlayerSpeedMod = sword.PlayerSpeedMod + (StatsAndGlobals.Instance.swordLevel * StatsAndGlobals.SWORD_DASH_DIST);
                    break;
                case 2:
                    sword.PlayerSpeedMod = sword.PlayerSpeedMod + (StatsAndGlobals.Instance.swordLevel * StatsAndGlobals.SWORD_DASH_DIST);
                    break;
                case 3:
                    sword.PlayerSpeedMod = sword.PlayerSpeedMod + (2 * StatsAndGlobals.SWORD_DASH_DIST);
                    sword.AttackSpeed = sword.AttackSpeed + (1 * StatsAndGlobals.SWORD_SWING_SPEED);
                    break;
                case 4:
                     sword.PlayerSpeedMod = sword.PlayerSpeedMod + (2 * StatsAndGlobals.SWORD_DASH_DIST);
                    sword.AttackSpeed = sword.AttackSpeed + (2 * StatsAndGlobals.SWORD_SWING_SPEED);
                    break;
                case 5:
                    sword.PlayerSpeedMod = sword.PlayerSpeedMod + (2 * StatsAndGlobals.SWORD_DASH_DIST);
                    sword.AttackSpeed = sword.AttackSpeed + (2 * StatsAndGlobals.SWORD_SWING_SPEED);
                    sword.BeginningAngle = sword.BeginningAngle + (1 * StatsAndGlobals.SWORD_ANGLE);
                    break;
                case 6:
                      sword.PlayerSpeedMod = sword.PlayerSpeedMod + (2 * StatsAndGlobals.SWORD_DASH_DIST);
                    sword.AttackSpeed = sword.AttackSpeed + (2 * StatsAndGlobals.SWORD_SWING_SPEED);
                    sword.BeginningAngle = sword.BeginningAngle + (2 * StatsAndGlobals.SWORD_ANGLE);
                    break;
                default:
                    break;
            }

            player.health = StatsAndGlobals.Instance.armourLevel + 1;

        }
    }

	// Update is called once per frame
	void Update () {
        if (readyToEndLevel)
        {
            if (timeToScreenSwitch >= TIME_TIL_TRANSITION)
            {
                timeToScreenSwitch = 0.0f;
                Application.LoadLevel(LevelIDToLoadAfterLevelComplete);
            }
            else
            {
                timeToScreenSwitch += Time.deltaTime;
            }
        }
        if(waveComplete)
        {
            currentTime = 0f;
            waveComplete = false;
        }
        if (inCombatLevel && !AllWavesSpawned)
        {
            if (levelCount == 1 && currentTime>=waveWaitTime)
            {
                Level1Waves();
            }
            else if (levelCount == 2 && currentTime >= waveWaitTime)
            {
                Level2Waves();
            }
            else if (levelCount == 3 && currentTime >= waveWaitTime)
            {
                Level3Waves();
            }
            else if (levelCount == 4 && currentTime >= waveWaitTime)
            {
                Level4Waves();
            }
            else if (levelCount > 4 && currentTime >= waveWaitTime)
            {

                if(levelCount%10==0)
                {
                    SpawnOgreBoss();
                }
                else if(levelCount%5==0)
                {
                    Debug.Log("if");
                    SpawnRangerBoss();
                }
                else
                    MoreLevelsWaves();
            }
            
            
        }
        currentTime += Time.deltaTime;
        
        if (AllWavesSpawned)
        {

            GameObject[] games = GameObject.FindGameObjectsWithTag("Enemy");

            //int numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (games.Length <= 0)
            {
                readyToEndLevel = true;
            }
        }
	}

    private void SpawnRangerBoss()
    {
        Debug.Log("ranger");
        enemySpawner.chanceMelee = 0f;
        enemySpawner.chanceArcher = 0f;
        enemySpawner.chanceMageLightning = 0f;
        enemySpawner.chanceShield = 0f;
        enemySpawner.chanceOgre = 0f;
        enemySpawner.chanceRanger = 100f;
        if (waveCount <= bossWaves)
        {
            enemySpawner.SpawnEnemies(numBosses );
        }
        
        else
        {


            AllWavesSpawned = true;
        }


    }
    private void SpawnOgreBoss()
    {
        enemySpawner.chanceMelee = 0f;
        enemySpawner.chanceArcher = 0f;
        enemySpawner.chanceMageLightning = 0f;
        enemySpawner.chanceShield = 0f;
        enemySpawner.chanceOgre = 100f;
        enemySpawner.chanceRanger = 0f;
        if(waveCount<=bossWaves)
        {
            enemySpawner.SpawnEnemies(numBosses);
        }
        
        else
        {
             if(IncreaseBossWaves)
        {
            bossWaves++;
        }
             //numBosses++;
             AllWavesSpawned = true;
        }
        
        
    }
    private void MoreLevelsWaves()
    {
        enemySpawner.chanceMelee = 25f;
        enemySpawner.chanceArcher = 25f;
        enemySpawner.chanceMageLightning = 25f;
        enemySpawner.chanceShield = 25f;
        enemySpawner.chanceOgre = 0f;
        enemySpawner.chanceRanger = 0f;
        if(waveCount<=moreLevelsNumWaves)
        {
            enemySpawner.SpawnEnemies(moreLevelsEnemiesToStart);

        }
        else
        {
            if (levelCount % 3 == 0)
            {
                moreLevelsEnemiesToStart++;

                
            }
            if(levelCount%5==0)
            { moreLevelsNumWaves++; 
            }
            AllWavesSpawned = true;
        }
    }
    private void Level4Waves()
    {
        enemySpawner.chanceMelee = 15f;
        enemySpawner.chanceArcher = 15f;
        enemySpawner.chanceMageLightning = 55f;
        enemySpawner.chanceShield = 15f;
        enemySpawner.chanceOgre = 0f;
        enemySpawner.chanceRanger = 0f;
        enemySpawner.enemySpawnTimer = enemySpawnTimerPerLevel[3];
        if (waveCount <= numWavesForLevel[3])
        {
            //Debug.Log("wavemanager enemies to spawn " + (numEnemiesToStartLevelWith[3] + waveCount + 1));
            enemySpawner.SpawnEnemies(numEnemiesToStartLevelWith[3] + waveCount + 1);

        }
        else
        {
            AllWavesSpawned = true;
        }
    }

    private void Level3Waves()
    {
        enemySpawner.chanceMelee = 25f;
        enemySpawner.chanceArcher = 25f;
        enemySpawner.chanceMageLightning = 0f;
        enemySpawner.chanceShield = 50f;
        enemySpawner.chanceOgre = 0f;
        enemySpawner.chanceRanger = 0f;
        enemySpawner.enemySpawnTimer = enemySpawnTimerPerLevel[2];
        if (waveCount <= numWavesForLevel[2])
        {
            //Debug.Log("wavemanager enemies to spawn " + (numEnemiesToStartLevelWith[2] + waveCount + 1));
            enemySpawner.SpawnEnemies(numEnemiesToStartLevelWith[2] + waveCount + 1);

        }
        else
        {
            AllWavesSpawned = true;
        }
    }

    private void Level2Waves()
    {
        enemySpawner.chanceMelee = 70f;
        enemySpawner.chanceArcher = 33f;
        enemySpawner.chanceMageLightning = 0f;
        enemySpawner.chanceShield = 0f;
        enemySpawner.chanceOgre = 0f;
        enemySpawner.chanceRanger = 0f;
        enemySpawner.enemySpawnTimer = enemySpawnTimerPerLevel[1];
        if (waveCount <= numWavesForLevel[1])
        {
            //Debug.Log("wavemanager enemies to spawn " + (numEnemiesToStartLevelWith[1] + waveCount + 1));
            enemySpawner.SpawnEnemies(numEnemiesToStartLevelWith[1] + waveCount + 1);

        }
        else
        {
            AllWavesSpawned = true;
        }
    }

    private void Level1Waves()
    {
        
        enemySpawner.chanceMelee = 100f;
        enemySpawner.chanceArcher = 0f;
        enemySpawner.chanceMageLightning = 0f;
        enemySpawner.chanceShield = 0f;
        enemySpawner.chanceOgre = 0f;
        enemySpawner.chanceRanger = 0f;
        enemySpawner.enemySpawnTimer = enemySpawnTimerPerLevel[0];
        if(waveCount<=numWavesForLevel[0])
        { //Debug.Log("wavemanager enemies to spawn " + (numEnemiesToStartLevelWith[0] + waveCount + 1));
        enemySpawner.SpawnEnemies(numEnemiesToStartLevelWith[0] + waveCount + 1);
            
        }
        else
        {
            AllWavesSpawned = true;
        }
        
    }
}
