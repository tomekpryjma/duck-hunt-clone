using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject reloadEffectPrefab;
    private AudioSource audioSource;
    private ParticleSystem particles;
    private Animator animator;
    private string animationFireName;
    private string animationIdleName;
    private float rotationOffset = 90;
    private float rotationSpeed = 4;
    private float rotationClamp = 15;
    private bool isShooting;
    private float reloadSpeed = 0.8f;
    public string label = "Weapon";

    private void Start()
    {
        animationFireName = label + "_Fire";
        animationIdleName = label + "_Idle";
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        GameObject prefab = Instantiate(
            reloadEffectPrefab,
            new Vector3(transform.position.x, -4.99f, -10f),
            Quaternion.identity
        );
        particles = prefab.GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (isShooting)
            {
                StartCoroutine("Reload");
            }

            animator.Play(animationIdleName);
        }
    }
    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadSpeed);

        if (!particles.isPlaying)
        {
            particles.Play();
        }

        isShooting = false;
    }

    public void Rotate(Vector3 mousePosition)
    {
        Vector3 difference = mousePosition - transform.position;
        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float degrees = Mathf.Clamp(rotationZ - rotationOffset, -rotationClamp, rotationClamp);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.Euler(0f, 0f, degrees),
            rotationSpeed * Time.deltaTime
        );
    }

    public void Shoot()
    {
        isShooting = true;
        audioSource.Play();
        animator.Play(animationFireName);
    }

    public float GetCooldown()
    {
        return reloadSpeed;
    }
}
