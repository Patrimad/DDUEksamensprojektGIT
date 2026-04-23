using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance { get; private set; }
    public List<Collectibles> allCollectibles = new List<Collectibles>();
    public int totalScore = 0;
    

    Dictionary<string, bool> collectedState = new Dictionary<string, bool>();

    void Awake()
    {
        if (Instance == null) Instance = this; 
        else Destroy(gameObject);
    }

    void Start()
    {
        // registrer og sammenkobl
        foreach (var c in allCollectibles)
        {
            if (c != null) c.manager = this;
            if (!collectedState.ContainsKey(c.collectibleID))
                collectedState[c.collectibleID] = false;
        }
        ApplyStates();
    }

    public void OnCollected(Collectibles c)
    {
        if (c == null) return;
        if (collectedState.ContainsKey(c.collectibleID) && collectedState[c.collectibleID]) return; // allerede samlet

        collectedState[c.collectibleID] = true;
        totalScore += c.value;
        UI_CollectibleUI.Instance?.UpdateScore(totalScore);

        if (c.respawn)
        {
            c.Deactivate();
            StartCoroutine(RespawnCoroutine(c, c.respawnTime));
        }
        else
        {
            c.Deactivate();
        }


    }

    IEnumerator RespawnCoroutine(Collectibles c, float delay)
    {
        yield return new WaitForSeconds(delay);
        collectedState[c.collectibleID] = false;
        c.Reactivate();
    }

    void ApplyStates()
    {
        foreach (var c in allCollectibles)
        {
            if (c == null) continue;
            bool wasCollected = collectedState.ContainsKey(c.collectibleID) && collectedState[c.collectibleID];
            c.gameObject.SetActive(!wasCollected || c.respawn);
        }
    }


}
