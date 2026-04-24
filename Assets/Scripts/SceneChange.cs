using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ButtonMoveScene(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}