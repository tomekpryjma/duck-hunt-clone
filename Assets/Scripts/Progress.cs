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

    public static void AddToStat(string statName, float value = 1f)
    {
        float currentOverall = GetOverallStat(statName);
        levelStats[statName] += value;
        PlayerPrefs.SetFloat(statName, currentOverall + value);
    }

    public static void SaveLevel(string sceneName)
    {
        PlayerPrefs.SetString("currentLevel", sceneName);
    }

    public static float GetOverallStat(string statName)
    {
        return PlayerPrefs.GetFloat(statName, 0);
    }

    public static void Setup()
    {
        levelStats = new Dictionary<string, float>();
        levelStats.Add("kills", 0f);
        levelStats.Add("shots", 0f);
        levelStats.Add("misses", 0f);
    }
}
