using UnityEngine;

public class CheckpointUI : MonoBehaviour
{
    [SerializeField] private WorldCheckpoints worldCheckpoints;

    private void Start()
    {
        worldCheckpoints.OnPlayerCorrectCheckpoint += WorldCheckpoints_OnPlayerCorrectCheckpoint;
        worldCheckpoints.OnPlayerIncorrectCheckpoint += WorldCheckpoints_OnPlayerIncorrectCheckpoint;
        Hide();
    }

    private void WorldCheckpoints_OnPlayerIncorrectCheckpoint(object sender, System.EventArgs e)
    {
        // Incorrect checkpoint logic here
        Debug.Log("Incorrect checkpoint reached!");
        Show();
    }

    private void WorldCheckpoints_OnPlayerCorrectCheckpoint(object sender, System.EventArgs e)
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
