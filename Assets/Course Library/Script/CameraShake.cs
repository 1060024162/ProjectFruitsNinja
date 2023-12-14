using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // ��ʼλ��
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

            yield return null; // �ȴ���һ֡
        }

        transform.position = initialPosition; // ���������󣬻ָ���ʼλ��
    }

    // �������������ʼ����
    public void StartShake(float shakeDuration, float shakeMagnitude)
    {
        StopAllCoroutines(); // ֹ֮ͣǰ�Ķ���Ч��
        StartCoroutine(Shake(shakeDuration,shakeMagnitude)); // ��ʼ�µĶ���Ч��
    }
}