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

    private void RemoveCurrentLevelItems()
    {
        Debug.Log("Removing previous");
        if (currentSpawners.Count != 0)
        {
            foreach (GameObject spawner in currentSpawners)
            {
                Destroy(spawner);
            }
        }
        
        if (currentSpecialSpawners.Count != 0)
        {
            foreach (GameObject spawner in currentSpecialSpawners)
            {
                Destroy(spawner);
            }
        }
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
            NextLevel();
        }
    }

    public void RegisterAmountOfMobs(int spawnAmount)
    {
        mobsOnThisLevel += spawnAmount;
    }

    public void NextLevel()
    {
        if (currentLevelIndex + 1 > levels.Count - 1)
        {
            SceneManager.LoadScene("End");
            return;
        }
        currentLevel = levels[++currentLevelIndex];
        LevelStart();
    }

    public void LevelStart()
    {
        RemoveCurrentLevelItems();
        SceneSetup(currentLevel);
    }

    public static void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public static void UnPause()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public static void NewGame()
    {
        Progress.NewGame();
        SceneManager.LoadScene("Main");
    }

    public static void Resume()
    {
        SceneManager.LoadScene("Main");
    }

    public static void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
