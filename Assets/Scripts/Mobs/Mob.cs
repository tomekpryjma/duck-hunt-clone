using UnityEngine;

public class Mob : MonoBehaviour
{
    private float fallDownDelay = 0.3f;
    [SerializeField] protected GameObject feathersEffectPrefab;
    [SerializeField] protected int maxSpeed = 5;
    protected int minSpeed = 1;
    protected float speed;
    protected int direction;
    protected Rigidbody2D rb2d;
    protected bool isDead = false;
    public int health = 5;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 movement = new Vector3(speed * direction, 0, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);
    }

    private void OnBecameInvisible()
    {
        if (!isDead)
        {
            Progress.AddToStat("misses");
        }

        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        if (!Player.canShoot)
        {
            return;
        }

        if (isDead)
        {
            return;
        }

        isDead = true;
        speed = 0;

        Instantiate(
            feathersEffectPrefab,
            new Vector3(transform.position.x, transform.position.y, 0f),
            Quaternion.identity
        );

        Progress.AddToStat("kills");

        Invoke("FallDown", fallDownDelay);
    }

    private void FallDown()
    {
        rb2d.gravityScale = 5;
    }

    /**
     * Called by a Spawner right after the Mob is instantiated.
     */
    public void Spawn(int dir)
    {
        speed = Random.Range(minSpeed, maxSpeed);
        direction = dir;
    }
}
