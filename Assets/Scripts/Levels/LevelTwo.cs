using System.Collections.Generic;

public class LevelTwo: Level
{
    public LevelTwo()
    {
        name = "The second Level";
        spawners = new List<Dictionary<string, float>>();
        specialSpawners = new List<Dictionary<string, float>>();
        specialSpawnAmount = 2;

        spawners.Add(Spawner(-10, 3, ((float)Direction.RIGHT), 10, 1));
        spawners.Add(Spawner(10, 1.5f, ((float)Direction.LEFT), 8, 2));
        spawners.Add(Spawner(10, 0.5f, ((float)Direction.LEFT), 15, 3));

        specialSpawners.Add(SpecialSpawner(-5, -2, 2));
        specialSpawners.Add(SpecialSpawner(5, -2, 1));
    }
}
