using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    //Singleton
    public static SoundManager instance;

    //AudioMixer
    public AudioMixer m_audioMixer;
    public AudioMixer AudioMixer { get => m_audioMixer; set => m_audioMixer = value; }

    //AudioSources
    private AudioSource m_backgroundSource = null;
    public AudioSource BackgroundSource { get => m_backgroundSource; set => m_backgroundSource = value; }
    private AudioSource m_sfxSource = null;
    public AudioSource SfxSource { get => m_sfxSource; set => m_sfxSource = value; }
    private AudioSource m_masterSource;
    public AudioSource MasterSource { get => m_masterSource; set => m_masterSource = value; }

    //Background AudioClip
    private AudioClip m_backgroundAudioClip = null;
    public AudioClip BackgroundAudioClip { get => m_backgroundAudioClip; set => m_backgroundAudioClip = value; }
    //Sfx AudioClip
    private AudioClip m_sfxAudioClip = null;
    public AudioClip SfxAudioClip { get => m_sfxAudioClip; set => m_sfxAudioClip = value; }

    //Features
    private bool isActiveMuteSfx = false;
    private bool isActiveMuteMusic = false;
    private float currentSfx = 0.0f;
    private float currentMusic = 0.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        AudioMixer = Resources.Load("MasterAudioMixer") as AudioMixer;
        BackgroundSource = GetComponent<AudioSource>();
        SfxSource = GetComponent<AudioSource>();
        MasterSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        MasterSource.Play();
    }

    public void PlayBackground(string Route)
    {
        BackgroundAudioClip = Resources.Load(Route) as AudioClip;
        BackgroundSource.clip = BackgroundAudioClip;
        BackgroundSource.Play();
    }

    public void PlaySfx(string Route)
    {
        SfxAudioClip = Resources.Load(Route) as AudioClip;
        SfxSource.clip = SfxAudioClip;
        SfxSource.Play();
    }

    public void SetMusicVolumen(float volume)
    {
        AudioMixer.SetFloat("MusicVolumeParam", volume);
        PlayerPrefs.SetFloat("MusicVolumeLevel", volume);
    }

    public void SetSfxVolumen(float volume)
    {
        AudioMixer.SetFloat("SfxVolumeParam", volume);
        PlayerPrefs.SetFloat("SfxVolumeLevel", volume);
    }
}
