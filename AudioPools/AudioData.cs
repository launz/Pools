using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// audiodata used by pooled audiosources
/// </summary>


[CreateAssetMenu(fileName = "AudioData", menuName = "OUT/Pools/AudioData", order = 1)]
public class AudioData : ScriptableObject
{
    public AudioClip[] audioClips;
    public float volume = 1f;
    public bool isRandomVolume = false;
    public Vector2 volumeRange = new Vector2(1,1);
    public float pitch = 1f;
    public bool isRandomPitch = false;
    public Vector2 pitchRange = new Vector2(1,1);
    public AudioMixerGroup audioMixerGroup;

    public AudioClip GetAudioClip() {
        return audioClips [Random.Range (0, audioClips.Length)];
    }

}
