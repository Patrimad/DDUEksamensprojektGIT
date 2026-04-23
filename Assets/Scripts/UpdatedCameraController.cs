using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    [Header("Virtual Cameras")]
    public CinemachineCamera freeLookCam;
    public CinemachineCamera aimCam;

    [Header("Blend Settings")]
    public int defaultPriority = 10;
    public int activePriority = 15;

    [HideInInspector]
    public bool isAiming = false;

    private void Start()
    {
        freeLookCam.Priority = activePriority;
        aimCam.Priority = defaultPriority;
    }

    void OnAim(InputValue value)
    {
        isAiming = value.isPressed;

        if (isAiming)
        {
            aimCam.Priority = activePriority;
            freeLookCam.Priority = defaultPriority;
        }
        else
        {
            freeLookCam.Priority = activePriority;
            aimCam.Priority = defaultPriority;
        }
    }
}