using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMusicToManager : MonoBehaviour
{
    public AudioClip myMusic;
    public bool PlayNone = false;
    // Start is called before the first frame update

    void Start()
    {
        if (PlayNone)
        {
            SoundManager.instance.StopMusic();
        }
        else
        {
            SoundManager.instance.PlayMusic(myMusic);
        }
    }
}
