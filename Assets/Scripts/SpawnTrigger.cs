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
    public AnimationController animator;

    private void Awake()
    {
        //animator.ChangeAnimationState("FireOff");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawnManager.UpdateSpawnPoint(transform);
            animator.ChangeAnimationState("FireOn");
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
