using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

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
    [SerializeField] private float minRaycastDistance = 2f; // skips near-camera geometry + player collider
    [SerializeField] private LayerMask raycastMask;         // make sure Player + Bullet layers are NOT in this mask

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        aimCamera.Priority = isAiming ? 10 : 0;
        freeLookCam.Priority = isAiming ? 0 : 10;
    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed) Shoot();
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