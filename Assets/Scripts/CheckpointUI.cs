using UnityEngine;

public class CheckpointUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        gameManager.OnPlayerCorrectCheckpoint += GameManager_OnPlayerCorrectCheckpoint;
        gameManager.OnPlayerIncorrectCheckpoint += GameManager_OnPlayerIncorrectCheckpoint;
        Hide();
    }

    private void GameManager_OnPlayerIncorrectCheckpoint(object sender, System.EventArgs e)
    {
        // Incorrect checkpoint logic here
        Debug.Log("Incorrect checkpoint reached!");
        Show();
    }

    private void GameManager_OnPlayerCorrectCheckpoint(object sender, System.EventArgs e)
    {
        // Correct checkpoint logic here
        Debug.Log("Correct checkpoint reached!");
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }


}
