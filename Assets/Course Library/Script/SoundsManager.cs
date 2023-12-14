using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; private set; }

    private void Awake()
    {
        // ȷ��ֻ��һ��ʵ��
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ʹ����Ч�������糡����������
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        // ����һ����ʱ����Ϸ������������Ч
        GameObject tempAudioSource = new GameObject("TempAudio");
        AudioSource audioSource = tempAudioSource.AddComponent<AudioSource>();

        // ������Ч������
        audioSource.clip = clip;
        audioSource.Play();
        Debug.Log("PlaySound");

        // ������ʱ��������Ϸ���󣬵���Ч������Ϻ�
        Destroy(tempAudioSource, clip.length);
    }
}
