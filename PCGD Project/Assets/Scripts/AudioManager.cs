using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    public float minWaitBetweenPlays = 1f;
    public float maxWaitBetweenPlays = 5f;
    public float waitTimeCountdown = -1f;

    public Sound[] sounds;

    public static AudioManager instance;

    [SerializeField] Slider volumeSlider;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadVolume();
        }
        else { LoadVolume(); }

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch;
            
        }
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }

    public void PlayZombieNoises(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;



        if (!s.source.isPlaying)

            if (waitTimeCountdown < 0f)
            {
                
                s.source.Play();
                waitTimeCountdown = UnityEngine.Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays);
            }
            else
            {
                waitTimeCountdown -= Time.deltaTime;
            }
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Pause();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolume();
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    void SaveVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
