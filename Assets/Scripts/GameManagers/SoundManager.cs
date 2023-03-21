using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool mute = false;
    [Range(0, 1)]
    public float Volume = 1f;

    [SerializeField] private AudioSource SoundEffect;
    [SerializeField] private AudioSource Music;
    [SerializeField] private SoundType[] Sounds;


    private void Start()
    {
        PlayMusic(SoundEvents.BackgroundMusic);
    }

    private void Update()
    {
        AudioSetting(Volume, mute);
    }

    public void PlayMusic(SoundEvents sound)
    {

        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        { 
            Music.clip = clip;
            Music.Play();
            Music.volume = 0.7f;
        }
        else
        {
            Debug.LogError("No Clip found for the event");
        }
    }

    public void Play(SoundEvents sound)
    {
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            SoundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("No Clip found for the event");
        }
    }

    public void Mute()
    {
        Play(SoundEvents.ButtonClick);
        if (mute)
        {
            mute = false;
        }
        else
        {
            mute = true;
        }
    }

    public void AudioSetting(float volume, bool status)
    {
        if (status)
        {
            Music.mute = true;
            SoundEffect.mute = true;
        }else if (!status)
        {
            Music.mute = false;
            SoundEffect.mute = false;
            Music.volume = volume;
            SoundEffect.volume = volume;
        }
    }

    private AudioClip getSoundClip(SoundEvents sound)
    {
        SoundType Clip = Array.Find(Sounds, i => i.soundType == sound);

        if (Clip != null)
        {
            return Clip.soundClip;
        }
        else
        {
            return null;
        }
    }



}
[Serializable]
public class SoundType
{
    public SoundEvents soundType;
    public AudioClip soundClip;
}

public enum SoundEvents
{
    ButtonClick,
    BackgroundMusic,
    Run,
    Jump,
    Travel,
    Attack,
    Hurt,
    Dead,
    EnemyDeath,
    CoinCollected,
    PotionCollected,
    DoorOpen,
    DoorLocked,
    GameWin,
    GameOver
}

