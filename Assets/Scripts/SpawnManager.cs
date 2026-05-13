using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [Header("Spawn Zones")]
    public List<SpawnZone> spawnZones = new List<SpawnZone>();

    [Header("Stats")]
    public int totalSpawned;
    public int enemiesAlive;
    public int enemiesKilled;

    private List<Coroutine> activeRoutines = new List<Coroutine>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartSpawningAllZones();
    }

    public void StartSpawningAllZones()
    {
        foreach (SpawnZone zone in spawnZones)
        {
            Coroutine routine = StartCoroutine(SpawnRoutine(zone));
            activeRoutines.Add(routine);
        }
    }

    public void StopAllSpawning()
    {
        foreach (Coroutine routine in activeRoutines)
        {
            if (routine != null)
                StopCoroutine(routine);
        }
        activeRoutines.Clear();
    }

    private IEnumerator SpawnRoutine(SpawnZone zone)
    {
        float timeBetweenSpawns = 1f / zone.spawnRate;

        for (int i = 0; i < zone.enemiesToSpawn; i++)
        {
            SpawnEnemy(zone);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
    private void SpawnEnemy(SpawnZone zone)
    {
        Vector3 spawnPoint = zone.GetRandomSpawnPoint();

        if (NavMesh.SamplePosition(spawnPoint, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            spawnPoint = hit.position;

        GameObject enemy = Instantiate(zone.enemyPrefab, spawnPoint, Quaternion.identity);
        EnemyLogic logic = enemy.GetComponent<EnemyLogic>();
        if (logic != null && zone.patrolPosts.Length > 0)
            logic.posts = zone.patrolPosts;

        totalSpawned++;
        enemiesAlive++;
    }

    public void ReportEnemyKilled()
    {
        enemiesAlive--;
        enemiesKilled++;

        if (enemiesAlive < 0) enemiesAlive = 0;

        Debug.Log("Enemies alive: " + enemiesAlive + " | Killed: " + enemiesKilled + " | Total: " + totalSpawned);

        if (enemiesAlive == 0)
            Debug.Log("All enemies cleared!");
    }

    public void ResetCounters()
    {
        totalSpawned = 0;
        enemiesAlive = 0;
        enemiesKilled = 0;
    }
}