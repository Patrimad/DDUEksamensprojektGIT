using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("References: Player")]
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CameraGunControll cameraGunControll;

    [Header("References:  Cameras")]
    [SerializeField] private CinemachineCamera aimCamera;
    [SerializeField] private CinemachineCamera freeLookCam;
    private CinemachineBrain cinemachineBrain;

    [Header("References: UI")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject deathDisplay;

    
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (Keyboard.current.escapeKey.isPressed && Time.timeScale == 1f)
            {
                Pause();
                
            }
            else if (Time.timeScale == 0f && deathDisplay.activeSelf == false)
            {
                Resume();
                cameraGunControll.isAiming = false;
                Debug.Log("PauseMenu: Resuming game from pause menu.");
            }
        }
    }

    void Pause()
    {
        playerInput.actions["Look"].Disable();
        playerInput.actions["Attack"].Disable();
        playerInput.actions["Aim"].Disable();

        if (cinemachineBrain != null) { cinemachineBrain.enabled = false; }
        if (cameraGunControll != null) { cameraGunControll.enabled = false; }
        if (freeLookCam != null) { freeLookCam.enabled = false; }
        if (aimCamera != null) aimCamera.enabled = false;
        if (crosshair != null) { crosshair.SetActive(false); }

        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Resume()
    {
        playerInput.actions["Look"].Enable();
        playerInput.actions["Attack"].Enable();
        playerInput.actions["Aim"].Enable();

        if (cinemachineBrain != null) { cinemachineBrain.enabled = true; }
        if (cameraGunControll != null) { cameraGunControll.enabled = true; }
        if (freeLookCam != null) { freeLookCam.enabled = true; }
        if (aimCamera != null) aimCamera.enabled = true;

        pauseMenuUI.SetActive(false);

        //cameraGunControll.enabled = true;
        Debug.Log("PauseMenu: Resuming game, isAiming set to false.");

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
