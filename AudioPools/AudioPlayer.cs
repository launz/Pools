using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// component to put on audiosource prefab that should be pooled
/// </summary>
public class AudioPlayer : MonoBehaviour
{
    [HideInInspector]
    public ObjectPool audioPool;
    AudioSource audioSource;
    // float timePlayed = 0f;
    float clipLength = 0f;
    Coroutine playingCoroutine;
    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void Init(AudioData _audioData) 
    {
        audioSource.clip = _audioData.GetAudioClip();
        clipLength = audioSource.clip.length; 
        audioSource.outputAudioMixerGroup = _audioData.audioMixerGroup;
        
        if (!_audioData.isRandomVolume) {
            audioSource.volume = _audioData.volume;
        } else {
            audioSource.volume = Random.Range(_audioData.volumeRange.x, _audioData.volumeRange.y);
        }

        if (!_audioData.isRandomPitch) {
            audioSource.pitch = _audioData.pitch;
        } else {
            audioSource.pitch = Random.Range(_audioData.pitchRange.x, _audioData.pitchRange.y);
        }
    }

    public void Play() 
    {
        if (playingCoroutine!=null) 
        {
            StopCoroutine(playingCoroutine);
            playingCoroutine=null;
        }
        playingCoroutine = StartCoroutine(PlayCR());
        audioSource.Play();
    }

    IEnumerator PlayCR() 
    {
        yield return new WaitForSeconds(clipLength + 0.2f);
        EndPlayback();
    }
    public void EndPlayback() 
    {
        if (playingCoroutine!=null) 
        {
            StopCoroutine(playingCoroutine);
            playingCoroutine=null;
        }
        AudioPools.Despawn(audioPool, gameObject);
    }
}
