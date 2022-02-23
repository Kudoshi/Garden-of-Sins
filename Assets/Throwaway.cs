using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Throwaway : MonoBehaviour
{
    public GameObject pf;

    public Vector3 mousePos;
    public float range;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(mousePos, 2);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = transform.position.z;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 direction = mousePos - transform.position;
            Vector3 pos = transform.position + direction;
            Debug.Log("Mouse Pos: " + pos);
            Debug.Log("Transform: " + transform.position);
            if (pos.x> (transform.position.x + range))
            {
                Debug.Log("Right");
                pos.x = transform.position.x + range;
            }
            else if(pos.x< (transform.position.x - range))
            {
                Debug.Log("Left");

                pos.x = (transform.position.x - range);
            }

            if (pos.y> (transform.position.y + range))
            {
                Debug.Log("Up");

                pos.y = (transform.position.y + range);  
            }
            else if (pos.y < (transform.position.y - range))
            {
                Debug.Log("Down");

                pos.y = (transform.position.y - range);
            }

            Debug.Log(pos);
            Instantiate(pf, pos, pf.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            
        }
    }
}
