using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { LEFT, RIGHT }

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject mobPrefab;
    [SerializeField] private Direction direction = Direction.LEFT;
    [SerializeField] private int spawnAmount = 5;
    [SerializeField] private float spawnSpeed = 1f;
    [SerializeField] private bool isDemo;
    private GameController gameController;
    private float mobPosYChange = 1.5f;

    private void Start()
    {
        if (!isDemo)
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            gameController.RegisterAmountOfMobs(spawnAmount);
        }
        StartCoroutine(Spawning());
    }

    private void Update()
    {
        if (GameController.nearEndOfRound)
        {
            spawnSpeed = 0.23f;
        }
    }

    private IEnumerator Spawning()
    {
        if (isDemo)
        {
            spawnSpeed = 2;
            while (true)
            {
                SpawnMob();
                yield return new WaitForSeconds(spawnSpeed);
            }
        }

        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnMob();
            yield return new WaitForSeconds(spawnSpeed);
        }
    }

    private void SpawnMob()
    {
        GameObject mob = Instantiate(
            mobPrefab,
            new Vector3(transform.position.x, DetermineMobYPos()),
            Quaternion.identity
        );

        mob.GetComponent<Mob>().Spawn(DirectionToInt());
    }

    private int DirectionToInt()
    {
        if (direction == Direction.LEFT)
        {
            return -1;
        }
        return 1;
    }

    private float DetermineMobYPos()
    {
        float min = transform.position.y - mobPosYChange;
        float max = transform.position.y + mobPosYChange;
        return Random.Range(min, max);
    }

    public void Setup(float sAmount, float sSpeed, float dir)
    {
        spawnAmount = (int) sAmount;
        spawnSpeed = (int) sSpeed;
        direction = (Direction) dir;
    }
}
