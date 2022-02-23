using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Sound system flow:
/// SoundRepoSO: SoundRepoList - contains all the global settings and audio clip
/// SoundListener: SoundAssignable - contains local settings and can override global settings
/// None: SoundStructure - Base class containing the audio settings
/// 
/// Steps to use it:
/// 1. Put clips and settings onto sound repo so
/// 2. Attach SoundListener component to a game object
/// 3. In sound listener's sound list. Name of sound must match the sound repository
/// </summary>
[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Sound Repository SO")]
public class SoundRepoSO : ScriptableObject
{
    public SoundRepoList[] soundRepo;

    public SoundRepoList GetAudioSetting(string soundName)
    {
        SoundRepoList audio = Array.Find(soundRepo, sound => sound.name == soundName);

        if (audio == null)
        {
            Debug.LogWarning("Audio Clip: " + soundName + "not found");
        }

        return audio;
    }


    /// <summary>
    /// Static events
    /// </summary>

    public static Action<GameObject, string> onPlaySound;

    public static void PlaySound(GameObject gameObj, string soundName)
    {
        onPlaySound?.Invoke(gameObj, soundName);
    }
    public static Action<GameObject, string> onPlayOneShotSound;

    public static void PlayOneShotSound(GameObject gameObj, string soundName)
    {
        onPlayOneShotSound?.Invoke(gameObj, soundName);
    }

    public static Action<GameObject, string> onStopSound;

    public static void StopSound(GameObject gameObj, string soundName)
    {
        onStopSound?.Invoke(gameObj, soundName);
    }
}
