using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCountdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private Canvas canvas;
    private GameObject gameControllerObject;
    private GameController gameController;

    private void Awake()
    {
        gameControllerObject = GameObject.Find("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    public IEnumerator Countdown(int seconds)
    {
        while (seconds >= 0)
        {
            countdownText.text = seconds.ToString();
            yield return new WaitForSeconds(1);
            seconds--;
        }
        countdownText.text = "";
        GameController.isCountingDown = false;
        gameController.LevelStart();
    }
}
