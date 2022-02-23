using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Throwaway : MonoBehaviour
{
    private void Awake()
    {
        
    }
    private void Start()
    {
        SoundRepoSO.PlayOneShotSound(gameObject, "TestMusic");
        Invoke("CallSound", 2);
    }

    private void CallSound()
    {
        SoundRepoSO.StopSound(gameObject, "TestMusic");
    }
}
