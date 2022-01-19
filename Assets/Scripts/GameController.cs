using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject spawnerPrefab;
    [SerializeField] private GameObject specialSpawnerPrefab;
    [SerializeField] private GameObject specialSpawnersControllerObject;
    [SerializeField] private GameObject startCountdownObject;
    [SerializeField] private GameObject curtain;
    private StartCountdown startCountdown;
    private SpecialMobSpawns specialSpawnersController;
    private List<Level> levels;
    private List<GameObject> currentSpawners;
    private List<GameObject> currentSpecialSpawners;
    private Level currentLevel;
    private int mobsOnThisLevel = 0;
    private int endOfRoundMobThreshold;
    private int currentLevelIndex = 0;
    private int countdownSeconds = 3;
    private float countdownDelay = 1;
    public static bool nearEndOfRound = false;
    public static bool isCountingDown = true;
    public static bool isPlaying;

    private void Awake()
    {
        startCountdown = startCountdownObject.GetComponent<StartCountdown>();
        Progress.Reset();
        levels = new List<Level>();
        currentSpawners = new List<GameObject>();
        currentSpecialSpawners = new List<GameObject>();

        levels.Add(new LevelOne());
        levels.Add(new LevelTwo());
        levels.Add(new LevelThree());
        levels.Add(new LevelFour());

        specialSpawnersController = specialSpawnersControllerObject.GetComponent<SpecialMobSpawns>();

        currentLevel = levels[currentLevelIndex];
        StartCoroutine(curtain.GetComponent<Curtain>().FlushIn(null));
        StartCoroutine(startCountdown.Countdown(countdownSeconds, countdownDelay));
    }

    private void SceneSetup(Level level)
    {
        Debug.Log(currentLevelIndex);
        nearEndOfRound = false;
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
            currentSpawners.Add(spawner);
        }

        foreach (Dictionary<string, float> s in level.specialSpawners)
        {
            GameObject spawner = Instantiate(
                specialSpawnerPrefab,
                new Vector3(s["x"], s["y"], 0),
                Quaternion.identity
            );
            specialSpawners.Add(spawner);
            currentSpecialSpawners.Add(spawner);
        }

        specialSpawnersController.Setup(specialSpawners, (int) level.specialSpawnAmount);
        endOfRoundMobThreshold = level.endOfRoundMobThreshold;
    }

    private void RemoveCurrentLevelItems()
    {
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
        currentSpawners.Clear();
        currentSpecialSpawners.Clear();
    }

    private void Start()
    {
        isPlaying = true;
    }

    private void Update()
    {
        /**
         * TODO: Change this to something like (TotalSpawnsInLevel - AlreadySpawnedInLevel == endOfRoundMobThreshold)
         * Possibly use mobsOnThisLevel - but this is actually wrongly named as "ThisLevel"
         * refers to the entire playthrough, so should ideally be renamed to mobsInTotal.
         */
        if (mobsOnThisLevel != 0 && mobsOnThisLevel - Progress.LevelKills == endOfRoundMobThreshold)
        {
            nearEndOfRound = true;
        }

        if (mobsOnThisLevel != 0 && Progress.LevelKills + Progress.LevelMisses == mobsOnThisLevel)
        {
            NextLevel();
        }
    }

    private void GoToEnd()
    {
        float delay = 2;
        isPlaying = false;
        StartCoroutine(curtain.GetComponent<Curtain>().FlushOut(ChangeToEndScreen, delay));
    }

    private static void ChangeToEndScreen()
    {
        SceneManager.LoadScene("End");
    }

    public void RegisterAmountOfMobs(int spawnAmount)
    {
        mobsOnThisLevel += spawnAmount;
    }

    public void NextLevel()
    {
        if (currentLevelIndex + 1 > levels.Count - 1)
        {
            GoToEnd();
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
