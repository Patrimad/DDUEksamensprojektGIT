using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("References: Player")]
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CameraGunControll cameraGunControll;

    [Header("References:  Cameras")]
    [SerializeField] private CinemachineCamera aimCamera;
    [SerializeField] private CinemachineCamera freeLookCam;

    [Header("References: UI")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject deathDisplay;

    private CinemachineBrain cinemachineBrain;
    

    private void Awake()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    private void OnEnable()
    {
        if (healthSystem != null)
        {
            healthSystem.OnPlayerDied += HandlePlayerDeath;
        }
    }

    private void OnDisable()
    {
        if (healthSystem != null)
        {
            healthSystem.OnPlayerDied -= HandlePlayerDeath;
        }
    }
    

    private void HandlePlayerDeath()
    {
        playerInput.actions["Look"].Disable();
        playerInput.actions["Attack"].Disable();
        playerInput.actions["Aim"].Disable();
        
        if (cinemachineBrain != null) {cinemachineBrain.enabled = false;}
        if (cameraGunControll != null) {cameraGunControll.enabled = false;}
        if (freeLookCam != null) {freeLookCam.enabled = false;}
        if (aimCamera != null) aimCamera.enabled = false;
        
        if (crosshair != null) {crosshair.SetActive(false);}
        if (deathDisplay != null) {deathDisplay.SetActive(true);}
        
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("GameManager: Player is dead.");
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
    }
    
    public void GoToMainMenu(string menuSceneName = "MainMenu")
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(menuSceneName);
    }
}