using System;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public string collectibleID; // unik per placeret prefab i scenen (sæt i inspector)
    public int value = 1;
    public bool respawn = false;
    public float respawnTime = 5f;
    public GameObject pickupEffect;

    public CollectibleManager manager;

    void OnValidate()
    {
        if (string.IsNullOrEmpty(collectibleID)) collectibleID = Guid.NewGuid().ToString();
    }

    public void Collect()
    {
        if (pickupEffect) Instantiate(pickupEffect, transform.position, Quaternion.identity);
        manager?.OnCollected(this);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Reactivate()
    {
        gameObject.SetActive(true);
    }
}
