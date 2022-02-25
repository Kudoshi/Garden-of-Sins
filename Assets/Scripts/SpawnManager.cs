using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Manages the spawnpoints
/// 
/// </summary>
public class SpawnManager : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;
    private void Awake()
    {
        spawnPoints[0].isActivated = true;
    }

    public void Respawn(Transform player)
    {
        for (int i = spawnPoints.Length-1; i >-1 ; i--)
        {
            if (spawnPoints[i].isActivated)
            {
                player.transform.position = spawnPoints[i].spawnPos.position;
                break;
            }
        }
    }

    public void UpdateSpawnPoint(Transform spawnPos)
    {
        SpawnPoint spawnPoint = Array.Find(spawnPoints, point => point.spawnPos == spawnPos);

        if (spawnPoint != null)
        {
            spawnPoint.isActivated = true;
        }
    }

    private void Event_OnPlayerDie(Transform player)
    {
        Respawn(player);
    }


    private void OnEnable()
    {
        Event.OnPlayerDie += Event_OnPlayerDie;
    }
    private void OnDisable()
    {
        Event.OnPlayerDie -= Event_OnPlayerDie;
    }
    
}

[System.Serializable]
public class SpawnPoint
{
    public Transform spawnPos;
    [HideInInspector] public bool isActivated = false;
}
