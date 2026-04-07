using UnityEngine;

public class PlayerCoreFollow : MonoBehaviour
{
    public Transform playerModel;

    void FixedUpdate()
    {
        if (playerModel != null)
        {
            transform.position = playerModel.position;
        }
    }
}
