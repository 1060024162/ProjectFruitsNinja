using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // 初始位置
    private Vector3 initialPosition;

    void Awake()
    {
        if (transform == null)
        {
            Debug.LogError("Transform is NULL.");
        }
    }

    void OnEnable()
    {
        initialPosition = Camera.main.transform.position;
    }

    public IEnumerator Shake(float shakeDuration,float shakeMagnitude )
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = initialPosition.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = initialPosition.y + Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = new Vector3(x, y, initialPosition.z);

            elapsed += Time.deltaTime;

            yield return null; // 等待下一帧
        }

        transform.position = initialPosition; // 抖动结束后，恢复初始位置
    }

    // 调用这个方法开始抖动
    public void StartShake(float shakeDuration, float shakeMagnitude)
    {
        StopAllCoroutines(); // 停止之前的抖动效果
        StartCoroutine(Shake(shakeDuration,shakeMagnitude)); // 开始新的抖动效果
    }
}