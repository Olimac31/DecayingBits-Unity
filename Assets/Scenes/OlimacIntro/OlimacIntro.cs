using UnityEngine;
using UnityEngine.Video;

public class OlimacIntro : MonoBehaviour
{
    VideoPlayer video;
    GodotTimer timer;
    bool already_left = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        video = GetComponentInChildren<VideoPlayer>();
        video.Play();

        timer = GetComponentInChildren<GodotTimer>();
        timer.timeout.AddListener(leave_intro);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && !already_left)
        {
            video.Stop();
            leave_intro();
        }
    }

    void leave_intro()
    {
        already_left = true;
        timer.timeout.RemoveListener(leave_intro);

        GameManager.instance.change_room("Assets/Scenes/MainMenu.unity");
    }
}
