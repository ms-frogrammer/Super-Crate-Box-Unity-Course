using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 origPos = new Vector3(0f, 0f, -100f);
        float elapsed = 0.0f;
        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = origPos + new Vector3(x, y, 0f);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = origPos;
    }
}