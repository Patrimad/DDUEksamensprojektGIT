using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private GameManager gameManager;
    private MeshRenderer meshRenderer;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        Hide();
    }

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

    public void Show()
    {
        meshRenderer.enabled = true;
    }

    public void Hide()
    {
        meshRenderer.enabled = false;
    }
}


