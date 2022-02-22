using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls all the interactions
/// </summary>
public class InteractionManager : MonoBehaviour
{
    public Vector2 interactionRadius;
    [Header("Debug")]
    public bool viewInteractionRadius;
    public Color color;
    /// <summary>
    /// Draw debug purpose interaction radius
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if(viewInteractionRadius)
        {
            Gizmos.color = color;
            Gizmos.DrawCube(transform.position, interactionRadius);
        }
    }
    /// <summary>
    /// PlayerInputController -> InteractionManager
    /// It does not care about anything. Just ping when it hits a player interactable
    /// </summary>
    public void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, interactionRadius, 0);
        // There are ground obj collided
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.GetComponent<PlayerInteractable>() != null)
            {
                colliders[i].gameObject.GetComponent<PlayerInteractable>().Interact();
                break;
            }
        }
    }
}
