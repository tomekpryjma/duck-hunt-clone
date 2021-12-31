using System.Collections.Generic;

public class LevelOne : Level
{
    public LevelOne()
    {
        name = "A nice day";
        spawners = new List<Dictionary<string, float>>();
        specialSpawners = new List<Dictionary<string, float>>();
        specialSpawnAmount = 2;

        spawners.Add(Spawner(-10, 3, ((float)Direction.RIGHT), 10, 1));
        spawners.Add(Spawner(10, 1.5f, ((float)Direction.LEFT), 5, 2));

        specialSpawners.Add(SpecialSpawner(-5, -2, 2));
    }

    protected override Dictionary<string, float> Spawner(float x, float y, float direction, float spawnAmount, float spawnSpeed)
    {
        Dictionary<string, float> dict = new Dictionary<string, float>();
        dict.Add("x", x);
        dict.Add("y", y);
        dict.Add("direction", direction);
        dict.Add("spawnAmount", spawnAmount);
        dict.Add("spawnSpeed", spawnSpeed);
        return dict;
    }

    protected override Dictionary<string, float> SpecialSpawner(float x, float y, float spawnAmount)
    {
        Dictionary<string, float> dict = new Dictionary<string, float>();
        dict.Add("x", x);
        dict.Add("y", y);
        dict.Add("spawnAmount", spawnAmount);
        return dict;
    }
}
