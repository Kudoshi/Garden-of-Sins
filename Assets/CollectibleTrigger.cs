using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleTrigger : MonoBehaviour
{
    public int amt = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.root.GetComponent<Player>().AddCollectible(amt);
            Destroy(gameObject);
        }
    }
}
