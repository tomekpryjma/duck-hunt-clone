using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killsText;
    [SerializeField] private TextMeshProUGUI shotsText;
    [SerializeField] private TextMeshProUGUI missesText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PopulateStats());
    }

    private IEnumerator PopulateStats()
    {
        for (int i = 1; i <= (int)Progress.GetOverallStat("kills"); i++)
        {
            killsText.text = i.ToString();
            yield return new WaitForSeconds(0.005f);
        }

        for (int i = 1; i <= (int)Progress.GetOverallStat("shots"); i++)
        {
            shotsText.text = i.ToString();
            yield return new WaitForSeconds(0.005f);
        }

        for (int i = 1; i <= (int)Progress.GetOverallStat("misses"); i++)
        {
            missesText.text = i.ToString();
            yield return new WaitForSeconds(0.005f);
        }
    }
}
