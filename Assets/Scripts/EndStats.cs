using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killsText;
    [SerializeField] private TextMeshProUGUI shotsText;
    [SerializeField] private TextMeshProUGUI missesText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private GameObject curtain;
    private Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator>();
    private float wait = 1;

    private void Awake()
    {
        coroutineQueue.Enqueue(StatDisplayLoop("kills", killsText));
        coroutineQueue.Enqueue(StatDisplayLoop("shots", shotsText));
        coroutineQueue.Enqueue(StatDisplayLoop("misses", missesText));
        coroutineQueue.Enqueue(StatDisplayLoop("total_score", totalScoreText));
    }

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        StartCoroutine(curtain.GetComponent<Curtain>().FlushIn(null));

        yield return new WaitForSeconds(wait);

        while (true)
        {
            while (coroutineQueue.Count > 0)
            {
                yield return StartCoroutine(coroutineQueue.Dequeue());
            }
            yield return null;
        }
    }

    private IEnumerator StatDisplayLoop(string statName, TextMeshProUGUI text)
    {
        float statValue = Progress.GetOverallStat(statName);

        for (int i = 1; i <= (int)statValue; i++)
        {
            text.text = i.ToString();
            yield return new WaitForSeconds(wait / statValue);
        }
    }
}
