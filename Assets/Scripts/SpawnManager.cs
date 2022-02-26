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
        Debug.Log("Start pos: " + spawnPoints[0].spawnPos.position);
    }

    public void Respawn(Transform player)
    {
        for (int i = spawnPoints.Length-1; i >-1 ; i--)
        {
            if (spawnPoints[i].isActivated)
            {
                Vector3 spawnPos = new Vector3(spawnPoints[i].spawnPos.position.x, spawnPoints[i].spawnPos.position.y, player.transform.position.z);
                player.transform.position = spawnPos;
                Debug.Log(spawnPos);
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
            Debug.Log("Updated spawnpoint");
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
