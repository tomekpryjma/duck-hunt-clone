using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject roundOverModal;
    private int mobsOnThisLevel = 0;
    private int endOfRoundMobThreshold = 5;
    private bool nearEndOfRound = false;
    public bool NearEndOfRound { get { return nearEndOfRound; } }

    private void Awake()
    {
        Progress.Setup();
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
