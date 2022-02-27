using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Detects if there is an object with rigidbody inside of its collider
/// 
/// Allows multiple obj to be ontop. Requires atleast 1 obj to be ontop to activate
/// Requires a collider trigger
/// 
/// Outputs to event trigger button. Both true and false
/// True - Got rigidbody on top
/// False - No rigidbody at all on top
/// </summary>
public class WeightedPlate : MonoBehaviour
{
    public GameObject pressedObj;
    public GameObject unpressedObj;
    private bool isPressed = false;
    private List<GameObject> objOnTop;
    //Check for collision
    //Check whether the collider has a rigidbody

    /// <summary>
    /// Detects if there is rigidbody in its trigger
    /// </summary>
    /// <param name="other"></param>
    private void Awake()
    {
        objOnTop = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.GetComponent<Rigidbody2D>())
        {
            //If not exist. Add new
            if (!objOnTop.Find(obj => obj == collision.gameObject))
                objOnTop.Add(collision.gameObject);

            CheckActivation();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.root.GetComponent<Rigidbody2D>())
        {
            //If exist. Remove
            if (objOnTop.Find(obj => obj == collision.gameObject))
                objOnTop.Remove(collision.gameObject);

            CheckActivation();
        }
    }

    private void CheckActivation()
    {
        if (objOnTop.Count > 0)
        {
            isPressed = true;
            pressedObj.SetActive(true);
            unpressedObj.SetActive(false);
        }
        else
        {
            isPressed = false;
            pressedObj.SetActive(false);
            unpressedObj.SetActive(true);
        }

        Event.TriggerButtonTriggered(gameObject, isPressed);
    }
}
