using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject curtain;

    public void NewGame()
    {
        StartCoroutine(curtain.GetComponent<Curtain>().FlushOut(GameController.NewGame, 0));
    }

    public void QuitGame()
    {
        GameController.QuitGame();
    }
}
