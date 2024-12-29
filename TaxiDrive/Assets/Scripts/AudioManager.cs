using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource musicSource;
    private AudioSource sfxSource;
    public AudioClip[] musicPlaylist;
    private int currentMusicIndex = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = false;
        musicSource.volume = 0.5f;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
    }

    void Start()
    {
        if (musicPlaylist.Length > 0)
        {
            PlayMusic(currentMusicIndex);
        }
    }

    void Update()
    {
        if (!musicSource.isPlaying && musicPlaylist.Length > 0)
        {
            PlayNextMusic();
        }
    }

    public void PlayMusic(int index)
    {
        if (index >= 0 && index < musicPlaylist.Length)
        {
            musicSource.clip = musicPlaylist[index];
            musicSource.Play();
        }
    }

    public void PlayNextMusic()
    {
        currentMusicIndex = (currentMusicIndex + 1) % musicPlaylist.Length;
        PlayMusic(currentMusicIndex);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }
}
