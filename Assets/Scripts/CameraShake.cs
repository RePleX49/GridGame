using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
        

    public IEnumerator Shake(float ShakeDuration, float magnitude)
    {
        Vector2 OriginalPos = transform.localPosition;

        float elapsedTime = 0.0f;

        while(elapsedTime < ShakeDuration)
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector2(OriginalPos.x + x, OriginalPos.y + y);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = OriginalPos;
        Debug.Log(transform.localPosition);
    }
}
