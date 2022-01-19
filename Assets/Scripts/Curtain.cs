using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float wait = 0.005f;
    private float alphaChange = 0.01f;

    public IEnumerator FlushIn(Action? callback)
    {
        while (spriteRenderer.color.a > 0)
        {
            Color newColour = new Color(0, 0, 0, spriteRenderer.color.a - alphaChange);
            spriteRenderer.color = newColour;
            yield return new WaitForSeconds(wait);
        }

        if (callback != null)
        {
            callback();
        }
    }
    
    public IEnumerator FlushOut(Action? callback, float delay)
    {
        ResetAlphaToZero();

        yield return new WaitForSeconds(delay);

        spriteRenderer.enabled = true;

        while (spriteRenderer.color.a < 1)
        {
            Color newColour = new Color( 0, 0, 0, spriteRenderer.color.a + alphaChange);
            spriteRenderer.color = newColour;
            yield return new WaitForSeconds(wait);
        }

        if (callback != null)
        {
            callback();
        }
    }

    private void ResetAlphaToZero()
    {
        spriteRenderer.color = new Color(0, 0, 0, 0);
    }
}
