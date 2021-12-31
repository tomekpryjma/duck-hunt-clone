using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject roundOverModal;
    [SerializeField] private GameObject spawnerPrefab;
    [SerializeField] private GameObject specialSpawnerPrefab;
    [SerializeField] private GameObject specialSpawnersControllerObject;
    private SpecialMobSpawns specialSpawnersController;
    private int mobsOnThisLevel = 0;
    private int endOfRoundMobThreshold = 5;
    private bool nearEndOfRound = false;
    private List<Level> levels;
    private Level currentLevel;
    private int currentLevelIndex = 0;
    public bool NearEndOfRound { get { return nearEndOfRound; } }

    private void Awake()
    {
        Progress.Setup();
        levels = new List<Level>();

        levels.Add(new LevelOne());

        specialSpawnersController = specialSpawnersControllerObject.GetComponent<SpecialMobSpawns>();

        currentLevel = levels[currentLevelIndex];
        SceneSetup(currentLevel);
    }

    private void SceneSetup(Level level)
    {
        // TODO: add level name GameObject into scene
        Debug.Log("Now playing: " + level.name);

        List<GameObject> specialSpawners = new List<GameObject>();

        foreach (Dictionary<string, float> s in level.spawners)
        {
            GameObject spawner = Instantiate(
                spawnerPrefab,
                new Vector3(s["x"], s["y"], 0),
                Quaternion.identity
            );

            Spawner spawnerScript = spawner.GetComponent<Spawner>();
            spawnerScript.Setup(s["spawnAmount"], s["spawnSpeed"], s["direction"]);
        }

        foreach (Dictionary<string, float> s in level.specialSpawners)
        {
            GameObject spawner = Instantiate(
                specialSpawnerPrefab,
                new Vector3(s["x"], s["y"], 0),
                Quaternion.identity
            );
            specialSpawners.Add(spawner);
        }

        specialSpawnersController.Setup(specialSpawners, (int) level.specialSpawnAmount);
    }

    private void Start()
    {
        roundOverModal.SetActive(false);
    }

    private void Update()
    {
        if (Progress.LevelKills == endOfRoundMobThreshold)
        {
            nearEndOfRound = true;
        }

        if (Progress.LevelKills + Progress.LevelMisses == mobsOnThisLevel)
        {
            roundOverModal.SetActive(true);
        }
    }

    public void RegisterAmountOfMobs(int spawnAmount)
    {
        mobsOnThisLevel += spawnAmount;
    }
}
