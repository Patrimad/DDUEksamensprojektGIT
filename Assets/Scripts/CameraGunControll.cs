using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using NUnit.Framework;
using System.Collections.Generic;

public class CameraGunControll : MonoBehaviour
{
    public bool isAiming; 

    [Header("References")]
    [SerializeField] private Transform muzzlePoint;
    public GameObject bulletPrefab;
    public CinemachineCamera freeLookCam;
    public CinemachineCamera aimCamera;
    public GameObject crosshair;

    [Header("Settings")]
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float maxRaycastDistance = 500f;
    [SerializeField] private float minRaycastDistance = 2f;
    [SerializeField] private LayerMask raycastMask;

    [Header("Audio")]
    private AudioSource dartSFX;
    public List<AudioClip> darts = new List<AudioClip>();

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        dartSFX = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        aimCamera.Priority = isAiming ? 10 : 0;
        freeLookCam.Priority = isAiming ? 0 : 10;
    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            Shoot();
            if(darts != null)
            {
                int randomIndex = Random.Range(0, darts.Count);
                dartSFX.PlayOneShot(darts[randomIndex], Random.Range(0.85f, 1.25f));

            }
        }
    }

    void OnAim(InputValue value)
    {
        isAiming = value.isPressed;
        StartCoroutine(ShowCrosshair());
    }

    IEnumerator ShowCrosshair()
    {
        yield return new WaitForSeconds(0.25f);
        crosshair.SetActive(isAiming);
    }

    private void Shoot()
    {
        Vector3 direction = muzzlePoint.forward;

        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.LookRotation(direction));

        if (bullet.TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
}