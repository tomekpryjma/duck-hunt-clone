using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCountdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    private GameObject gameControllerObject;
    private GameController gameController;
    private AudioSource audioSource;

    private void Awake()
    {
        countdownText.enabled = false;
        gameControllerObject = GameObject.Find("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator Countdown(int seconds, float countdownDelay)
    {
        yield return new WaitForSeconds(countdownDelay);
        countdownText.enabled = true;

        while (seconds >= 1)
        {
            audioSource.Play();
            countdownText.text = seconds.ToString();
            yield return new WaitForSeconds(1);
            seconds--;
        }
        audioSource.pitch = 2;
        audioSource.Play();

        countdownText.text = "";
        GameController.isCountingDown = false;
        gameController.LevelStart();
    }
}
