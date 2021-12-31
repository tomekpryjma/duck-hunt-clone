using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level
{
    public string name;
    public List<Dictionary<string, float>> spawners;
    public List<Dictionary<string, float>> specialSpawners;
    public int spawnAmount;
    public float spawnSpeed;
    public Level next;

    protected abstract Dictionary<string, float> Spawner(float x, float y, float direction, float spawnAmount, float spawnSpeed);
    protected abstract Dictionary<string, float> SpecialSpawner(float x, float y, float spawnAmount);
}
