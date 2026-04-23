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
    public GameObject corshair;

    [Header("Settings")]
    [SerializeField] private float bulletSpeed = 50f;
    [SerializeField] private float maxRaycastDistance = 500f;
    [SerializeField] private LayerMask raycastMask;

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
        if (value.isPressed)
        {
            Shoot();
        }
    }

    void OnAim(InputValue value)
    {
        isAiming = value.isPressed;
    }
    
    IEnumerator ShowCorsair()
    {
        yield return new WaitForSeconds(0.25f);
        corshair.SetActive(enabled);
    }


    private void Shoot()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, maxRaycastDistance, raycastMask))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.origin + ray.direction * maxRaycastDistance;
        }

        Vector3 direction = (targetPoint - muzzlePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.LookRotation(direction));

        //bullet.GetComponent<Dart_Script>().SetVelocity(direction * bulletSpeed);
    }
}