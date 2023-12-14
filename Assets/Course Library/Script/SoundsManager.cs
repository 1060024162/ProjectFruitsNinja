using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; private set; }

    private void Awake()
    {
        // 确保只有一个实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 使得音效管理器跨场景不被销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        // 创建一个临时的游戏对象来播放音效
        GameObject tempAudioSource = new GameObject("TempAudio");
        AudioSource audioSource = tempAudioSource.AddComponent<AudioSource>();

        // 设置音效并播放
        audioSource.clip = clip;
        audioSource.Play();
        Debug.Log("PlaySound");

        // 销毁临时创建的游戏对象，当音效播放完毕后
        Destroy(tempAudioSource, clip.length);
    }
}
