using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LevelFour : Level
{
    public LevelFour()
    {
        spawners = new List<Dictionary<string, float>>();
        specialSpawners = new List<Dictionary<string, float>>();
        specialSpawnAmount = 1;
        endOfRoundMobThreshold = 5;

        spawners.Add(Spawner(-10, 3, ((float)Direction.RIGHT), 20, 1));
        spawners.Add(Spawner(10, 1.5f, ((float)Direction.LEFT), 10, 2));
        spawners.Add(Spawner(10, 0.5f, ((float)Direction.LEFT), 20, 1.5f));

        specialSpawners.Add(SpecialSpawner(5, -2, specialSpawnAmount));
    }
}
