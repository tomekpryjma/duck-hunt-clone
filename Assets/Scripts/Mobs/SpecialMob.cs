using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMob : Mob
{
    [SerializeField] private float flyAroundTime = 5f;
    private Transform moveToPoint;
    private Vector2 screenBounds;
    private bool flyAway;

    private void Awake()
    {
        minSpeed = 5;
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveToPoint = transform.GetChild(0);
        moveToPoint.parent = null;

        screenBounds = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)
        );

        Invoke("ShouldFlyAway", flyAroundTime);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void ShouldFlyAway()
    {
        if (isDead)
        {
            return;
        }

        flyAway = true;
        speed = 10;
    }

    private void Movement()
    {
        if (!flyAway && moveToPoint != null)
        {
            float distance = Vector3.Distance(moveToPoint.position, transform.position);
            if (distance <= 0.005)
            {
                moveToPoint.position = SelectRandomVector();
            }
        }
        else
        {
            moveToPoint.position = new Vector3(transform.position.x, screenBounds.y + 1, 0);
        }

        transform.position = Vector3.MoveTowards(transform.position, moveToPoint.position, speed * Time.deltaTime);
    }

    private Vector3 SelectRandomVector()
    {
        float randomX = Random.Range(screenBounds.x, screenBounds.x * -1);
        float randomY = Random.Range(screenBounds.y, 0);
        return new Vector3(randomX, randomY, 0);
    }

    public void Spawn()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void OnBecameInvisible()
    {
        if (!isDead)
        {
            Progress.AddToStat("misses");
        }

        Destroy(moveToPoint.gameObject);
        Destroy(gameObject);
    }
}
