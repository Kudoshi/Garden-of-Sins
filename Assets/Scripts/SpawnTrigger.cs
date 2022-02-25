using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Requires trigger collision
/// 
/// Notifies spawn manager when player has touched it
/// </summary>
public class SpawnTrigger : MonoBehaviour
{
    public SpawnManager spawnManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawnManager.UpdateSpawnPoint(transform);
        }
    }
}
