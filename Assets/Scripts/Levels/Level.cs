using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public List<Dictionary<string, float>> spawners;
    public List<Dictionary<string, float>> specialSpawners;
    public float specialSpawnAmount;
    public float spawnSpeed;
    public Level next;
    public int endOfRoundMobThreshold;

    protected virtual Dictionary<string, float> Spawner(float x, float y, float direction, float spawnAmount, float spawnSpeed)
    {
        Dictionary<string, float> dict = new Dictionary<string, float>();
        dict.Add("x", x);
        dict.Add("y", y);
        dict.Add("direction", direction);
        dict.Add("spawnAmount", spawnAmount);
        dict.Add("spawnSpeed", spawnSpeed);
        return dict;
    }

    protected virtual Dictionary<string, float> SpecialSpawner(float x, float y, float spawnAmount)
    {
        Dictionary<string, float> dict = new Dictionary<string, float>();
        dict.Add("x", x);
        dict.Add("y", y);
        dict.Add("spawnAmount", spawnAmount);
        return dict;
    }
}
