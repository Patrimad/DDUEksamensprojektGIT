using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [Header("Zone Size")]
    public Vector3 zoneSize = new Vector3(10f, 1f, 10f);

    public GameObject enemyPrefab;

    [Header("Spawn Settings")]
    public int enemiesToSpawn = 5;
    public float spawnRate = 1f;

    public Transform[] patrolPosts;

    public Vector3 GetRandomSpawnPoint()
    {
        float x = Random.Range(-zoneSize.x / 2f, zoneSize.x / 2f);
        float y = 0;
        float z = Random.Range(-zoneSize.z / 2f, zoneSize.z / 2f);
        return transform.position + new Vector3(x, y, z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.3f, 0.3f, 0.9f);
        Gizmos.DrawWireCube(transform.position, zoneSize);
    }
}