using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

    enum Enemies
    {
        Melee = 0, Archer = 1, MageLightning = 2,Shield = 3,Ranger=4,Ogre=5
    }
    public Transform[] enemySpawnPoints;
    
    public float enemySpawnTimer;
    public GameObject[] enemies;
    public float chanceMelee;
    public float chanceArcher;
    public float chanceMageLightning;
    public float chanceShield;
    public float chanceRanger;
    public float chanceOgre;
    
    private float currentTimer;
    private GameObject enemyToSpawn;
    public int numberOfEnemiesToSpawn;
    public int enemiesSpawned;
    WaveManager waveManagerInstance;
    bool startSpawningEnemies;
	// Use this for initialization
	void Start () {
        currentTimer = enemySpawnTimer;
        enemiesSpawned = 0;
        waveManagerInstance = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        startSpawningEnemies = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("enemies spawned: " + enemiesSpawned);
        //Debug.Log("enemies to spawn" + numberOfEnemiesToSpawn);
        if (enemiesSpawned >= numberOfEnemiesToSpawn && startSpawningEnemies)
        {
            waveManagerInstance.waveCount++;
            waveManagerInstance.waveComplete = true;
            startSpawningEnemies = false;

        }
        else if (currentTimer >= enemySpawnTimer && startSpawningEnemies)
        {
            CreateEnemy();
            currentTimer = 0;
            enemiesSpawned++;
        }
        currentTimer += Time.deltaTime; 
	}
    public void SpawnEnemies(int numEnemies)
    {
        if (!startSpawningEnemies)
        {
            enemiesSpawned = 0;
            numberOfEnemiesToSpawn = numEnemies;
            startSpawningEnemies = true;
        }
    }
    private void CreateEnemy()
    {
        //randomly choose an enemy spawn point
        
        int spawnPointIndex=Random.Range(0,enemySpawnPoints.Length);
        Transform spawnPointToUse=enemySpawnPoints[spawnPointIndex];
        
        float spawnChance = Random.value*100f;
        //Debug.Log("Spawn Chance: " + spawnChance);
        float enemySpawnerIndex=chanceMelee;
        if (spawnChance <= chanceMelee)
        {
            enemyToSpawn=enemies[(int)Enemies.Melee];
            enemySpawnerIndex=chanceMelee;
        }
        //else if (spawnChance > enemySpawnerIndex 
        //    && spawnChance <= enemySpawnerIndex + chanceArcher)
        else if(spawnChance>chanceMelee && spawnChance<=chanceMelee+chanceArcher)
        {
            enemyToSpawn = enemies[(int)Enemies.Archer];
            enemySpawnerIndex += chanceArcher;
        }
        //else if(spawnChance>enemySpawnerIndex
        //    && spawnChance <= enemySpawnerIndex + chanceMageLightning)
        else if (spawnChance > chanceMelee + chanceArcher
           && spawnChance <= chanceMelee + chanceArcher + chanceMageLightning)
        {
            enemyToSpawn = enemies[(int)Enemies.MageLightning];
            enemySpawnerIndex += chanceMageLightning;
        }
        //else if (spawnChance > enemySpawnerIndex
        //    && spawnChance <= enemySpawnerIndex + chanceShield)
        else if (spawnChance > chanceMelee + chanceArcher + chanceMageLightning
            && spawnChance <= chanceMelee + chanceArcher + chanceMageLightning + chanceShield)
        {
            enemyToSpawn = enemies[(int)Enemies.Shield];
            enemySpawnerIndex += chanceShield;
        }
        else if (spawnChance > chanceMelee + chanceArcher + chanceMageLightning + chanceShield
            && spawnChance <= chanceMelee + chanceArcher + chanceMageLightning + chanceShield+chanceRanger)
        {
            enemyToSpawn = enemies[(int)Enemies.Ranger];

        }
        else if(spawnChance > chanceMelee + chanceArcher + chanceMageLightning + chanceShield+chanceRanger
            && spawnChance <= chanceMelee + chanceArcher + chanceMageLightning + chanceShield+chanceRanger+chanceOgre)
        {
            enemyToSpawn = enemies[(int)Enemies.Ogre];
        }
        
            
        Instantiate(enemyToSpawn, spawnPointToUse.position, Quaternion.identity);
        
    }

}
