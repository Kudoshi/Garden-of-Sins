using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{ 
    public Transform[] points;
    public float speed;

    private int currentPath = 0;

   

    // Update is called once per frame
    void Update()
    {
            

        if (gameObject == null || points.Length == 0 || points[points.Length - 1] == null)
        {
            Debug.LogWarning("gameObject: " + gameObject.name + " not found");
            Destroy(this);
            return;
        }
        if (transform.position.x == points[currentPath].position.x)
        {
            currentPath++;
            //if reach last point
            if (currentPath == points.Length)
            {
                currentPath = 0;
            }
        }

        Vector3 pointToTravel = new Vector3(points[currentPath].position.x, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, pointToTravel, speed * Time.deltaTime);
        FlipObj();
    }

    private void FlipObj()
    {
        //Target to the left of obj (Face left)
        if (transform.position.x >= points[currentPath].position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }//Target to right of obj (Face right)
        else if (transform.position.x <= points[currentPath].position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

    }
}
