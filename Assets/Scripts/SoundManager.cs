using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip backgroundMusic;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }

    void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (bgmSource != null && backgroundMusic != null)
        {
            bgmSource.clip = backgroundMusic;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }
    public void StopBGM()
    {
        if (bgmSource != null && backgroundMusic != null)
        {
            bgmSource.Stop();
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
