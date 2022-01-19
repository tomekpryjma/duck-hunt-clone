using System.Collections.Generic;

public class LevelTwo: Level
{
    public LevelTwo()
    {
        spawners = new List<Dictionary<string, float>>();
        specialSpawners = new List<Dictionary<string, float>>();
        specialSpawnAmount = 3;
        endOfRoundMobThreshold = 5;

        spawners.Add(Spawner(-10, 3, ((float)Direction.RIGHT), 20, 1));
        spawners.Add(Spawner(10, 1.5f, ((float)Direction.LEFT), 9, 2));
        spawners.Add(Spawner(10, 0.5f, ((float)Direction.LEFT), 6, 3));

        specialSpawners.Add(SpecialSpawner(-5, -2, 2));
        specialSpawners.Add(SpecialSpawner(5, -2, 1));
    }
}
