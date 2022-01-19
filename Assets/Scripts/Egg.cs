using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject scorePopupPrefab;
    private Animator animator;
    private AudioSource audioSource;
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb2d;
    private int score = 5;

    private const string ANIM_EXPLOSION = "Eggsplosion";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        circleCollider = GetComponent<CircleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        circleCollider.enabled = false;
        Invoke("Drop", 0.5f);
    }

    private void OnMouseDown()
    {
        if (!GameController.isPlaying)
        {
            return;
        }

        Eggsplode();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void Eggsplode()
    {
        Helpers.ShowScore(transform.position, scorePopupPrefab, score, false);
        rb2d.velocity = Vector3.zero;
        rb2d.gravityScale = 0;
        circleCollider.enabled = true;
        audioSource.Play();
        animator.Play(ANIM_EXPLOSION);
    }

    private void EndOfExplosionAnimation()
    {
        Destroy(gameObject);
    }

    private void Drop()
    {
        rb2d.gravityScale = 0.4f;
    }
}
