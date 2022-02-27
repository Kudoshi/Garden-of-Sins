using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FragmentCollect : MonoBehaviour
{
    [SerializeField] private UnityEvent onFragmentCollect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onFragmentCollect.Invoke();
            SoundRepoSO.PlaySound(gameObject, "CollectiblesCollect");
            Destroy(this);
        }
    }
}
