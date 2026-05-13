using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public SpawnManager spawnManager;
    public TextMeshProUGUI killCount;
    public TextMeshProUGUI enemyLeft;

    void Update()
    {
        killCount.text = "You killed: " + spawnManager.enemiesKilled + " Enemies";
        enemyLeft.text = "There are: " + spawnManager.enemiesAlive + " enemies left";
    }
}
