using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TextMesh text;
    private int score;
    private Color colorSpecial = new Color(252, 186, 0, 255);
    private Color colorNormal = new Color(255, 255, 255, 255);

    // Start is called before the first frame update
    void Start()
    {
        text.text = "+" + score.ToString();
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            Vector3 movement = new Vector3(0, 2, 0);
            movement *= Time.deltaTime;
            transform.Translate(movement);

            yield return new WaitForSeconds(0.0015f);

            Color updatedColor = new Color(
                text.color.r,
                text.color.g,
                text.color.b,
                text.color.a - 0.002f
            );

            text.color = updatedColor;

            if (text.color.a <= 0)
            {
                break;
            }

            yield return new WaitForSeconds(0.001f);
        }

        Destroy(gameObject);
    }

    public void Setup(int _score, bool _isSpecial)
    {
        score = _score;

        if (_isSpecial)
        {
            text.color = colorSpecial;
        }
        else
        {
            text.color = colorNormal;
        }
    }
}
