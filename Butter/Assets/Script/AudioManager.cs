using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("BGM Settings")]
    public AudioSource bgmSource;         // 반복 재생할 BGM용 AudioSource
    public AudioClip[] bgmClips;         // Inspector에 BGM 클립들 등록
    [Range(0f, 1f)]
    public float bgmVolume = 0.5f;        // 기본 BGM 볼륨

    [Header("SFX Settings")]
    public AudioSource sfxSourcePrefab;   // 효과음 전용 AudioSource 프리팹
    public AudioClip[] sfxClips;         // Inspector에 SFX 클립들 등록
    [Range(0f, 1f)]
    public float sfxVolume = 1f;          // 기본 SFX 볼륨

    void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 초기 볼륨 세팅
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
