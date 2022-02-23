using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
/// <summary>
/// Attach this to the virtual camera
/// 
/// It controls the camera
/// 
/// Input from DialogueEventListener
/// </summary>
public class CameraController : MonoBehaviour
{

    private CinemachineVirtualCamera cam;
    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    public void OnDialogueStart(Dialogue dialogue)   
    {
        cam.m_Follow = dialogue.cinematicTarget;
        cam.enabled = true;
    }

    public void DisableCamera()
    {
        cam.enabled = false;
        cam.m_Follow = null;    
    }
}
