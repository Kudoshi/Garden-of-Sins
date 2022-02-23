using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

/// <summary>
/// Attaches to a game object
/// 
/// Attach whatever sounds you need to use in this script in the inspector
/// Allows you to override the global settings or use the local settings
/// Allows to change the sound settings in runtime
/// 
/// Input (GameObject, SoundName):
/// - OnPlaySound
/// - OnPlayOneShotSound
/// - OnStopSound
/// </summary>
public class SoundListener : MonoBehaviour
{
    public SoundRepoSO soundRepoSO;
    public AudioMixerGroup mixerGroup;
    public SoundAssignable[] soundList;

    
    private void Awake()
    {
        LoadSound();
    }

    ////////////////////
    /// SUBSCRIPTIONS
    ///////////////////

    private void OnEnable()
    {
        //Subscribe to event

        SoundRepoSO.onPlaySound += PlaySound;
        SoundRepoSO.onPlayOneShotSound += PlayOneShotSound;
        SoundRepoSO.onStopSound += StopSound;
    }
    private void OnDisable()
    {
        //Unsubscribe to event

        SoundRepoSO.onPlaySound -= PlaySound;
        SoundRepoSO.onPlayOneShotSound -= PlayOneShotSound;
        SoundRepoSO.onStopSound -= StopSound;
    }

    /// <summary>
    /// Loads sound from sound repository onto the game object
    /// Basically it caches the sound on instantiate
    /// 
    /// Gets the global audio setting from sound repository
    /// If use global value, copy over the global settings to local settings. Otherwise use local settings
    /// 
    /// Then spawn audio source component and copy over local settings to audio source
    /// </summary>
    private void LoadSound()
    {
        
        //Effect List
        foreach (SoundAssignable sound in soundList)
        {
            SoundRepoList globalAudio = soundRepoSO.GetAudioSetting(sound.name);

            //Copy global settings over to local setting
            if (sound.useGlobalValue)
            {
                
                sound.volume = globalAudio.volume;
                sound.spatialBlend = globalAudio.spatialBlend;
                sound.pitch = globalAudio.pitch;
                sound.loop = globalAudio.loop;

                //Copy settings over to local settings
            }


            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            sound.source = audioSource;
            sound.clip = globalAudio.clip;

            audioSource.outputAudioMixerGroup = mixerGroup;
            audioSource.clip = sound.clip;
            audioSource.volume = sound.volume;
            audioSource.spatialBlend = sound.spatialBlend;
            audioSource.pitch = sound.pitch;
            audioSource.loop = sound.loop;
            
        }
    }

    /// <summary>
    /// Receives the game object and sound name from the DialogueSO
    /// Checks to see if the game object is itself
    /// Then search in its array for a matching name then stop it
    /// 
    /// - Spits warning if sound is not found
    /// </summary>
    /// <param name="gameObj">Game Object</param>
    /// <param name="soundName">The name of the sound needed to stop</param>
    private void StopSound(GameObject gameObj, string soundName)
    {
        if (gameObj != gameObject)
        {
            return;
        }

        SoundAssignable audio = Array.Find(soundList, sound => sound.name == soundName);
        if (audio == null || audio.source == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found in game object - " + gameObj.name);
            return;
        }

        audio.source.Stop();
    }
    /// <summary>
    /// Plays one shot sound
    /// Received from SoundRepoSO
    /// Allow runtime changes on the audio source variable
    /// 
    /// For more info refer to StopSound()
    /// </summary>
    /// <param name="gameObj"></param>
    /// <param name="soundName"></param>
    private void PlayOneShotSound(GameObject gameObj, string soundName)
    {
        if (gameObj != gameObject)
        {
            return;
        }

        SoundAssignable audio = Array.Find(soundList, sound => sound.name == soundName);

        if (audio == null || audio.source == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found in game object - " + gameObj.name);
            return;
        }

        if (audio.updateInRuntime)
        {
            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.loop = audio.loop;
            audio.source.spatialBlend = audio.spatialBlend;
        }

        audio.source.PlayOneShot(audio.source.clip);

    }

    /// <summary>
    /// Refer 
    /// </summary>
    /// <param name="gameObj"></param>
    /// <param name="soundName"></param>
    private void PlaySound(GameObject gameObj, string soundName)
    {
        if (gameObj != gameObject)
        {
            return;
        }

        SoundAssignable audio = Array.Find(soundList, sound => sound.name == soundName);

        if (audio == null || audio.source == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found in game object - " + gameObj.name);
            return;
        }

        if (audio.updateInRuntime)
        {
            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.loop = audio.loop;
            audio.source.spatialBlend = audio.spatialBlend;
            
        }

        audio.source.Play();

    }
}
