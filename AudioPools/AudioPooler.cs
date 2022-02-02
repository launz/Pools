using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioPooler", menuName = "boyant/AudioPooler", order = 0)]
public class AudioPooler : MonoBehaviour
{
    //[Header("REFERENCES")]
    //[SerializeField] AudioHandler audioHandler;

    //public override void InitPools()
    //{
    //    base.InitPools();
    //    audioHandler.Init(objectPools[0]);
    //}
    //public GameObject SpawnAudioPlayer(ObjectPool _pool, Vector3 position_, AudioData _audioData) {
    //    GameObject newObj;
    //    // check if there are items left in pool
    //    if (_pool.queue.Count < 1) {
    //        // check if pool exceeds max size
    //        if (_pool.currentSize >= _pool.maxSize) {
    //            Debug.Log("#Warning: pool reached maximum items " + _pool.currentSize + " / " + _pool.maxSize);
    //            return null;
    //        }
    //        // if there are no items left but pool is not yet at max size:
    //        newObj = CreateNewItem(_pool);
    //    } 
    //    // take item from queue
    //    newObj = _pool.queue.Dequeue();
    //    // activate item and set right
    //    newObj.SetActive(true);
    //    newObj.transform.position = position_;
    //    // initialize audio player
    //    AudioPlayer audioPlayer = newObj.GetComponent<AudioPlayer>();
    //    audioPlayer.Init(_audioData);
    //    // hit OnSpawn function of item that got spawned
    //    IPooledObject pooledObj = newObj.GetComponent<IPooledObject>();
    //    if (pooledObj!=null) {
    //        pooledObj.Spawn(_pool);
    //    }
    //    return newObj;
    //}
}
