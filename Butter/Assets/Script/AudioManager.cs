using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("BGM Settings")]
    public AudioSource bgmSource;         // �ݺ� ����� BGM�� AudioSource
    public AudioClip[] bgmClips;         // Inspector�� BGM Ŭ���� ���
    [Range(0f, 1f)]
    public float bgmVolume = 0.5f;        // �⺻ BGM ����

    [Header("SFX Settings")]
    public AudioSource sfxSourcePrefab;   // ȿ���� ���� AudioSource ������
    public AudioClip[] sfxClips;         // Inspector�� SFX Ŭ���� ���
    [Range(0f, 1f)]
    public float sfxVolume = 1f;          // �⺻ SFX ����

    void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // �ʱ� ���� ����
            bgmSource.volume = bgmVolume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(int clipIndex, bool loop = true)
    {
        if (clipIndex < 0 || clipIndex >= bgmClips.Length) return;
        bgmSource.clip = bgmClips[clipIndex];
        bgmSource.loop = loop;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= sfxClips.Length)
            return;
        bgmSource.PlayOneShot(sfxClips[clipIndex], sfxVolume);
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }
}
