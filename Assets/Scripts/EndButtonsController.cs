using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject curtain;

    public void QuitGame()
    {
        GameController.QuitGame();
    }
    public void NewGame()
    {
        StartCoroutine(curtain.GetComponent<Curtain>().FlushOut(GameController.NewGame, 0));
    }
}
