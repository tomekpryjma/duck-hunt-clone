using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;
    private bool isShooting;
    private float reloadSpeed = 0f;
    public string label = "Weapon";

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (isShooting)
            {
                StartCoroutine("Reload");
            }
        }
    }
    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadSpeed);

        isShooting = false;
    }

    public void Shoot()
    {
        isShooting = true;
        audioSource.Play();
    }

    public float GetCooldown()
    {
        return reloadSpeed;
    }
}
