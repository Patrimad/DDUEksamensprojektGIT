using UnityEngine;

public class GunNozzleAim : MonoBehaviour
{
    [SerializeField] private float maxDistance = 500f;
    [SerializeField] private LayerMask aimMask;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint = Physics.Raycast(ray, out RaycastHit hit, maxDistance, aimMask)
            ? hit.point
            : ray.origin + ray.direction * maxDistance;

        Vector3 direction = (targetPoint - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}