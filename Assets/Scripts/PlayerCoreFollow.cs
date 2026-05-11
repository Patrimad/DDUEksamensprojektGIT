using UnityEngine;

public class PlayerCoreFollow : MonoBehaviour
{
    public Transform playerModel;
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (playerModel != null)
        {
           
        }
    }
        void FixedUpdate()
    {
        if (playerModel != null)
        {
            transform.position = playerModel.position;
            transform.rotation = mainCamera.transform.rotation;
        }
    }
}