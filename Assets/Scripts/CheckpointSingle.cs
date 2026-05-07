using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private GameManager gameManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            gameManager.PlayerThroughCheckpoint(this);
        }
    }

    public void SetCheckpoints(GameManager gameManager)
    {
                this.gameManager = gameManager;
    }
}

