using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButtonsController : MonoBehaviour
{
    public void QuitGame()
    {
        GameController.QuitGame();
    }
    public void NewGame()
    {
        GameController.NewGame();
    }
}
