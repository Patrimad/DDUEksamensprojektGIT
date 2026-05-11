using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private WorldCheckpoints worldCheckpoints;
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
            worldCheckpoints.PlayerThroughCheckpoint(this);
        }
    }

    public void SetCheckpoints(WorldCheckpoints worldCheckpoints)
    {
        this.worldCheckpoints = worldCheckpoints;
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


