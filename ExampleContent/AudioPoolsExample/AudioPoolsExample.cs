using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPoolsExample : MonoBehaviour
{
   [SerializeField] ObjectPool testAudioPool;
   [SerializeField] AudioData testAudioData;

    private void Awake()
    {
        AudioPools.InitAudioPool(testAudioPool);
    }

    public void PlaySound()
    {
        AudioPools.PlaySound(testAudioPool, testAudioData, Vector3.zero);
    }
}
