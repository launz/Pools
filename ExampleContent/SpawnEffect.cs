using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour, IPooledObject
{
    [SerializeField] AnimationCurve animCurve;

    public void OnSpawn()
    {
        Debug.Log("object spawned");

        StartCoroutine(SpawnAnimCR());
    }

    public void OnDespawn()
    {
        Debug.Log("object despawned");
    }

    private IEnumerator SpawnAnimCR()
    {
        float elapsedTime = 0f;
        float dur = .6f;
        float progress = 0f;
        float startY = transform.position.y;

        while (elapsedTime < dur)
        {
            elapsedTime += Time.deltaTime;
            progress = elapsedTime / dur;
            transform.position = new Vector3(
                transform.position.x,
                startY + animCurve.Evaluate(progress),
                transform.position.z);

            yield return 0;
        }
        yield return null;
    }
}
