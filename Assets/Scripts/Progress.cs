using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Progress
{
    private static Dictionary<string, float> levelStats;
    private static string currentLevel;
    public static string CurrentLevel { get { return currentLevel; } }
    public static float LevelKills { get { return levelStats["kills"]; } }
    public static float LevelMisses { get { return levelStats["misses"]; } }
    public static float TotalScore { get { return levelStats["total_score"]; } }

    public static void AddToStat(string statName, float value = 1f)
    {
        if (! GameController.isPlaying)
        {
            return;
        }

        float currentOverall = GetOverallStat(statName);
        levelStats[statName] += value;
        PlayerPrefs.SetFloat(statName, currentOverall + value);
    }

    public static void SaveLevel(int currentLevelIndex)
    {
        PlayerPrefs.SetInt("currentLevel", currentLevelIndex);
    }

    public static float GetOverallStat(string statName)
    {
        return PlayerPrefs.GetFloat(statName, 0);
    }

    public static void Reset()
    {
        levelStats = new Dictionary<string, float>();
        levelStats.Add("kills", 0f);
        levelStats.Add("shots", 0f);
        levelStats.Add("misses", 0f);
        levelStats.Add("total_score", 0f);
    }

    public static void NewGame()
    {
        PlayerPrefs.SetInt("currentLevel", 0);
        PlayerPrefs.SetFloat("kills", 0);
        PlayerPrefs.SetFloat("shots", 0);
        PlayerPrefs.SetFloat("misses", 0);
        PlayerPrefs.SetFloat("total_score", 0);
    }
}
