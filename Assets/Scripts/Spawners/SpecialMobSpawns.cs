using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMobSpawns : MonoBehaviour
{
    private const int MAX_SPAWN_CHANCE = 100;

    [SerializeField] private GameObject mobPrefab;
    [SerializeField] private List<GameObject> specialSpawners;
    [SerializeField] [Range(0, MAX_SPAWN_CHANCE)] private int chanceToSpawn;
    [SerializeField] private int spawnAmount = 2;
    private int spawnCount = 0;
    private GameController gameController;
    private GameObject activeSpawner;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.RegisterAmountOfMobs(spawnAmount);
        InvokeRepeating("MaybeSpawnSomething", 0, 5);
    }

    private void DetermineActiveSpawner()
    {
        int index = Random.Range(0, specialSpawners.Count - 1);
        activeSpawner = specialSpawners[index];
    }

    private void MaybeSpawnSomething()
    {
        if (spawnCount == spawnAmount)
        {
            return;
        }

        int chance = Random.Range(0, chanceToSpawn);
        
        if (gameController.NearEndOfRound)
        {
            chanceToSpawn = MAX_SPAWN_CHANCE;
        }

        if (chance <= chanceToSpawn)
        {
            DetermineActiveSpawner();
            SpawnSpecialMob();
            spawnCount++;
        }
    }

    private void SpawnSpecialMob()
    {
        GameObject mob = Instantiate(
            mobPrefab,
            new Vector3(activeSpawner.transform.position.x, activeSpawner.transform.position.y, 0),
            Quaternion.identity
        );
        mob.GetComponent<SpecialMob>().Spawn();
    }

    public void Setup(List<GameObject> spawners, int sAmount)
    {
        specialSpawners = spawners;
        spawnAmount = sAmount;
    }
}
