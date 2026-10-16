using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource _musicSource, _effectSource;

    //Singleton code---------------------------------
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //Functions-------------------------------
    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip); //PlayOneShot will allow us to play multiple sounds at the same time
    }

    public void PlayMusic(AudioClip myClip)
    {
        // If clip sent isn't the same as the one currently playing
        if(_musicSource.GetComponent<AudioSource>().clip != myClip)
        {
            _musicSource.GetComponent<AudioSource>().clip = myClip;
            _musicSource.Play(); //Only one song shall play at once, for now at least
        }
        else // If it IS the same one, only play it if it wasn't playing already
        {
            if (!_musicSource.isPlaying)
            {
                _musicSource.Play();
            }
        }
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }
}