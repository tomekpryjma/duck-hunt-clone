using System.Collections.Generic;

public class LevelOne : Level
{
    public LevelOne()
    {
        spawners = new List<Dictionary<string, float>>();
        specialSpawners = new List<Dictionary<string, float>>();
        specialSpawnAmount = 2;
        endOfRoundMobThreshold = 5;

        spawners.Add(Spawner(-10, 3, ((float)Direction.RIGHT), 10, 1));
        spawners.Add(Spawner(10, 1.5f, ((float)Direction.LEFT), 10, 2));

        specialSpawners.Add(SpecialSpawner(-5, -2, specialSpawnAmount));
    }
}
