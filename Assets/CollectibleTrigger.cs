using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleTrigger : MonoBehaviour
{
    private bool isAlive = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isAlive)
        {
            collision.transform.root.GetComponent<Player>().AddCollectible();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            isAlive = false;
            SoundRepoSO.PlaySound(gameObject, "CollectiblesCollect");
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
