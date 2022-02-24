using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached on objects to destroy itself
/// </summary>
public class GetDestroyed : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
