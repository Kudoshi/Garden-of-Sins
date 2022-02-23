using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// Base class holding the audio settings
/// </summary>
[System.Serializable]
public class SoundStructure
{

    // Name of sound for searching purpose
    public string name;

    [Header("Audio Settings")]
    //Settings to be copied over to the audio source

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0f, 1f)]
    public float spatialBlend = .5f;
    [Range(.1f, 3f)]
    public float pitch = 1f;
    public bool loop;

    
}

/// <summary>
/// Attached to the sound repository
/// 
/// Contains audio clip
/// </summary>
[System.Serializable]
public class SoundRepoList : SoundStructure
{
    public AudioClip clip;
}

/// <summary>
/// Attached on sound listener
/// 
/// Has override global variable for overriding the global settings in Sound Repository
/// Enables update change of the audio variables in runtime
/// </summary>
[System.Serializable]
public class SoundAssignable : SoundStructure
{
    [Header("Config")]
    [Tooltip("If value is true, it will ignore the settings above and uses the settings in the repository")]
    public bool useGlobalValue = true;
    [Tooltip("If value is true, allows to change the audio settings in runtime")]
    public bool updateInRuntime;

    [HideInInspector]
    public AudioClip clip; //Holds local clip. Clip copied over from sound repo

    // Unity Gameobject component that holds and plays the sound
    [HideInInspector]
    public AudioSource source;
}