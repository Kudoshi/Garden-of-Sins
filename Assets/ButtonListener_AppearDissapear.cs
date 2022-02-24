using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListener_AppearDissapear : ButtonListener
{
    public GameObject[] objectsToAppearance; //Objects that will appear and dissapear
    public bool shouldAppear = false; // Set true when activate you want it to appear

    private bool shouldActivate = false;


    private void Start()
    {
        //Set objects to initial result
        foreach (GameObject obj in objectsToAppearance)
        {
            obj.SetActive(!shouldAppear);
        }
    }

    protected override void CheckCondition()
    {
        if (buttonsRequired.Length == buttonEvents.Count) //All pressed
            shouldActivate = true;
        else //Not all is pressed
        {
            shouldActivate = false;
        }

        AppearDissapearObj();
    }

    private void AppearDissapearObj()
    {
        //Activated - appear it 
        if (shouldActivate)
        {
            foreach (GameObject obj in objectsToAppearance)
            {
                obj.SetActive(shouldAppear);
            }
        }
        else //Deactivated
        {
            foreach (GameObject obj in objectsToAppearance)
            {
                obj.SetActive(!shouldAppear);
            }
        }
        
    }
}
