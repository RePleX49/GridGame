using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector2 OriginalPos = new Vector2(0, 0);

    public void GetInitialPos(Vector3 InitialPos)
    {
        OriginalPos = InitialPos;
    }

    public IEnumerator Shake(float ShakeDuration, float magnitude)
    {
        

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
