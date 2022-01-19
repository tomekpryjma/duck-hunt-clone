using System.Collections.Generic;

public class LevelThree : Level
{
    public LevelThree()
    {
        spawners = new List<Dictionary<string, float>>();
        specialSpawners = new List<Dictionary<string, float>>();
        specialSpawnAmount = 1;
        endOfRoundMobThreshold = 5;

        spawners.Add(Spawner(-10, 3, ((float)Direction.RIGHT), 15, 1));
        spawners.Add(Spawner(10, 1.5f, ((float)Direction.LEFT), 10, 2));
        spawners.Add(Spawner(-10, 0.5f, ((float)Direction.RIGHT), 15, 1.8f));

        specialSpawners.Add(SpecialSpawner(5, -2, specialSpawnAmount));
    }
}
