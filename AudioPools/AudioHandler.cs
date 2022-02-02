using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// scriptable object that is called to play sounds
/// </summary>
[CreateAssetMenu(fileName = "AudioHandler", menuName = "boyant/AudioHandler", order = 0)]
public class AudioHandler : ScriptableObject {
    [Header("REFERENCES")]
    [SerializeField] AudioPooler audioPooler;
    [Header("AUDIO DATA")]
    public AudioData testAudioData;
    ObjectPool audioObjectPool;
    // needs to be initialized with the object pool that is used for sounds
    public void Init(ObjectPool _audioObjectPool) {
        audioObjectPool = _audioObjectPool;
    }
    // public function to play most sounds
    public void PlaySound(AudioData _audioData, Vector3 _position) {
        //audioPooler.SpawnAudioPlayer(audioObjectPool, _position, _audioData);
    }
}
