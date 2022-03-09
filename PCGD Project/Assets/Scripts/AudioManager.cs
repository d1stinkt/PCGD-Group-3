using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public float minWaitBetweenPlays = 1f;
    public float maxWaitBetweenPlays = 5f;
    public float waitTimeCountdown = -1f;
    

    public Sound[] sounds;

    public static AudioManager instance;

    static readonly string FirstPlay = "FirstPlay";
    static readonly string volumePref = "VolumePref";

    int firstPlayInt;
    float volumeFloat;

    [SerializeField] Slider volumeSlider;
    [SerializeField] GameObject menu;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if(firstPlayInt == 0)
        {
            volumeFloat = 0.75f;
            volumeSlider.value = volumeFloat;
            PlayerPrefs.SetFloat(volumePref, volumeFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            volumeFloat = PlayerPrefs.GetFloat(volumePref);
            volumeSlider.value = volumeFloat;
        }
    }

    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Menu")
        {
            menu.SetActive(true);
        }
        else 
        {
            ContinueSettings();
            menu.SetActive(false); 
        }

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

    public IEnumerator FadeOut(string name, float fadeSpeed)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        

        float startVolume = s.source.volume;
        float adjustedVolume = startVolume;

        while (adjustedVolume > 0)
        {
            adjustedVolume -= startVolume * Time.fixedDeltaTime / fadeSpeed;
            yield return null;
        }

        s.source.Stop();
        s.source.volume = startVolume;
       
    }

    public IEnumerator FadeIn(string name, float fadeSpeed)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        float adjustedVolume = s.source.volume;
        adjustedVolume = 0f;
        s.source.Play();
        
        while (adjustedVolume < 0.5f)
        {
            adjustedVolume += Time.deltaTime / fadeSpeed;
            yield return null;
        }

        s.source.volume = 0.5F;
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Pause();
    }

    void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(volumePref, volumeSlider.value);
    }

    public void UpdateSound()
    {
        AudioListener.volume = volumeSlider.value;
    }

    void ContinueSettings()
    {
        volumeFloat = PlayerPrefs.GetFloat(volumePref);
        AudioListener.volume = volumeFloat;
    }

    void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveVolumeSettings();
        }
    }

    public void EnableMenu()
    {
        StartCoroutine(ActivateMenu());
    }

    IEnumerator ActivateMenu()
    {
        yield return new WaitForSeconds(1.4f);
        menu.SetActive(true);
    }
}
