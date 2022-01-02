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
}
