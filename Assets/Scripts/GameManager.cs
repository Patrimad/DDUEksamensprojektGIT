using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerIncorrectCheckpoint;

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
    [SerializeField] private Image healthUI;

    private CinemachineBrain cinemachineBrain;

    private List<CheckpointSingle> checkpointSingleList = new List<CheckpointSingle>();
    private int nextCheckpointSingleIndex;

    private void Awake()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        Transform checkpointsTransform = transform.Find("Checkpoints");

        //checkpointSingleList = new List<CheckpointSingle>();
        foreach (Transform checkpointSingleTransform in checkpointsTransform)
        {
           CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetCheckpoints(this);
            checkpointSingleList.Add(checkpointSingle);
        }

        nextCheckpointSingleIndex = 0;

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
        Color c = healthUI.color;
        c.a = 1f;
        healthUI.color = c;
        if (healthUI != null) { c.a = 1f; }

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
    
    
    
    public void GoToMainMenu(string menuSceneName = "MainMenu")
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(menuSceneName);
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle)
    {
        if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            //Rigtige checkpoint
            Debug.Log("Forkert checkpoint");
            CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
            correctCheckpointSingle.Hide();

            nextCheckpointSingleIndex = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
                       //Forkert checkpoint, måske reset til start eller noget?
            Debug.Log("Forkert checkpoint");
            OnPlayerIncorrectCheckpoint?.Invoke(this, EventArgs.Empty);

            CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
            correctCheckpointSingle.Show();
        }
    }
}