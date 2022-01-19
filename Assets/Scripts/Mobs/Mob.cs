using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    private float fallDownDelay = 0.3f;
    [SerializeField] protected GameObject feathersEffectPrefab;
    [SerializeField] protected GameObject scorePopupPrefab;
    [SerializeField] protected float maxSpeed = 5;
    [SerializeField] protected List<string> animationPool;
    [SerializeField] protected GameObject eggPrefab;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected float minSpeed = 3;
    protected float speed;
    protected float baseAnimationSpeed = 2;
    protected int direction;
    protected Rigidbody2D rb2d;
    protected bool isDead;
    protected bool isCooked;
    protected bool isSpecial;
    protected int score;
    protected int eggDropChance = 10;
    protected string chosenAnimation;
    public int health = 5;

    private const string ANIM_COOKED = "Cooked";
    private const float DIE_FRAME = 0.5f;

    private void Awake()
    {
        score = 10;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator.speed = speed;
        PlayRandomAnimation();
        InvokeRepeating("DropEgg", 2, 4);
    }

    private void Update()
    {
        if (direction > 0)
        {
            spriteRenderer.flipX = true;
        }
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
        if (! GameController.isPlaying)
        {
            return;
        }

        if (!Player.canShoot)
        {
            return;
        }

        Die();
    }

    private void FallDown()
    {
        rb2d.gravityScale = 5;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetType() != typeof(CircleCollider2D))
        {
            return;
        }

        Cooked();
        Die();
    }

    protected void DropEgg()
    {
        if (isDead)
        {
            return;
        }

        int chance = Random.Range(0, 100);
        if (chance <= eggDropChance)
        {
            Instantiate(
                eggPrefab,
                transform.position,
                Quaternion.identity
            );
        }
    }

    protected void Die()
    {
        if (isDead)
        {
            return;
        }

        if (!isCooked)
        {
            animator.speed = 0;
            animator.Play(chosenAnimation, 0, DIE_FRAME);
        }

        isDead = true;
        speed = 0;
        Vector3 currentVector = new Vector3(transform.position.x, transform.position.y, 0f);

        Helpers.ShowScore(currentVector, scorePopupPrefab, score, isSpecial);

        Instantiate(
            feathersEffectPrefab,
            currentVector,
            Quaternion.identity
        );

        Progress.AddToStat("kills");

        Invoke("FallDown", fallDownDelay);
    }

    protected void Cooked()
    {
        isCooked = true;
        animator.Play(ANIM_COOKED);
    }

    protected void PlayRandomAnimation()
    {
        string animation;

        if (animationPool.Count == 1)
        {
            animation = animationPool[0];
        }
        else
        {
            int random = Random.Range(0, animationPool.Count);
            animation = animationPool[random];
        }

        chosenAnimation = animation;
        animator.Play(animation);
    }

    /**
     * Called by a Spawner right after the Mob is instantiated.
     */
    public void Spawn(int dir)
    {
        if (GameController.nearEndOfRound)
        {
            minSpeed *= 1.5f;
            maxSpeed *= 1.5f;
        }
        speed = Random.Range(minSpeed, maxSpeed);
        direction = dir;
    }
}
